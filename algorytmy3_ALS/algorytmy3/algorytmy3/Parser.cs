using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmy3
{
    class Parser
    {
        public List<Product> products = new List<Product>();
        int q;  //quantity of product to parse from file 


        public List<Result> ResultsList = new List<Result>();
        private readonly Dictionary<string, int> _customerIds = new Dictionary<string, int>();
        private int _nextInt = 0;

        public Parser(int q)
        {
            ResultsList = ParseInitialData(q);
        }

     

        public List<List<string>> ReadFromFile(int q) {

            List<List<string>> results = new List<List<string>>();
            List<string> current = null;

            StreamReader reader = File.OpenText("test.txt");
            string line;
            while ((line = reader.ReadLine()) != null && results.Count != q)
            {
                if (line.Contains("Id:"))
                {
                    CreateProduct(reader, line);

                }
            }
            return results;
        }
        private void CreateProduct(StreamReader r, string line)
        {
           
            Console.Write("mam id");
            string[] id_line = line.Split();
            int id = int.Parse(id_line[3]);
            Console.WriteLine($" id = {id}");

            Product p = new Product(id);
            products.Add(p);

            string line_in;
                while ((line_in = r.ReadLine()) != null)
                {
                    if (line_in.Contains("cutomer: "))
                    {
                    List<string> list = new List<string>();
                    string[] rate_line = line_in.Split();
                        foreach (string i in rate_line)
                    {
                        if (!i.Equals(""))
                        {
                            list.Add(i);
                        }
                    }

                    string userName = list[2];
                    int rate = int.Parse(list[4]); 
                    Console.WriteLine($"user = {userName}, rate  = {rate}");
                    p.AddRate(userName, rate);
                    }
                    if (line_in.Contains("Id:"))
                    {
                    //break;
                    CreateProduct(r, line_in);
                    }
                }

            
        }
        public void ReadData()
        {
            StreamReader reader = File.OpenText("test.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if(line.Contains("Id:"))
                {
                    CreateProduct(reader, line);
                    
                }
            }
        }

    }
}
