/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 2
/// Rozwiązywanie układów równań liniowych metodą eliminacji Gaussa
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

            StringBuilder header = new StringBuilder();
            header.AppendLine("Time[ms];;;;;;;;;Difference");
            header.AppendLine("Float;;;Double;;;Fraction;;;Float;;;Double;;;Fraction;;");
            header.AppendLine("G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG;G;PG;FG");
            File.WriteAllText("test.csv", header.ToString());

            for (int i = 5; i<= 201; i += 5)
            {
                StringBuilder content = new StringBuilder();
                Test test = new Test(i);

                content.AppendLine(test.Run());
                File.AppendAllText("test.csv", content.ToString());


            }


            Console.WriteLine("Done");
            Console.ReadLine();

        }
    }
}
