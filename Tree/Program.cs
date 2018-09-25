using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class TreeNode
    {
        public int Value { get; set; }

        public TreeNode Left { get; set; }

        public TreeNode Right { get; set; }

        public TreeNode Peer { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Peer = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TreeNode node10 = new TreeNode(10);
            TreeNode node11 = new TreeNode(11);

            TreeNode node5 = new TreeNode(5);
            TreeNode node4 = new TreeNode(4);
            TreeNode node3 = new TreeNode(3);
            TreeNode node2 = new TreeNode(2);

            TreeNode node0 = new TreeNode(0);
            TreeNode node1 = new TreeNode(1);

            TreeNode nodem1 = new TreeNode(-1);

            nodem1.Left = node0;
            nodem1.Right = node1;

            node0.Left = node5;
            node0.Right = node4;

            node1.Left = node3;
            node1.Right = node2;

            node5.Left = node10;
            node2.Right = node11;

            bindPeers(ref nodem1);
        }

        static void bindPeers(ref TreeNode root)
        {
            var searchQueue = new Queue<Tuple<int, TreeNode>>();

            searchQueue.Enqueue(new Tuple<int, TreeNode>(0, root));

            Tuple<int, TreeNode> first = null;
            Tuple<int, TreeNode> prev = null;

            Tuple<int, TreeNode> tuple = null;

            while (searchQueue.Count > 0)
            {
                tuple = searchQueue.Dequeue();

                if (first == null)
                {
                    first = tuple;
                    prev = tuple;
                }

                int level = tuple.Item1;
                TreeNode node = tuple.Item2;

                if (level > prev.Item1)
                {
                    first.Item2.Peer = prev.Item2;
                    first = tuple;
                }
                else
                {
                    node.Peer = prev.Item2;
                }

                prev = tuple;
                if(node.Left != null)
                    searchQueue.Enqueue(new Tuple<int, TreeNode>(level + 1, node.Left));
                if (node.Right != null)
                    searchQueue.Enqueue(new Tuple<int, TreeNode>(level + 1, node.Right));
            }

            if (tuple != null)
            {
                first.Item2.Peer = tuple.Item2;
            }
        }
    }
}
