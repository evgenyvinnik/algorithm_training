using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "20+2*3";
            Console.WriteLine($"{calculator(s)}");
        }

        static int calculator(string s)
        {
            var sOp = s.Split('+', '*');

            List<int> ops = new List<int>();
            foreach (var s1 in sOp)
            {
                ops.Add(Int32.Parse(s1));
            }

            Stack<int> st = new Stack<int>();

            int currOp = 0;

            bool Plus = false;
            bool Mult = false;

            foreach (var c in s)
            {
                if (c == '+')
                {
                    if (st.Count == 0)
                    {
                        st.Push(ops[currOp]);

                        currOp++;
                    }

                    st.Push(ops[currOp]);
                    currOp++;
                    if (currOp >= ops.Count)
                        break;

                    Plus = true;
                }
                else if (c == '*')
                {
                    if (st.Count == 0)
                    {
                        st.Push(ops[currOp]);
                        currOp++;
                    }

                    int op = st.Pop();
                    //st.Push(ops[currOp]);
                    st.Push(op * ops[currOp]);
                    currOp++;

                    if (currOp >= ops.Count)
                        break;


                    Mult = true;
                }
            }

            int res = 0;
            while (st.Count > 0)
            {
                res += st.Pop();
            }

            return res;
        }
    }
}
