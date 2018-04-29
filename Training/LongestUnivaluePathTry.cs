using System;
using System.Collections.Generic;
using System.Text;

namespace Training
{
    class LongestUnivaluePathTry
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        int maximum = 1;

        public int LongestUnivaluePath(TreeNode root)
        {
            if (root == null)
                return 0;

            if (root.left == null && root.right == null)
            {
                return 1;
            }
            else
            {


                return maximum;
            }
        }
    }
}
