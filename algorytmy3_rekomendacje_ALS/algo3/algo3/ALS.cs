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
        MatrixSetUp Provider;

        private int d = 3;
        private double reg = 0.1;
        private int iteration = 300;

        public ALS()
        {
            Provider = new MatrixSetUp(15);  // give this argument for ALS 

           SetValues(Provider);

           // SetTestVal();

            int usersCount = Ratings.GetLength(0);
            int productsConut = Ratings.GetLength(1);
            /*
            Console.WriteLine($"testowe dane ratings userxproduct [{usersCount}x{productsConut}]");

            Console.WriteLine("\n P  \n");
            Utils<double>.PrintMatrix(P);
            Console.WriteLine("\n U  \n");
            Utils<double>.PrintMatrix(U);*/
            for (int i = 0; i < iteration; i++)
            {
                for (int u = 0; u < usersCount; u++)   //wazne tylko do u < d  potem sie liczy, ale nie wpisuje do U, bo jest za małych rozmiarów ... 
                    StepForU(u);

                for (int p = 0; p < productsConut; p++)
                    StepForP(p);
            }
            ObjectiveFunction.Calculate(Ratings, U, P, d, reg);

            for (int i = 0; i < iteration; i++)
            {
                for (int u = 0; u < usersCount; u++)   //wazne tylko do u < d  potem sie liczy, ale nie wpisuje do U, bo jest za małych rozmiarów ... 
                    StepForU(u);

                for (int p = 0; p < productsConut; p++)
                    StepForP(p);
            }

            ObjectiveFunction.Calculate(Ratings, U, P, d, reg);

            for (int i = 0; i < iteration; i++)
            {
                for (int u = 0; u < usersCount; u++)   //wazne tylko do u < d  potem sie liczy, ale nie wpisuje do U, bo jest za małych rozmiarów ... 
                    StepForU(u);

                for (int p = 0; p < productsConut; p++)
                    StepForP(p);
            }

            ObjectiveFunction.Calculate(Ratings, U, P, d, reg);


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
            

            //test
            /*
            Console.WriteLine($"wymiary B_u: {_B_u.GetLength(0)}, {_B_u.GetLength(1)}");
            Console.WriteLine($"\n U_I_p: \n");
            Utils<double>.PrintMatrix(_U_I_p);
            Console.WriteLine($"\n U_I_p_T: \n");
            Utils<double>.PrintMatrix(_U_I_p_T);
            Console.WriteLine($"\n re E \n");
            Utils<double>.PrintMatrix(_regE); 

            Console.WriteLine("\n solution \n");
            Utils<double>.PrintVector(GaussSolution);
            Console.WriteLine("\n P  \n");
            Utils<double>.PrintMatrix(P);*/
        }

        private void StepForU(int u)
        {
            List<int> _I_u = FlatNonZeroInRow(u, Ratings);
            double[,] _regE = EyeMulDouble(d);
            MyMatrix<double> gauss = new MyMatrix<double>(d);

            //Liczymy _P_I_u_(czyli kolumny z macierzy P o indeksach w _I_u_)
            double[,] _P_I_u = MyMatrix<double>.GetMatrixFromOtherMatrixColumns(P, _I_u);
            double[,] _P_I_u_T = MyMatrix<double>.Transpose(_P_I_u);
            double[,] _A_u = 
                MyMatrix<double>.Add(
                    MyMatrix<double>.Multiplication(_P_I_u, _P_I_u_T),
                    _regE
                    );
            double[] _V_u = Count_V_u(_I_u, P, Ratings, u);

            //Jak już mamy wszystko policzone, możemy podstawić A_u oraz V_u do gaussa:
            gauss.A = _A_u;
            gauss.B = _V_u;
            gauss.ComputePG();
            double[] GaussSolution = gauss.Xgauss;
            U = InsertGaussColumn(u, U, GaussSolution);

            //test
            /*
            Console.WriteLine($"wymiary A_u: {_A_u.GetLength(0)}, {_A_u.GetLength(1)}");
            Console.WriteLine($"\n P_I_u: \n");
            Utils<double>.PrintMatrix(_P_I_u);
            Console.WriteLine("\n A jako A_u \n");
            Utils<double>.PrintMatrix(gauss.A);
            Console.WriteLine("\n V_u jako B \n");
            Utils<double>.PrintVector(gauss.B);
            Console.WriteLine("\n solution \n");
            Utils<double>.PrintVector(GaussSolution);
            Console.WriteLine("\n U  \n");
            Utils<double>.PrintMatrix(U);
            */
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
            var W_u = new double[d];
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

        // TODO  remove all test staff 
        public void Test()
        {
            //TestEyeMulDouble();
            //TestFloatNonZero();
            //TestColumnFromMatrix();
            //TestTransponce();
            //TestMultiplication();
        }
        //TODO  to remove
        private void SetTestVal()
        {
            Ratings = new int[,] { { 0, 0, 0, 0, 4, 0, 5, 4, 0, 0 },
                                    { 4, 0, 4, 0, 0, 4, 0, 0, 0, 4 },
                                    { 5, 4, 5, 5, 0, 5, 5, 5, 5, 5 },
                                    { 0, 5, 5, 0, 5, 0, 0, 5, 0, 5 },
                                    { 0, 5, 5, 0, 5, 0, 0, 5, 0, 5 }
                                 };
            P = new double[,] { {0.93119636, 0.01215318, 0.82254304, 0.92704314, 0.72097256,
                                 0.1119594 , 0.05907673, 0.27337659, 0.51578453, 0.47299487 },
                                {0.1671686, 0.02328032, 0.64793332, 0.46310597, 0.98508579,
                                 0.23390272, 0.34862754, 0.29751156, 0.81994987, 0.32293732 },
                                {0.72302848, 0.91165485, 0.70980305, 0.20125138, 0.33071352,
                                 0.40941998, 0.6984816 , 0.94986196, 0.52719633, 0.66722182 }
                                };
            U = new double[,] {{0.02930222, 0.90635812, 0.71271017 },
                               {0.03319273, 0.2316068, 0.96492267 },
                               {0.35638381, 0.42064508, 0.83929454 }
                              };
        }
        private double[,] TestCreateEye(int size)
        {
            var eye = new double[size, size];
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
        private void TestEyeMulDouble()
        {
            double[,] eyeMatrix = EyeMulDouble(5);
            Utils<double>.PrintMatrix(eyeMatrix);
        }
        private void TestFloatNonZero()
        {
            int[,] eyeMatrix = CreateEye(5);
            eyeMatrix[2, 1] = 8;
            eyeMatrix[0, 2] = 9;
            List<int> nz_w_kolumnie = FlatNonZeroInColumn(2, eyeMatrix);
            List<int> nz_w_wierszu = FlatNonZeroInRow(2, eyeMatrix);
            foreach (int i in nz_w_wierszu)
            {
                Console.WriteLine(i);
            }
        }
        private void TestColumnFromMatrix()
        {
            double[,] eyeMatrix = TestCreateEye(5);
            eyeMatrix[2, 1] = 8;
            eyeMatrix[0, 2] = 9;
            Utils<double>.PrintMatrix(eyeMatrix);
            List<int> myL = new List<int>();
            myL.Add(1);
            myL.Add(2);
            double[,] M = MyMatrix<double>.GetMatrixFromOtherMatrixColumns(eyeMatrix, myL);
            Utils<double>.PrintMatrix(M);
        }
        private void TestTransponce()
        {
            MatrixSetUp provider = new MatrixSetUp(5);
            double[,] U = provider.U;
            double[,] trans = MyMatrix<double>.Transpose(U);
            Utils<double>.PrintMatrix(U);
            Console.WriteLine("trasponowana:\n");
            Utils<double>.PrintMatrix(trans);
        }

        private void TestMultiplication()
        {
            double[,] eyeMatrix = TestCreateEye(2);
            eyeMatrix[0, 0] = 1;
            eyeMatrix[0, 1] = 2;
            eyeMatrix[1, 0] = 3;
            eyeMatrix[1, 1] = 4;
            Utils<double>.PrintMatrix(eyeMatrix);
            double[,] m2 = TestCreateEye(2);
            m2[0, 0] = 5;
            m2[0, 1] = 6;
            m2[1, 0] = 7;
            m2[1, 1] = 8;
            Utils<double>.PrintMatrix(m2);

            double[,] sol = MyMatrix<double>.Multiplication(eyeMatrix, m2);
            Utils<double>.PrintMatrix(sol);   // [19, 20; 43, 50]
        }
    }
}

