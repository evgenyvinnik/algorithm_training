using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q
{
    class Program
    {
        enum OperationType
        {
            Sum,
            Sub,
            Mult,
            Div,
            Opd
        }

        static void Main(string[] args)
        {
            foreach(var arg in args)
            {
                Console.WriteLine(PerformCalculation(arg));
            }
        }

        static int PerformCalculation(string expression)
        {
            int result;

            var tokenizedExpressions = expression.Split(' ');
            int expressionPointer = 0;

            List<string> stackExpressions = new List<string>();

            for (; ; )
            {
                stackExpressions.Add(tokenizedExpressions[expressionPointer]);
                expressionPointer++;

                while (IsValidOperation(stackExpressions))
                {
                    int res = PerformOperation(stackExpressions);
                    stackExpressions.RemoveRange(stackExpressions.Count - 3, 3);
                    stackExpressions.Add(res.ToString());
                }

                if (stackExpressions.Count == 1 && GetOperationType(stackExpressions[0]) == OperationType.Opd)
                {
                    break;
                }
            }

            result = Int32.Parse(stackExpressions[0]);
            return result;
        }

        static int PerformOperation(List<string> stackExpressions)
        {
            int left = Int32.Parse(stackExpressions[stackExpressions.Count - 2]);
            int right = Int32.Parse(stackExpressions[stackExpressions.Count - 1]);

            switch (GetOperationType(stackExpressions[stackExpressions.Count - 3]))
            {
                case OperationType.Sum:
                    return left + right;
                case OperationType.Sub:
                    return left - right;
                case OperationType.Mult:
                    return left * right;
                case OperationType.Div:
                    return left / right;
                default:
                    throw new Exception();
            }
        }

        static OperationType GetOperationType(string op)
        {
            switch (op)
            {
                case "+":
                    return OperationType.Sum;
                case "-":
                    return OperationType.Sub;
                case "*":
                    return OperationType.Mult;
                case "/":
                    return OperationType.Div;
                default:
                    return OperationType.Opd;
            }
        }

        static bool IsValidOperation(List<string> stackExpressions)
        {
            if (stackExpressions.Count >= 3)
            {
                if (GetOperationType(stackExpressions[stackExpressions.Count - 1]) == OperationType.Opd &&
                    GetOperationType(stackExpressions[stackExpressions.Count - 2]) == OperationType.Opd &&
                    GetOperationType(stackExpressions[stackExpressions.Count - 3]) != OperationType.Opd)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
