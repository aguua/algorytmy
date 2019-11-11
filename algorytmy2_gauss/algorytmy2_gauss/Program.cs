using System;
using System.IO;
using System.Text;

namespace algorytmy2_gauss
{
    public enum GaussType { Basic, Part, Full };

    class Program
    {


        static void Main(string[] args)
        {

            //MyTests newTest = new MyTests(3);
            //newTest.RunForDouble();
            //newTest.RunForF();
            //newTest.RunInitialTest();

            /* for (int i = 5; i< 500 ; i ++)
             {
                 Tests newTest = new Tests(i);
                 newTest.RunForDouble();

             }
             */
            StringBuilder header = new StringBuilder();
            header.AppendLine("Time;;;;;;;;;Difference");
            header.AppendLine("Float;;;Double;;;Fraction;;;Float;;;Double;;;Fraction;;");
            header.AppendLine("G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG");
            File.WriteAllText("test.csv", header.ToString());

            for (int i = 130; i< 501; i += 5)
            {
                StringBuilder content = new StringBuilder();
                EfficiencyTest etest = new EfficiencyTest(i);
                content.AppendLine(etest.Run());
                File.AppendAllText("test.csv", content.ToString());
            }

            Console.WriteLine("Ready!");
            Console.ReadLine();

        }
    }
}
