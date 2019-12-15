﻿
using System;
using System.Collections.Generic;
using System.Linq;
/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
namespace algorytmy3
{
    class MatrixSetUp
    {
        private int p;  //products
        private int u;  //users
        private int d = 3;  //parameter

        private Parser parser;
        public int[,] Ratings { get; set; }
        public double[,] U { get; set; }
        public double[,] P { get; set; }

        public MatrixSetUp(int productAmount)   // TODO 3 rozmiary list do przeliczenia S: 10-100, M: 100:1000, B* tez? 
        {
            this.p = productAmount;
            this.parser = new Parser(quantity: p);

            this.u = 10;
            U = SetMatrix(d, u);
            P = SetMatrix(d, p);
            Utils<double>.PrintMatrix(U);
            Utils<double>.PrintMatrix(P);


        }

        private double[,] SetMatrix(int row, int col)
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
            var row = results.Max(x => x.UserId) + 1;
            var col = results.Max(x => x.ProductId) + 1;
            int[,] array = new int[row, col];

            foreach (var result in results)
            {
                try
                {
                    array[result.UserId, result.ProductId] = result.Rating;
                }

                catch
                {
                    array[result.UserId, result.ProductId] = 0;
                    throw new Exception("Rating not found, inserting 0...");
                }

              

            }
            return                array;

        }
    }
}