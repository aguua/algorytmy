/// Agnieszka Harłozińska
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

            ALS als = new ALS(100, 3, 0.1);  // (prodAmount, d, reg)

            Console.ReadLine();
            
        }
    }
}
