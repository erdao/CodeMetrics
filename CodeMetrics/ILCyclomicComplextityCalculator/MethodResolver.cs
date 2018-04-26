using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ILCyclomicComplextityCalculator
{
    public static class MethodResolver
    {
        public static MethodResult Resolve(MethodDefinition method,int codeSmells)
        {
            int iLCc = 0;

            foreach (Instruction instruction in method.Body.Instructions)
            {
                if (IsTransferControl(instruction.OpCode))
                    iLCc++;
            }

            return new MethodResult(method.Name, iLCc, iLCc<codeSmells);
        }
        
        /// <summary>
        /// 判断规则是是否有 transfers control
        /// </summary>
        /// <param name="opCode"></param>
        /// <returns></returns>
        private static bool IsTransferControl(OpCode opCode)
        {
            if (opCode.Code == Code.Beq ||
                opCode.Code == Code.Beq_S || opCode.Code == Code.Bge || opCode.Code == Code.Bge_S ||
                opCode.Code == Code.Bge_Un || opCode.Code == Code.Bge_Un_S || opCode.Code == Code.Bgt ||
                opCode.Code == Code.Bgt_S || opCode.Code == Code.Bgt_Un || opCode.Code == Code.Bgt_Un_S ||
                opCode.Code == Code.Ble || opCode.Code == Code.Ble_S || opCode.Code == Code.Ble_Un ||
                opCode.Code == Code.Ble_Un_S || opCode.Code == Code.Blt || opCode.Code == Code.Blt_S ||
                opCode.Code == Code.Blt_Un || opCode.Code == Code.Blt_Un_S || opCode.Code == Code.Bne_Un ||
                opCode.Code == Code.Bne_Un_S ||
                opCode.Code == Code.Brfalse || opCode.Code == Code.Brfalse_S || opCode.Code == Code.Brtrue ||
                opCode.Code == Code.Brtrue_S)
            {
                return true;
            }

            if (opCode.Code == Code.Brfalse || opCode.Code == Code.Brfalse_S || opCode.Code == Code.Brtrue ||
                opCode.Code == Code.Brtrue_S||opCode.Code==Code.Br|| opCode.Code == Code.Br_S||
                opCode.Code == Code.Leave|| opCode.Code == Code.Leave_S)
            {
                return true;
            }

            return false;
        }
    }
}
