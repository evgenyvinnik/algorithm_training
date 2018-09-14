using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexport2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> stacks = new List<List<int>>();
            stacks.Add(new List<int>());

            Push(ref stacks, 1);
            Push(ref stacks, 2);

            Begin(ref stacks);
            Console.WriteLine($"{Pop(ref stacks)}");
            Push(ref stacks, 3);
            Console.WriteLine($"{Peek(stacks)}");
            RollBack(ref stacks);

            Console.WriteLine($"{Peek(stacks)}");

            Begin(ref stacks);
            Push(ref stacks, 4);
                Begin(ref stacks);
                Push(ref stacks, 5);
                Commit(ref stacks);
            Console.WriteLine($"{Peek(stacks)}");
            RollBack(ref stacks);

            Console.WriteLine($"{Peek(stacks)}");

            //RollBack(ref stacks);//should return error
        }

        static int Pop(ref List<List<int>> stacks)
        {
            if(stacks.Count == 0)
            {
                throw new Exception();
            }
            var topStack = stacks[stacks.Count - 1];

            if (topStack.Count == 0)
            {
                throw new Exception();
            }
            var val = topStack[topStack.Count - 1];

            topStack.RemoveAt(topStack.Count - 1);

            return val;
        }

        static void Push(ref List<List<int>> stacks, int val)
        {
            if (stacks.Count == 0)
            {
                throw new Exception();
            }

            var topStack = stacks[stacks.Count - 1];
            topStack.Add(val);
        }

        static int Peek(List<List<int>> stacks)
        {
            if (stacks.Count == 0)
            {
                throw new Exception();
            }

            var topStack = stacks[stacks.Count - 1];
            if (topStack.Count == 0)
            {
                throw new Exception();
            }
            var val = topStack[topStack.Count - 1];
            return val;
        }

        static void Begin(ref List<List<int>> stacks)
        {
            if (stacks.Count == 0)
            {
                throw new Exception();
            }

            var topStack = stacks[stacks.Count - 1];

            stacks.Add(new List<int>(topStack));
        }

        static void RollBack(ref List<List<int>> stacks)
        {
            if (stacks.Count <= 1)
            {
                throw new Exception();
            }

            stacks.RemoveAt(stacks.Count - 1);
        }


        static void Commit(ref List<List<int>> stacks)
        {
            if (stacks.Count <=2)
            {
                throw new Exception();
            }

            var topStack = stacks[stacks.Count - 1];
            stacks.RemoveAt(stacks.Count - 1);

            var replaceStack = stacks[stacks.Count - 1];
            stacks.RemoveAt(stacks.Count - 1);

            stacks.Add(new List<int>(topStack));
        }
    }
}
