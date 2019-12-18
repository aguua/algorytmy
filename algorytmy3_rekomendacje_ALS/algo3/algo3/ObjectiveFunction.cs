using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo3
{
    class ObjectiveFunction
    {
        public static double Calculate(int[,] Ratings, double[,] U, double[,] P, int d, double lambda)
        {
            double S_k = 0,       // Sum of (rating – U_u.T * P_p) ^ 2
                    S_u = 0,       // Sum of U columns squared norm 
                    S_p = 0,       // Sum of P columns squared norm 
                    result;
            double[,] U_T = MyMatrix<double>.Transpose(U);
            double[,] ratingsCalculated = MyMatrix<double>.Multiplication(U_T, P);
            Console.WriteLine($" rozmiary\n" +
                $"ratings : {Ratings.GetLength(0)} x {Ratings.GetLength(1)}\n" +
                $"p {P.GetLength(0)} x {P.GetLength(1)}\n" +
                $"u {U.GetLength(0)} x {U.GetLength(1)}\n");
            for (int u = 0; u < U.GetLength(1); u++) //Ratings.GetLength(0)
            {
                for (int p = 0; p < P.GetLength(1); p++)
                {
                    if (Ratings[u, p] != 0)
                    {
                        S_k += Math.Pow((Ratings[u, p] - ratingsCalculated[u, p]), 2);
                    }
                }
            }

            for (int j = 0; j < P.GetLength(1); j++)
            {
                for (int i = 0; i < P.GetLength(0); i++)
                {
                    S_p += Math.Pow(P[i, j], 2);
                }
            }

            for (int j = 0; j < U.GetLength(1); j++)
            {
                for (int i = 0; i < U.GetLength(0); i++)
                {
                    S_u += Math.Pow(U[i, j], 2);
                }
            }
            result = S_k + lambda * (S_u + S_p);
            Console.WriteLine($"\n {result}");
            return result;
        }
    }



}