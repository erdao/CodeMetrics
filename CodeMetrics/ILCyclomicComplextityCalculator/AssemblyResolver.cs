using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace ILCyclomicComplextityCalculator
{
    public static class AssemblyResolver
    {
        public static async Task<AssemblyResult> Resolver(string assemblyPath, int codeSmells)
        {
            var result = await Task.Run(() =>
            {
                var assemblyResult = new AssemblyResult { AssemblyName = Path.GetFileName(assemblyPath) };

                var assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyPath);

                var allTypes = GetAllTypes(assemblyDefinition);

                foreach (var typeDefinition in allTypes)
                {
                    if (!IsValidType(typeDefinition))
                    {
                        continue;
                    }

                    var validMethods = typeDefinition.Methods.Where(IsValidMethod).ToList();
                    if (!validMethods.Any())
                    {
                        continue;
                    }

                    var typeResult = new TypeResult { TypeName = typeDefinition.FullName };
                    foreach (var methodDefinition in validMethods)
                    {
                        var methodResult = MethodResolver.Resolve(methodDefinition, codeSmells);
                        typeResult.MethodResults.Add(methodResult);
                    }

                    assemblyResult.TypeResults.Add(typeResult);
                }

                return assemblyResult;
            });

            return result;
        }


        private static bool IsValidType(TypeDefinition typeDefinition)
        {
            if (IgnoreTypes.Any(x => typeDefinition.FullName.Contains(x)))
                return false;

            if (!typeDefinition.IsClass)
                return false;

            return true;
        }

        private static readonly List<string> IgnoreMethods = new List<string>()
        {
            "InitializeComponent",
            "Markup.IComponentConnector",
            "__"
        };

        private static readonly List<string> IgnoreTypes = new List<string>()
        {
            "Properties.Resources",
            "Properties.Settings",
            "GeneratedInternalTypeHelper",
            "PrivateImplementationDetails",
            "<Module>",
            "__"
        };


        private static bool IsValidMethod(MethodDefinition methodDefinition)
        {
            return methodDefinition.Body != null && !methodDefinition.IsSetter &&
                   !methodDefinition.IsGetter && !methodDefinition.IsConstructor &&
                   !IgnoreMethods.Any(x => methodDefinition.Name.Contains(x));
        }


        private static IEnumerable<TypeDefinition> GetAllTypes(AssemblyDefinition assemblyDefinition)
        {
            foreach (var type in assemblyDefinition.MainModule.Types)
            {
                yield return type;
            }
        }
    }
}
