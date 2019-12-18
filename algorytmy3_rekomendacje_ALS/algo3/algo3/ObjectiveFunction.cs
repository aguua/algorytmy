
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo3
{
    class ObjectiveFunction
    {
        public static double CalculateFromResult (List<Result> result)
        {
            // zrob to samo  
            return 
        }
        public static double Calculate(int[,] Ratings, double[,]  U, double[,]  P, int d, double lambda)
        {
            double  S_k = 0,       // Sum of (rating – U_u.T * P_p) ^ 2
                    S_u = 0,       // Sum of U columns squared norm 
                    S_p = 0,       // Sum of P columns squared norm 
                    result;

            double[,] U_T = MyMatrix<double>.Transpose(U);
            for (int u = 0; u < U.GetLength(1); u++)  //Ratings.GetLenght(0)
            {
                for (int p = 0; p < P.GetLength(1); p++) //Ratings.GetLenght(1)
                {
                    double K = 0;
                    double PsquaredNorm = 0;
                    double UsquaredNorm = 0;
                    for (int row = 0; row < d; row++)
                    {
                        if (Ratings[u, p] != 0)
                        {
                           // Console.WriteLine($"ocena: {Ratings[u, p]}, U[row, u] = {U[row, u]}, P[row,p] = {P[row, p]}\n");
                            K += U_T[u, row] * P[row, p];
                        }
                        PsquaredNorm += Math.Pow(P[row, p], 2);
                        UsquaredNorm += Math.Pow(U_T[u, row], 2);
                    }
                    // Console.WriteLine($"K={K}, Pnorm= {PsquaredNorm}, Unorm= {UsquaredNorm}");

                    if (Ratings[u, p] != 0)  S_k += Math.Pow((Ratings[u, p] - K), 2);
                    S_p += PsquaredNorm;
                    S_u += UsquaredNorm;
                }
            }
            result = S_k + lambda * (S_u + S_p);
            Console.WriteLine("\n result\n");
            Console.WriteLine(result);
            return result;
        }
    }


}

