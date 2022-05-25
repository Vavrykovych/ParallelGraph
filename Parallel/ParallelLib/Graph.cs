using ParallelLib.Abstract;
using System;

namespace ParallelLib
{
    public class Graph : IGraph
    {
        private readonly int[,] matrix;

        public int Size => matrix.GetLength(0);

        public Graph(int n)
        {
            matrix = new int[n, n];
        }

        public Graph(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Invalid matrix size.");
            }

            this.matrix = matrix;
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
