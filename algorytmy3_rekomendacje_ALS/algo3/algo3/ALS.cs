using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo3
{
    class ALS
    {
        private int[,] Ratings;
        public double[,] P;
        public double[,] U;
        private MatrixSetUp Provider;
        private List<ReportData> ReportData = new List<ReportData>();

        private int d;
        private double reg;

        private int iteration = 300;
        int productsCount;
        int usersCount;
        public ALS(int productsCount, int usersCount, int d, double reg)
        {
            this.d = d;
            this.reg = reg;

            Provider = new MatrixSetUp(productsCount, usersCount, d);
            SetValues(Provider);

            for (int i = 0; i < iteration; i++)
            {
                for (int u = 0; u < usersCount; u++)
                    StepForU(u);

                for (int p = 0; p < productsCount; p++)
                    StepForP(p);

                var objFunc = ObjectiveFunction.Calculate(Ratings, U, P, d, reg);
                ReportData.Add(new ReportData(i, reg, d, objFunc));
            }
        }

        private void SetValues(MatrixSetUp Provider)
        {
            U = Provider.U;
            P = Provider.P;
            Ratings = Provider.Ratings;
        }

        private void StepForP(int p)
        {
            List<int> _I_p = FlatNonZeroInColumn(p, Ratings);
            double[,] _regE = EyeMulDouble(d);
            MyMatrix<double> gauss = new MyMatrix<double>(d);

            double[,] _U_I_p = MyMatrix<double>.GetMatrixFromOtherMatrixColumns(U, _I_p);
            double[,] _U_I_p_T = MyMatrix<double>.Transpose(_U_I_p);
            double[,] _B_u =
                 MyMatrix<double>.Add(
                     MyMatrix<double>.Multiplication(_U_I_p, _U_I_p_T),
                     _regE
                     );
            double[] _W_u = Count_W_u(_I_p, U, Ratings, p);
            gauss.A = _B_u;
            gauss.B = _W_u;
            gauss.ComputePG();
            double[] GaussSolution = gauss.Xgauss;
            P = InsertGaussColumn(p, P, GaussSolution);
        }

        private void StepForU(int u)
        {
            List<int> _I_u = FlatNonZeroInRow(u, Ratings);
            double[,] _regE = EyeMulDouble(d);
            MyMatrix<double> gauss = new MyMatrix<double>(d);

            double[,] _P_I_u = MyMatrix<double>.GetMatrixFromOtherMatrixColumns(P, _I_u);
            double[,] _P_I_u_T = MyMatrix<double>.Transpose(_P_I_u);
            double[,] _A_u = MyMatrix<double>.Add(
                                MyMatrix<double>.Multiplication(_P_I_u, _P_I_u_T),
                                                                _regE);
            double[] _V_u = Count_V_u(_I_u, P, Ratings, u);
            gauss.A = _A_u;
            gauss.B = _V_u;
            gauss.ComputePG();
            double[] GaussSolution = gauss.Xgauss;
            U = InsertGaussColumn(u, U, GaussSolution);
        }

        private double[] Count_V_u(List<int> listOfIndexes, double[,] arrayIndexValues, int[,] RatingsMatrix, int u)
        {
            var V_u = new double[d];
            var j = 0;
            for (; j < d; j++)
            {
                V_u[j] = 0;
            }
            j = 0;
            for (; j < d; j++)
            {
                foreach (var index in listOfIndexes) // for each non-zero rate from rating array - looking in columns (products)
                {

                    V_u[j] += arrayIndexValues[j, index] * RatingsMatrix[u, index];
                }
            }
            return V_u;
        }
        private double[] Count_W_u(List<int> listOfIndexes, double[,] arrayIndexValues, int[,] RatingsMatrix, int p)

        {
            var W_u = new double[d];   //  od listOfIndexes.Count? ? ?
            var j = 0;
            for (; j < d; j++)
            {
                W_u[j] = 0;
            }
            j = 0;

            foreach (var index in listOfIndexes) // for each non-zero rate from rating array  - looking in rows (users)
            {
                for (; j < d; j++)
                {
                    W_u[j] += arrayIndexValues[j, index] * RatingsMatrix[index, p];
                }
            }
            return W_u;
        }

        // Insert Gauss solution for u column into u column inside matrixU
        private double[,] InsertGaussColumn(int u, double[,] matrixU, double[] GaussSolution)
        {
            var ArrayAfterSwitch = new double[matrixU.GetLength(0), matrixU.GetLength(1)];
            //search for column
            for (var j = 0; j < matrixU.GetLength(1); j++)
            {
                for (var i = 0; i < matrixU.GetLength(0); i++)
                {
                    if (j == u) ArrayAfterSwitch[i, j] = GaussSolution[i];    /// j == u  switch existing column with gauss solution
                    else ArrayAfterSwitch[i, j] = matrixU[i, j];
                }
            }
            return ArrayAfterSwitch;
        }

        //Returs indexes of columns for each matrix 'array' has non-zero values in 'n' ROW.  //zwraca indeksy kolumn, które mają wartości niezerowe w wierszu n macierzy array
        private List<int> FlatNonZeroInRow(int n, int[,] array)
        {
            var listOfIndexes = new List<int>();
            // ma przeszukac wiersz n i znalezc  indeksy kolumn z niezerowymi elemenami          
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[n, j] != 0)
                    listOfIndexes.Add(j);
            }
            return listOfIndexes;
        }
        //Returns indexes of rows for each matrix 'array' has non-zero values in 'n' COLUMN //zwraca indeksy wierszy, które mają wartości niezerowe w kolumnie n macierzy array
        private List<int> FlatNonZeroInColumn(int n, int[,] array)
        {
            var listOfIndexes = new List<int>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, n] != 0)
                    listOfIndexes.Add(i);
            }
            return listOfIndexes;
        }

        //Create Eye Matrix
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

        //Returns Eye Matrix multiplied by reg (lambda parameter)
        private double[,] EyeMulDouble(int size)
        {
            var multiplied = new double[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (i == j) multiplied[i, j] = reg;
                    else multiplied[i, j] = 0;
                }
            }
            return multiplied;
        }
    }
}

