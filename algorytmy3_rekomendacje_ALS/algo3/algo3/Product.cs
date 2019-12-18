/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System.Collections.Generic;


namespace algo3
{
    class Product
    {
        public int Id;
        public Dictionary<int, int> Reviews = new Dictionary<int, int>(); // user_id(int): rate(int)
        public Product(int id, Dictionary<int,int> ratings)
        {
            this.Id = id;
            this.Reviews = ratings;
        }

        public override string ToString()
        {
            string s = "\nProduct Id: " + Id + "\nReviews: " + Reviews.Count + "\n";
            foreach (KeyValuePair<int, int> r in Reviews)
            {
                s += "customer: " + r.Key + " | ";
                s += "rating: " + r.Value + "\n";
            }

            return s;
        }



    }

}
