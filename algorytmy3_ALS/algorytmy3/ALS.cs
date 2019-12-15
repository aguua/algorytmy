using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmy3
{
    class ALS
    {

        private double[,] Transpose(double [,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] transposed = new double[cols, rows];
            for(int i = 0; i < rows; i++)
            {
                for (int j =0; j< cols; j++)
                {
                    transposed[j,i] = matrix[i,j];
                }
            }
            return transposed;
        }

        public void Test()
        {/*
            MatrixSetUp provider = new MatrixSetUp(5);
            double[,] U = provider.U;
            double[,] trans = Transpose(U);
            Utils<double>.PrintMatrix(U);
            Console.WriteLine("trasponowana:\n");
            Utils<double>.PrintMatrix(trans);
*/
            int[,] eyeMatrix = CreateEye(5);
            Utils<int>.PrintMatrix(eyeMatrix);
        }



    private int[,] CreateEye(int size) 
        {
            var eye = new int[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (i == j) eye[i, j] = 1;
                    else eye[i, j] = 0;
                }
            }
            return eye;
        }
    }
}
