using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace algorytmy2_gauss
{
    class Tests
    {
        private int dimention;
        private MyMatrix<float> matrixfloat;
        private MyMatrix<double> matrixdouble;
        private MyMatrix<Fraction> matrixfraction;

        public Tests(int dimention)
        {
            matrixfloat = new MyMatrix<float>(dimention);
            matrixdouble = new MyMatrix<double>(dimention);
            matrixfraction = new MyMatrix<Fraction>(dimention);
            this.dimention = dimention;
        }

        public void PrintMatrixF() { 
            for (int y = 0; y < dimention; y++)
            {
                for (int x = 0; x < dimention; x++)
                    Console.Write(matrixfraction.A[x, y] + "   ");
                
                 Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintMatrixFloat()
        {
            for (int y = 0; y < dimention; y++)
            {
                for (int x = 0; x < dimention; x++)
                    Console.Write(matrixfloat.A[x, y] + "   ");

                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void PrintMatrixD()
        {
            for (int y = 0; y < dimention; y++)
            {
                for (int x = 0; x < dimention; x++)
                {
                    Console.Write(matrixdouble.A[x, y] + "   ");
                }
                Console.WriteLine();

            }

        }

        //Fill all matrix with random values [-2**16; 2**16-1]
        public void FillMatrix()
        {
            Random rnd = new Random();
            int range = 65536; //2**16

            for (int y = 0; y < dimention; y++)
            {
                int rr = rnd.Next(-1 * range, range - 1);
                Fraction fra1 = new Fraction(rr, range);
                double d1 = (double)rr / (double)range;
                float f1 = (float)rr / (float)range;

                matrixfloat.SetVectorX(y, f1);
                matrixdouble.SetVectorX(y, d1);
                matrixfraction.SetVectorX(y, fra1);

                matrixfloat.SetVectorB(y, 0);
                matrixdouble.SetVectorB(y, 0);
                matrixfraction.SetVectorB(y, 0);

                for (int x = 0; x < dimention; x++)
                {
                    int r = rnd.Next(-1 * range, range - 1);
                    Fraction fra = new Fraction(r , range);
                    double d = (double)r / (double)range;
                    float f = (float)r / (float)range;
                    Console.WriteLine("to je "+f);

                    matrixfraction.SetMatrixA(x, y, fra);
                    matrixdouble.SetMatrixA(x, y, d);
                    matrixfloat.SetMatrixA(x, y, f);
                }


            }

        }
    }
}
