using System;

namespace ParallelLib.Matrix
{
    public class Matrix
    {
        private double[,] matrix;

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix(int size)
        {
            this.matrix = new double[size,size];
        }

        public Matrix(int n, int m)
        {
            this.matrix = new double[n, m];
        }

        public static Matrix CreateRandom(int size)
        {
            Random random = new Random();
            Matrix matrix = new Matrix(size);
            for(int i = 0;i < size; i++)
            {
                for(int j = 0;j < size; j++)
                {
                    matrix[i, j] = random.NextDouble() * random.Next(int.MaxValue);
                }
            }

            return matrix;
        }

        public double this[int row, int col]
        {
            get { return matrix[row, col]; }
            set { matrix[row, col] = value; }
        }


        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new ArgumentException("Matrixes must be the same size.");
            }
            Matrix res = new Matrix(matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1));
            for (int i = 0; i < res.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < res.matrix.GetLength(1); j++)
                {
                    res[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return res;
        }
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new ArgumentException("Matrixes must be the same size.");
            }
            Matrix res = new Matrix(matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1));
            for (int i = 0; i < res.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < res.matrix.GetLength(1); j++)
                {
                    res[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return res;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(0))
            {
                throw new ArgumentException("Matrix sizes not correct.");
            }
            Matrix res = new Matrix(matrix1.matrix.GetLength(0), matrix2.matrix.GetLength(1));

            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix1.matrix.GetLength(1); k++)
                    {
                        res[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return res;
        }
    }
}
