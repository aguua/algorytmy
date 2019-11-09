using System;
using System.IO;
using System.Text;

namespace algorytmy2_gauss
{
    class Program
    {

        public static T GenerycznaMetoda<T>(T parametr)
        {
            Console.WriteLine("Metoda z podaniem typu parametru: " + parametr + parametr.GetType());
            return parametr;
            // Convert a Double to a Continent.
            //double number = 6.0;
            
            
                //Console.WriteLine("{0}",
                                  //Convert.ChangeType(number, typeof(T)));
            
        }



        static void Main(string[] args)
        {
 
            Fraction f1 = new Fraction(2, 3);
            Fraction f2 = new Fraction(1, 4);

            var stringi1 = GenerycznaMetoda(f2);
            Console.WriteLine("Metoda bez podania typu parametru: " + stringi1);

            Tests newTest = new Tests(4);
            newTest.FillMatrix();
            newTest.PrintMatrixFloat();
            newTest.PrintMatrixF();
            newTest.PrintMatrixD();

            MyMatrix<float> macierzfloat = new MyMatrix<float>(2);
            //macierzfloat.FillMatrix();
            Console.ReadLine();



        }
    }
}
