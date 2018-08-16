using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap
{
    class Program
    {
        class Tree
        {
            public Tree left;
            public Tree right;
            public int data;

            public Tree(int data) : this(data, null, null)
            {

            }

            public Tree(int data, Tree left, Tree right)
            {
                this.data = data;
                this.left = left;
                this.right = right;
            }
        }
        //        1
        //     2   3
        //   4   5    6
        // 4 5 2 6 3 1
        //
        static void Main(string[] args)
        {
            Tree tree4 = new Tree(4);
            Tree tree5 = new Tree(5);
            Tree tree6 = new Tree(6);

            Tree tree2 = new Tree(2, tree4, tree5);
            Tree tree3 = new Tree(3, null, tree6);

            Tree root = new Tree(1, tree2, tree3);

            List<int> res = postfix(root);

            for(int i = res.Count-1; i>=0; i--)
            {
                Console.WriteLine(res[i]);
            }
        }

        static List<int> postfix(Tree root)
        {
            List<int> res = new List<int>();

            Stack<Tree> stack = new Stack<Tree>();
            stack.Push(root);

            while(stack.Count > 0)
            {
                Tree p = stack.Pop();
                if(p.left != null)
                {
                    stack.Push(p.left);
                }

                if (p.right != null)
                {
                    stack.Push(p.right);
                }

                res.Add(p.data);
            }

            return res;
        }
    }
}
