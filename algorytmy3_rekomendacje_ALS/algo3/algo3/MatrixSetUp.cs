/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
/// 
using System;
using System.Collections.Generic;
using System.Linq;
namespace algo3
{
    class MatrixSetUp
    {
        private int p;  //products
        private int u;  //users
        private int d;  //parameter

        private Parser parser;
        public int[,] Ratings { get; set; }
        public double[,] U { get; set; }
        public double[,] P { get; set; }

        public MatrixSetUp(int productAmount, int d)   // TODO 3 rozmiary list do przeliczenia S: 10-100, M: 100:1000, B* tez? 
        {
            this.d = d;
            this.parser = new Parser(quantity: productAmount);
            List<Result> results = parser.ResultsList;

            this.u = results.Max(x => x.UserId) + 1;
            this.p = results.Max(x => x.ProductId) + 1;

            U = SetMatrix(d, u);
            P = SetMatrix(d, p);

            Ratings = SetRatingMatrix();
           // Utils<double>.PrintMatrix(U);
            //Utils<double>.PrintMatrix(P);
           // Utils<int>.PrintMatrix(Ratings);



        }
        // todo change to private
        public static double[,] SetMatrix(int row, int col)
        {
            double[,] array = new double[row, col];
            Random rnd = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    array[i, j] = (double)rnd.NextDouble();
                }
            }
            return array;
        }

        private int[,] SetRatingMatrix()
        {
            List<Result> results = parser.ResultsList;
            int[,] array = new int[u, p];

            foreach (var result in results)
            {
                try
                {
                    array[result.UserId, result.ProductId] = result.Rating;
                }

                catch
                {
                    array[result.UserId, result.ProductId] = 0;
                    throw new Exception("No rate - insert 0");
                }
            }
            return  array;

        }
    }
}