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
         



        private int d = 3;  //parameter
        private double reg = 0.1; //parametr 

        public ALS()
        {
            /*MatrixSetUp Provider = new MatrixSetUp(2);
            U = Provider.U;
            P = Provider.P;
            Ratings = Provider.Ratings;
            */
            Ratings =  new int[,] { { 0, 0, 0, 0, 4, 0, 5, 4, 0, 0 },
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

    int usersCount = Ratings.GetLength(0);
            int productsConut = Ratings.GetLength(1);
            Console.WriteLine($"testowe dane ratings userxproduct [{usersCount}x{productsConut}]");
            Utils<int>.PrintMatrix(Ratings);
            Console.WriteLine($"\ntestowe dane P: ");
            Utils<double>.PrintMatrix(P);
            Console.WriteLine($"\ntestowe dane U: ");
            Utils<double>.PrintMatrix(U);


            Console.ReadLine();
            for( int u =0; u < usersCount; u++)
            {
                Console.WriteLine($"u = {u}");
                StepForU(u);
                Console.ReadLine();

            }
           // StepForU(0);
        }

        private double[,] SetMatrix(int d, object u)
        {
            throw new NotImplementedException();
        }

        private void StepForU(int u)
        {
            // niezerowe koluny dla wiersza u w macierzy Ratings
            List<int> _I_u = FlatNonZeroInRow(u, Ratings);
            Console.WriteLine($"Znaleiono {_I_u.Count()} niezerowych wyrazow w wierszu u ");
            double[,] _regE = EyeMulDouble(d);

            MyMatrix<double> gauss = new MyMatrix<double>(_I_u.Count());


            //Teraz liczymy _P_I_u_(czyli kolumny z macierzy P o indeksach w _I_u_)
            double[,] _P_I_u = MyMatrix<double>.GetMatrixFromOtherMatrixColumns(P, _I_u);
            double[,]  _P_I_u_T = MyMatrix<double>.Transpose(_P_I_u);
            double[,]  _A_u = MyMatrix<double>.
                Add(
                    MyMatrix<double>.Multiplication(_P_I_u, _P_I_u_T),
                    _regE
                );

            Console.WriteLine($"wymiary A_u: {_A_u.GetLength(0)}, {_A_u.GetLength(1)}");


            double[] _V_u = Count_V_u(_I_u, P, Ratings, u);    //todo sprawdz czy dobrze podtsawiono 

            //Jak już mamy wszystko policzone, możemy podstawić A_u oraz V_u do gaussa:
            gauss.A = _A_u;
            gauss.B = _V_u;

            Console.WriteLine("\n A \n");
            Utils<double>.PrintMatrix(gauss.A);
            Console.WriteLine("\n B \n");
            Utils<double>.PrintVector(gauss.B);
            

            gauss.ComputePG();      // moja metoda gauusa jest tylko dla macierzy kwadratowych ... 
            double[] GaussSolution = gauss.Xgauss;
            Console.WriteLine("\n solution \n");
            Utils<double>.PrintVector(GaussSolution);


            Console.WriteLine("\n U  \n");
           U = SwitchGaussColumn(u, U, GaussSolution);
            Utils<double>.PrintMatrix(U);

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
                foreach (var index in listOfIndexes) // dla kazdej z ocen (indeksy niezerowych ocen)
                {
                    V_u[j] += arrayIndexValues[j, index] * RatingsMatrix[u, index]; // 
                                                                                //arrayIndexValues[i++, j] ??
                }
                
            }

            return V_u;

            //czyli chcemy uzyskac w tym przypadku macierz 1x3 np [ 1, 2, 3]
        }

        private double[,] SwitchGaussColumn(int u, double[,] matrixU, double[] GaussSolution)
        {
            var ArrayAfterSwitch = new double[matrixU.GetLength(0), matrixU.GetLength(1)];
            //najpier po kolumnach
            for (var j = 0; j < matrixU.GetLength(1); j++)
            {
                for (var i = 0; i < matrixU.GetLength(0); i++)
                {

                    if (j == u) ArrayAfterSwitch[i, j] = GaussSolution[i];    /// j == u  w kolumne u wstawiamy to
                    else ArrayAfterSwitch[i, j] = matrixU[i, j];
                }
            }
        
            return ArrayAfterSwitch;
        }

        public void Test()
        {
            //TestEyeMulDouble();
            TestFloatNonZero();
            //TestColumnFromMatrix();
            //TestTransponce();
            //TestMultiplication();
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



        //zwraca indeksy kolumn, które mają wartości niezerowe w wierszu n macierzy array
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
        //zwraca indeksy wierszy, które mają wartości niezerowe w kolumnie n macierzy array
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

