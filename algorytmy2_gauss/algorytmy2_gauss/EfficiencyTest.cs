using System;
using System.Diagnostics;

namespace algorytmy2_gauss
{
    public class EfficiencyTest
    {
        private int dimensions;
        private double[] time;
        private double[] diff;

        public EfficiencyTest(int dim)
        {
            this.dimensions = dim;
            time = new double[9];
            diff = new double[9];
        }

        public String Run()
        {
            Console.WriteLine("Test: " + dimensions);
           // RunFloat(GaussType.Basic);
           // RunFloat(GaussType.Part);
            //RunFloat(GaussType.Full);

            //Console.WriteLine("Double");
           // RunDouble(GaussType.Basic);
           // RunDouble(GaussType.Part);
           // RunDouble(GaussType.Full);

           // Console.WriteLine("Fraction");
            RunFraction(GaussType.Basic);
            RunFraction(GaussType.Part);
            RunFraction(GaussType.Full);

            return String.Join(";", time) + ";" + String.Join(";", diff);
        }


        public void RunFloat(GaussType gausstype)
        {
            MyMatrix<float> matrix = new MyMatrix<float>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            for (int y = 0; y < dimensions; y++)
            {
                matrix.SetVectorB(y, 0);
                int rr = rnd.Next(-1 * range, range - 1);
                float f1 = (float)rr / (float)range;
                matrix.SetVectorX(y, f1);
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
                    //Console.WriteLine("  bez wyboru elementu podstawowego");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[0] = sw.Elapsed.TotalMilliseconds;
                    diff[0] = matrix.GetDiff();
                    break;
                case GaussType.Part:
                    //Console.WriteLine("  z czesciowym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[1] = sw.Elapsed.TotalMilliseconds;
                    diff[1] = matrix.GetDiff();
                    break;
                case GaussType.Full:
                    //Console.WriteLine("  z pelnym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[2] = sw.Elapsed.TotalMilliseconds;
                    diff[2] = matrix.GetDiff();
                    break;
            }

            sw.Stop();
            //Console.WriteLine("    Czas: {0}", sw.Elapsed.TotalMilliseconds);
            //Console.WriteLine("    błąd: " + matrix.CalculateDiff());
        }
        public void RunDouble(GaussType gausstype)
        {
            MyMatrix<double> matrix = new MyMatrix<double>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            int rr = rnd.Next(-1 * range, range - 1);
            for (int y = 0; y < dimensions; y++)
            {
                matrix.SetVectorB(y, 0);
                double d1 = (double)rr / (double)range;
                matrix.SetVectorX(y, d1);
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
                    //Console.WriteLine("  bez wyboru elementu podstawowego");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[3] = sw.Elapsed.TotalMilliseconds;
                    diff[3] = matrix.GetDiff();
                    break;
                case GaussType.Part:
                    //Console.WriteLine("  z czesciowym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[4] = sw.Elapsed.TotalMilliseconds;
                    diff[4] = matrix.GetDiff();
                    break;
                case GaussType.Full:
                    //Console.WriteLine("  z pelnym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[5] = sw.Elapsed.TotalMilliseconds;
                    diff[5] = matrix.GetDiff();
                    break;
            }
            //Console.WriteLine("    Czas: {0}", sw.Elapsed);
            //Console.WriteLine("    błąd: " + matrix.CalculateDiff());
        }

        public void RunFraction(GaussType gausstype)
        {
            MyMatrix<Fraction> matrix = new MyMatrix<Fraction>(dimensions);
            Random rnd = new Random();
            int range = 65536; //2**16
            for (int y = 0; y < dimensions; y++)
            {
                matrix.SetVectorB(y, 0);
                int rr = rnd.Next(-1 * range, range - 1);
                Fraction fra = new Fraction(rr, range);
                matrix.SetVectorX(y, fra);
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
                    //Console.WriteLine("  bez wyboru elementu podstawowego");
                    sw.Start();
                    matrix.ComputeG();
                    sw.Stop();
                    time[6] = sw.Elapsed.TotalMilliseconds;
                    diff[6] = matrix.GetDiff().ToDouble();
                    break;
                case GaussType.Part:
                    //Console.WriteLine("  z czesciowym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputePG();
                    sw.Stop();
                    time[7] = sw.Elapsed.TotalMilliseconds;
                    diff[7] = matrix.GetDiff().ToDouble();
                    break;
                case GaussType.Full:
                    //Console.WriteLine("  z pelnym wyborem elementu podstawowego");
                    sw.Start();
                    matrix.ComputeFG();
                    sw.Stop();
                    time[8] = sw.Elapsed.TotalMilliseconds;
                    diff[8] = matrix.GetDiff().ToDouble();
                    break;
            }
            //Console.WriteLine("    Czas: {0}", sw.Elapsed);
            //Console.WriteLine("    błąd: " + matrix.CalculateDiff());
        }



    }
}
