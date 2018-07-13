using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Construct tree
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Root = new BinaryTreeNode<int>(1);
            bTree.Root.Left = new BinaryTreeNode<int>(2);
            bTree.Root.Right = new BinaryTreeNode<int>(3);
            bTree.Root.Left.Left = new BinaryTreeNode<int>(4);
            bTree.Root.Left.Right = new BinaryTreeNode<int>(5);
            bTree.Root.Right.Right = new BinaryTreeNode<int>(6);
            bTree.Root.Right.Left = new BinaryTreeNode<int>(7);
            bTree.Root.Right.Left.Left = new BinaryTreeNode<int>(8);

            //Transverse and assign to hashmap
            VerticalSum(bTree);
            Console.WriteLine("Max Width: " + MaxWidthViaQueue(bTree.Root));
            Console.WriteLine("Max Height: " + MaxHeight(bTree.Root));
        }

        /// <summary>
        /// Returns the max height of a binary tree
        /// </summary>

        private static int MaxHeight(BinaryTreeNode<int> node)
        {
            if (node == null) return 0;

            int lHeight = MaxHeight(node.Left);
            int rHeight = MaxHeight(node.Right);

            return lHeight > rHeight ? lHeight + 1 : rHeight + 1;
        }

        /// <summary>
        /// Returns the max width of a binary tree
        /// </summary>
        private static int MaxWidthViaQueue(BinaryTreeNode<int> node)
        {
            if (node == null)
                return 0;

            int maxWidth = 0;

            Queue<BinaryTreeNode<int>> q = new Queue<BinaryTreeNode<int>>();
            q.Enqueue(node);

            while (q.Count != 0)
            {
                int count = q.Count;
                maxWidth = Math.Max(maxWidth, q.Count);

                while(count-- > 0)
                {
                    var temp = q.Dequeue();
                    if (temp.Left != null)
                        q.Enqueue(temp.Left);
                    if (temp.Right != null)
                        q.Enqueue(temp.Right);
                }
            }

            return maxWidth;
        }

        /// <summary>
        /// Prints out the vertical sum of each vertical line
        /// </summary>
        private static void VerticalSum(BinaryTree<int> bTree)
        {
            if (bTree.Root == null)
                return;

            Dictionary<int, int> hashMap = new Dictionary<int, int>();

            TransverseAndMap(hashMap, 0, bTree.Root);

            foreach (var entry in hashMap)
            {
                Console.WriteLine("Line/Value: {0} / {1}",
                    entry.Key, entry.Value);
            }
        }

        private static void TransverseAndMap(Dictionary<int, int> hashMap, int verticalPosition, BinaryTreeNode<int> root)
        {
            if (hashMap == null || root == null)
                return;

            TransverseAndMap(hashMap, verticalPosition - 1, root.Left);

            if (hashMap.ContainsKey(verticalPosition))
                hashMap[verticalPosition] += root.Value;
            else
                hashMap.Add(verticalPosition, root.Value);

            TransverseAndMap(hashMap, verticalPosition + 1, root.Right);
        }
    }
}
