﻿/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System;


namespace algo3
{
    class Program
    {
        static void Main(string[] args)
        {
            // MatrixSetUp setUP = new MatrixSetUp(10);

            ALS als = new ALS();
           // als.Test();

         /*   Parser parser = new Parser(40);
          Console.WriteLine(parser.ResultsList.Count);
          foreach(Result res in parser.ResultsList)
           {

                 Console.WriteLine($" Produkt: {res.ProductId}, user: {res.UserId}, rate: {res.Rating}"); 



            }
           
    */
            Console.ReadLine();
            
        }
    }
}