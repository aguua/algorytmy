using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo3
{
    class ObjectiveFunction
    {
        public static double Calculate(double[,] Ratings, double[,]  U, double[,]  P, int d, double lambda)
        {
            double  S_k = 0,       // Sum of (rating – U_u.T * P_p) ^ 2
                    S_u = 0,       // Sum of U columns squared norm 
                    S_p = 0,       // Sum of P columns squared norm 
                    result;
            for (int u = 0; u < U.GetLength(1); u++)
            {
                for (int p = 0; p < P.GetLength(1); p++)
                {
                    double K = 0;
                    for (int row = 0; row < d; row++)
                    {
                        K += U[row, u] * P[row, p];
                    }
                    S_k += Math.Pow((Ratings[u, p] - K), 2);

                    double PsquaredNorm = 0;
                    for (int row = 0; row < d; row++)
                    {
                        PsquaredNorm += Math.Pow(P[row, p], 2);
                    }
                    S_u += PsquaredNorm;
                }

                double UsquaredNorm = 0;
                for (int row = 0; row < d; row++)
                {
                    UsquaredNorm += Math.Pow(U[row, u], 2);
                }
                S_p += UsquaredNorm;
            }
            result = S_k + lambda * (S_u + S_p);
            Console.WriteLine(result);
            return result;
        }
    }


}

