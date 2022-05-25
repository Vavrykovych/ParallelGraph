using ParallelLib.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLib
{

    public class ExpressionGraph : IGraph
    {
        private readonly int[,] matrix;
        private readonly Node[] nodes;

        public int Size => matrix.GetLength(0);

        public ExpressionGraph(Node tree)
        {
            int size = ExpressionTree.GetSize(tree);
            nodes = ExpressionTree.ToList(tree).ToArray();
            matrix = new int[size, size];
            
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (ReferenceEquals(nodes[i].left, nodes[j]) || ReferenceEquals(nodes[i].right, nodes[j]))
                        matrix[i, j] = 1;
                }
            }
        }
        public bool HasPath(int from, int to)
        {
            return matrix[from, to] == 1;
        }
        public void SetPath(int from, int to)
        {
            matrix[from, to] = 1;
        }
        public void RemovePath(int from, int to)
        {
            matrix[from, to] = 0;
        }
    }
}
