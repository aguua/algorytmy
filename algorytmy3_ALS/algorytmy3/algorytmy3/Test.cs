/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System;

using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;

namespace algorytmy3
{
    class Test
    {
        private int p;  //products
        private int u;  //users
        private int d;  //parameter
        private double lam = 0.1;  //lamdba
        Matrix<double> P;
        Matrix<double> U;

        public Test() :this(3,10) { }   // domyślnie 3 uzytkownikow i 10 produktow
        public Test(int u, int p) : this(u, p, 3) { }
        public Test(int u = 1, int p = 2, int d =3 )
        {
            this.u = u;
            this.p = p;
            this.d = d;
        }

        private double GetRandom()
        {
            Random rnd = new Random();
            return rnd.NextDouble();
        }
        private void SetPandU()
        {
            Random rnd = new Random();
            P = Matrix.Build.RandomPositiveDefinite(d, p);   //nie sa z zakrsu -0-1
            U = Matrix.Build.RandomPositiveDefinite(d, u);

           // P = Matrix.Build.Dense(d, p, GetRandom());
           // U = Matrix.Build.Dense(d, u, GetRandom());



        }
        
        

        public void Run()
        {

            SetPandU();

            Console.WriteLine(U);
            Console.WriteLine(P);


            Console.Read();
        }
    }
}
