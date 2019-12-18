/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System;
using System.Collections.Generic;

namespace algo3
{
    public class Utils<T>
    {
        public static void PrintMatrix(T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }

        public static void PrintVector(T[] vector)
        {
            for (int j = 0; j < vector.GetLength(0); j++)
            {
                Console.Write(vector[j] + "   ");
            }
            Console.WriteLine();
        }
    }
}
