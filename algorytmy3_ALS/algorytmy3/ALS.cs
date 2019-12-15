using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmy3
{
    class ALS
    {
        //private MatrixSetUp provider = new MatrixSetUp(8);
        // private int[,] Ratings = provider.Ratings;
        private int[,] Ratings;
        public double[,] P;
        public double[,] U;

        private void StepForU(int u)
        {
            // niezerowe koluny dla wiersza u w macierzy Ratings
            List<int> I_u = FlatNonZeroInRow(u, Ratings);
            double[,] P_I_u;
            for (int i = 0; i < P.GetLength(0); i++)
            {
                foreach (int i_u in I_u)
                {
                    //Teraz liczymy _P_I_u_(czyli kolumny z macierzy P o indeksach w _I_u_)
                    //  P_I_u = P[:, I_u]

                }
            }

        }


        // generuj nowa macierz  stworzona z kolumn macierzy bazowej
        //to do change to private
        public double[,] GetMatrixFromOtherMatrixColumns(int[,] matrix, List<int> columns)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] M = new double[rows, columns.Count()];
            int c = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (columns.Contains(j))
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            Console.WriteLine($" i, j : {i},{j},  c= {c}");
                            M[i, c] = matrix[i, j];
                        }  c += 1;
                    }
                }
            
            return M;
        }


        public void Test()
        {
            // StepForU(0);
            int[,] eyeMatrix = CreateEye(5);
            eyeMatrix[2, 1] = 8;
            eyeMatrix[0, 2] = 9;
            Utils<int>.PrintMatrix(eyeMatrix);
            List<int> myL = new List<int>();
            myL.Add(1);
            myL.Add(2);
            double[,] M = GetMatrixFromOtherMatrixColumns(eyeMatrix, myL);
            Utils<double>.PrintMatrix(M);


        }



        //zwraca indeksy kolumn, które mają wartości niezerowe w wierszu n macierzy array
        private List<int> FlatNonZeroInRow(int n,  int[,] array)  
        { 
             var listOfIndexes = new List<int>();
                // ma przeszukac wiersz n i znalezc  indeksy kolumn z niezerowymi elemenami          
                for (int j = 0; j < array.GetLength(1); j++) 
                {
                    if (array[n,j] != 0)
                        listOfIndexes.Add(j);
                }
                return listOfIndexes;
        }
        //zwraca indeksy wierszy, które mają wartości niezerowe w kolumnie n macierzy array
        private List<int> FlatNonZeroInColumn(int n, int[,] array)  
        {
            var listOfIndexes = new List<int>();         
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i,n] != 0)
                    listOfIndexes.Add(i);
            }
            return listOfIndexes;
        }

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

        public void TestMatrix()
        {/*
            MatrixSetUp provider = new MatrixSetUp(5);
            double[,] U = provider.U;
            double[,] trans = Transpose(U);
            Utils<double>.PrintMatrix(U);
            Console.WriteLine("trasponowana:\n");
            Utils<double>.PrintMatrix(trans);
*/
        int[,] eyeMatrix = CreateEye(5);
            eyeMatrix[2, 1] = 8;
            eyeMatrix[0,2] = 9;
            Utils<int>.PrintMatrix(eyeMatrix);
            
            /*List<int> nz_w_kolumnie = FlatNonZeroInColumn(2,eyeMatrix);
            List<int> nz_w_wierszu = FlatNonZeroInRow(2,eyeMatrix);
            foreach (int i in nz_w_wierszu){
                Console.WriteLine(i);
            }
            */

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
