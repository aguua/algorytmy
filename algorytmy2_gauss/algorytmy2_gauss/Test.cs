/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 2
/// Rozwiązywanie układów równań liniowych metodą eliminacji Gaussa
using System;
using System.Diagnostics;  

namespace algorytmy2_gauss
{
    public class Test
    {
        private int dimensions;
        private double[] time;
        private double[] diff;

        public Test(int dim)
        {
            this.dimensions = dim;
            time = new double[9];
            diff = new double[9];
        }

        public String Run()
        {
           Console.WriteLine("Start test for: " + dimensions);
            
            RunFloat(GaussType.Basic);
            RunFloat(GaussType.Part);
            RunFloat(GaussType.Full);

            Console.Write(" GP ");
            RunDouble(GaussType.Basic);
            RunDouble(GaussType.Part);
            RunDouble(GaussType.Full);
            
           // Console.Write(" GF ");
           // RunFraction(GaussType.Basic);
            //RunFraction(GaussType.Part);
           // RunFraction(GaussType.Full);

            return String.Join(";", time) + ";" + String.Join(";", diff);
        }


        public void RunFloat(GaussType gausstype)
        {
            MyMatrix<float> matrix = new MyMatrix<float>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            for (int y = 0; y < dimensions; y++)
            {
                int rr = rnd.Next(-1 * range, range - 1);
                float f1 = (float)rr / (float)range;
                matrix.SetVectorX(y, f1);
                matrix.SetVectorB(y, 0);
                for (int x = 0; x < dimensions; x++)
                {
                    rr = rnd.Next(-1 * range, range - 1);
                    f1 = (float)rr / (float)range;
                    matrix.SetMatrixA(x, y, f1);
                }
            }
            matrix.ComputeVectorB();

            Stopwatch sw = new Stopwatch();

            switch (gausstype)
            {
                case GaussType.Basic:
                    //Console.WriteLine(" G");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[0] = sw.Elapsed.TotalMilliseconds;
                    diff[0] = matrix.GetDiff();
                    break;
                case GaussType.Part:
                    //Console.WriteLine(" PG");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[1] = sw.Elapsed.TotalMilliseconds;
                    diff[1] = matrix.GetDiff();
                    break;
                case GaussType.Full:
                    //Console.WriteLine(" FG");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[2] = sw.Elapsed.TotalMilliseconds;
                    diff[2] = matrix.GetDiff();
                    break;
            }
            sw.Stop();
        }

        public void RunDouble(GaussType gausstype)
        {
            MyMatrix<double> matrix = new MyMatrix<double>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            
            for (int y = 0; y < dimensions; y++)
            {
                int rr = rnd.Next(-1 * range, range - 1);
                double d1 = (double)rr / (double)range;
                matrix.SetVectorX(y, d1);
                matrix.SetVectorB(y, 0);
                for (int x = 0; x < dimensions; x++)
                {
                    rr = rnd.Next(-1 * range, range - 1);
                    d1 = (double)rr / (double)range;
                    matrix.SetMatrixA(x, y, d1);
                }
            }
            matrix.ComputeVectorB();

            Stopwatch sw = new Stopwatch();

            switch (gausstype)
            {
                case GaussType.Basic:
                    //Console.WriteLine("  G");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[3] = sw.Elapsed.TotalMilliseconds;
                    diff[3] = matrix.GetDiff();
                    break;
                case GaussType.Part:
                    //Console.WriteLine("  PG");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[4] = sw.Elapsed.TotalMilliseconds;
                    diff[4] = matrix.GetDiff();
                    break;
                case GaussType.Full:
                    //Console.WriteLine("  FG");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[5] = sw.Elapsed.TotalMilliseconds;
                    diff[5] = matrix.GetDiff();
                    break;
            }
        }

        public void RunFraction(GaussType gausstype)
        {
            MyMatrix<Fraction> matrix = new MyMatrix<Fraction>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            for (int y = 0; y < dimensions; y++)
            {
                int rr = rnd.Next(-1 * range, range - 1);
                Fraction fra = new Fraction(rr, range);
                matrix.SetVectorX(y, fra);
                matrix.SetVectorB(y, 0);
                for (int x = 0; x < dimensions; x++)
                {
                    rr = rnd.Next(-1 * range, range - 1);
                    fra = new Fraction(rr, range);
                    matrix.SetMatrixA(x, y, fra);
                }
            }
            matrix.ComputeVectorB();

            Stopwatch sw = new Stopwatch();

            switch (gausstype)
            {
                case GaussType.Basic:
                    //Console.WriteLine("  G");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[6] = sw.Elapsed.TotalMilliseconds;
                    diff[6] = matrix.GetDiff().ToDouble();
                    break;
                case GaussType.Part:
                    //Console.WriteLine("  PG");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[7] = sw.Elapsed.TotalMilliseconds;
                    diff[7] = matrix.GetDiff().ToDouble();
                    break;
                case GaussType.Full:
                    //Console.WriteLine("  z FG");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[8] = sw.Elapsed.TotalMilliseconds;
                    diff[8] = matrix.GetDiff().ToDouble();
                    break;
            }
        }



    }
}
