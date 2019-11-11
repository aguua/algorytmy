using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace algorytmy2_gauss
{
    class MyTests
    {
        private int dimensions;
        private MyMatrix<float> matrixfloat;
        private MyMatrix<double> matrixdouble;
        private MyMatrix<Fraction> matrixfraction;

        private MyMatrix<float> storefloat;
        private MyMatrix<double> storedouble;
        private MyMatrix<Fraction> storefraction;

        public MyTests(int dimensions)
        {
            matrixfloat = new MyMatrix<float>(dimensions);
           //storefloat = new MyMatrix<float>(dimensions);
            matrixdouble = new MyMatrix<double>(dimensions);
            //storedouble = new MyMatrix<double>(dimensions);
            matrixfraction = new MyMatrix<Fraction>(dimensions);
           // storefraction = new MyMatrix<Fraction>(dimensions);
            this.dimensions = dimensions;
        }

        public void RunInitialTest()
        {
            matrixfloat.SetVectorB(0, 0);
            matrixfloat.SetVectorB(1, 0);
            matrixfloat.SetVectorB(2, 0);

            matrixfloat.SetVectorX(0, 2);
            matrixfloat.SetVectorX(1, -1);
            matrixfloat.SetVectorX(2, 3);

            matrixfloat.SetMatrixA(0, 0, -1);
            matrixfloat.SetMatrixA(0, 1, 1);
            matrixfloat.SetMatrixA(0, 2, 3);

            matrixfloat.SetMatrixA(1, 0, 2);
            matrixfloat.SetMatrixA(1, 1, -3);
            matrixfloat.SetMatrixA(1, 2, -1);

            matrixfloat.SetMatrixA(2, 0, 1);
            matrixfloat.SetMatrixA(2, 1, -2);
            matrixfloat.SetMatrixA(2, 2, -1);


            matrixfloat.ComputeVectorB();
            matrixfloat.PrintVectorB();
            matrixfloat.ComputeFG();
            matrixfloat.GetDiff();
            matrixfloat.PrintVectorX();
            matrixfloat.PrintVectorXgauss();
            matrixfloat.PrintMatrixA();
            storefloat.PrintMatrixA();

        }
        

        public void ComputeVectorB() 
        {
            matrixdouble.ComputeVectorB();
            matrixfloat.ComputeVectorB();
            matrixfraction.ComputeVectorB();
        }
        public void CalculateG()
        {
            matrixdouble.ComputeG();
            matrixfloat.ComputeG();
            matrixfraction.ComputeG();
        }
        public void CalculatePG()
        {
            matrixdouble.ComputePG();
            matrixfloat.ComputePG();
            matrixfraction.ComputePG();
        }

        public void CalculateFG()
        {
            matrixdouble.ComputeFG();
            matrixfloat.ComputeFG();
            matrixfraction.ComputeFG();
        }

        //Fill all matrix with random values [-2**16; 2**16-1]
        public void FillMatrixs()
        {
            Random rnd = new Random();
            int range = 65536; //2**16

            for (int y = 0; y < dimensions; y++)
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

                for (int x = 0; x < dimensions; x++)
                {
                    int r = rnd.Next(-1 * range, range - 1);
                    Fraction fra = new Fraction(r , range);
                    double d = (double)r / (double)range;
                    float f = (float)r / (float)range;

                    matrixfraction.SetMatrixA(x, y, fra);
                    matrixdouble.SetMatrixA(x, y, d);
                    matrixfloat.SetMatrixA(x, y, f);
                }


            }

        }

    }
}
