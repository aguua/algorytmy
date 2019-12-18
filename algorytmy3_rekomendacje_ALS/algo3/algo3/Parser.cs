/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace algo3
{
    //to do :  filter for users:  take users, who reviewed min 3 products
    // add dict <int, int> user_id : number-of-reviews 

    class Parser
    {
        public List<Result> ResultsList = new List<Result>();
        private readonly Dictionary<string, int> _usersDict = new Dictionary<string, int>();  // user_name (string) : user_id (int)

        private int _nextInt = 0;   // to generate next Id number for user

        private int minReviewsAmount;

        public Parser(int quantity, int minReviewsAmount)   // how many products should be find with min amount of reviews
        {
            this.minReviewsAmount = minReviewsAmount;
            ResultsList = ParseData(quantity);
        }
        public Parser(int quantity)
            : this(quantity, 5) { }


        public List<Result> ParseData(int q)
        {
            List<Result> results = new List<Result>();
            var productsFound = new List<Product>();
            List<List<string>> dataFromFile = ReadFromFile(q);
            dataFromFile = FilterForUnique(dataFromFile);
            int id = 0;
            foreach (List<string> result in dataFromFile)
            {
                Dictionary<int, int> reviews = ProcessProductReviews(result);
                Product prod = new Product(id, reviews);
                productsFound.Add(prod);
                id += 1;
            }

            foreach (var product in productsFound)
            {
                foreach (var userId in product.Reviews.Keys)
                {
                    
                    var result = new Result(userId, product.Id, product.Reviews[userId]);
                     ResultsList.Add(result);
                }
            }

            return ResultsList;
        }

        public List<List<string>> ReadFromFile(int q)
        {
            List<List<string>> results = new List<List<string>>();
            List<string> loadedLines = null;
            string line;
            StreamReader reader = File.OpenText("amazon-meta.txt");
                while ((line = reader.ReadLine()) != null && results.Count != q)
                        {
                            if (line.Contains("Id:") && loadedLines == null)
                                loadedLines = new List<string>();
                            else if (line.Length == 0 && loadedLines != null)
                            {
                                if (CheckViability(loadedLines))
                                    results.Add(loadedLines);
                                loadedLines = null;
                            }

                            if (loadedLines != null)
                                loadedLines.Add(line);
                        }
            

            return results;
        }

        private bool CheckViability(List<string> product)
        {
            if (product.Contains("  group: Book"))
            {
                int reviewCount = product.Count - product.FindIndex(x => x.Contains("reviews")) - 1;
                return reviewCount >= this.minReviewsAmount;
            }

            return false;
        }

        private List<List<string>> FilterForUnique(List<List<string>> list)
        {
            return list.GroupBy(x => x.First()).Select(x => x.Last()).ToList();
        }

        private Dictionary<int, int> ProcessProductReviews(List<string> data)   //data for one product
        {
            Dictionary<string, int> reviewsBare = new Dictionary<string, int>();

            foreach (string line in data)
            {
                if (line.Contains("cutomer: "))
                {
                    List<string> list = new List<string>();
                    string[] rate_line = line.Split();
                    foreach (string i in rate_line)
                    {
                        if (!i.Equals("")) { list.Add(i); }
                    }

                    string userName = list[2];
                    int rate = int.Parse(list[4]);
                    try
                    {
                        reviewsBare.Add(userName, rate);
                    }

                    catch (Exception) // Key already in dictionary (user has previously reviewed the product)
                    {
                        reviewsBare.Remove(userName);
                        reviewsBare.Add(userName, rate);
                    }

                }
            }
            // Add user to dictionary and set Id for each user 
            Dictionary<int, int> reviewsConverted = new Dictionary<int, int>();

            foreach (var userName in reviewsBare.Keys)
            {

                    if (_usersDict.Keys.Contains(userName))
                    {
                        reviewsConverted.Add(_usersDict[userName], reviewsBare[userName]);
                    }
                    else
                    {
                        _usersDict.Add(userName, _nextInt);
                        reviewsConverted.Add(_nextInt, reviewsBare[userName]);
                        _nextInt += 1;
                    }
            }

            return reviewsConverted;
        }
    }
}
