using System.Collections.Generic;

namespace ILCyclomicComplextityCalculator
{
    public class MethodResult
    {
        public MethodResult(string methodName, int iLcc, bool isPass)
        {
            MethodName = methodName;
            ILCc = iLcc;
            IsPass = isPass;
        }

        public string MethodName { get; set; }

        public int ILCc { get; set; }

        public bool IsPass { get; set; }
    }

    public class TypeResult
    {
        public string TypeName { get; set; }

        public List<MethodResult> MethodResults { get; set; } = new List<MethodResult>();
    }


    public class AssemblyResult
    {
        public string AssemblyName { get; set; }

        public List<TypeResult> TypeResults { get; set; } = new List<TypeResult>();
    }
}
