using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class BinaryTreeNode
    {
        public int Key;
        public BinaryTreeNode Left=null;
        public BinaryTreeNode Right =null;
        public BinaryTreeNode Parent=null;
        public BinaryTreeNode(int key)
        {
            Key = key;
        }
    }

    public static class Program
    {
        public static int N = 0;
        public static List<BinaryTreeNode> Tree;
        public static void Main()
        {
            //Ввод N
            Console.WriteLine("Input n:");
            int n = int.Parse(Console.ReadLine());
            N = n;
            //Ввод дерева как линейной структуры,
            //т.е если rootIndex=i,
            //то leftNodeIndex=2*i+1, а rightNodeIndex=2*i+2
            var listOfNodes = InputTree();
            Tree = listOfNodes;
            //Запуск 3-х потоков
            Parallel.For(0, 2, CallThreads);
        }

        public static void CallThreads(int i)
        {
            for(int k = i; k < Tree.Count; k += 3)
            {
                PrintIfNNodes(Tree[k], new List<BinaryTreeNode>() { Tree[k] }, 1, N);
            }
        }

        public static void PrintIfNNodes(BinaryTreeNode node, List<BinaryTreeNode> currentTree, int currentNodes, int n)
        {
            // если текущий узел null, то выходим
            
            
            
            
            
            if (node is null)
                return;
            // если в текущем дереве количество узлов равно n, то выводим дерево
            if (currentNodes == n)
            {
                PrintNodes(currentTree);
                return;
            }
            // если нет обоих сыновей, то выходим
            if (node.Left == null && node.Right == null)
            {
                return;
            }// если есть оба, то запускаемся сначала только от левого сына, потом от обоих с текущим узлом в левом и правом, потом только от правого
            if (node.Left != null && node.Right != null)
            {
                currentTree.Add(node.Left);
                PrintIfNNodes(node.Left, currentTree, currentNodes + 1, n);
                currentTree.Add(node.Right);
                PrintIfNNodes(node.Left, currentTree, currentNodes + 2, n);
                PrintIfNNodes(node.Right, currentTree, currentNodes + 2, n);
                currentTree.RemoveAt(currentNodes - 2);
                PrintIfNNodes(node.Right, currentTree, currentNodes + 1, n);
                currentTree.RemoveAt(currentNodes - 1);

            }// если есть только левый, то запускаемся только от него
            else if(node.Left == null)
            {
                currentTree.Add(node.Right);
                PrintIfNNodes(node.Right, currentTree, currentNodes + 1, n);
                currentTree.RemoveAt(currentNodes - 1);
            }// аналогично с правым
            else if(node.Right == null)
            {
                currentTree.Add(node.Left);
                PrintIfNNodes(node.Left, currentTree, currentNodes + 1, n);
                currentTree.RemoveAt(currentNodes - 1);
            }
        }

        private static void PrintNodes(List<BinaryTreeNode> tree)
        {
            //Вывод поддеревьев
            StringBuilder sb = new StringBuilder();
            sb.Append(tree[0].Key.ToString() + "-");
            for (int i = 1; i < tree.Count; i++)
            {
                sb.Append(tree[i].Key.ToString() + "-");
            }
            Console.WriteLine(sb.ToString());
        }

        private static List<BinaryTreeNode> InputTree()
        {
            Console.WriteLine("Input tree:");
            var listOfNodes = new List<BinaryTreeNode>();
            //Ввод ключей дерева
            while (true)
            {
                string key = Console.ReadLine();
                int value;
                if (int.TryParse(key, out value)) 
                    listOfNodes.Add(new BinaryTreeNode(value));
                else
                    break;
            }
            //Выставление у каждой Node левого и правого дочернего узла
            for (int i = 0; i < listOfNodes.Count; i++)
            {
                BinaryTreeNode node = listOfNodes[i];
                int leftNum = 2 * i + 1;
                if (leftNum < listOfNodes.Count)
                    if (listOfNodes[leftNum].Key != 0)
                        node.Left = listOfNodes[leftNum];
                int rightNum = 2 * i + 2;
                if (rightNum < listOfNodes.Count)
                    if (listOfNodes[rightNum].Key != 0)
                        node.Right = listOfNodes[rightNum];
            }
            return listOfNodes;
        }
    }
}