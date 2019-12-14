using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmy3
{
    class Product
    {
        int Id;
        Dictionary<int, int> Reviews = new Dictionary<int, int>(); // user_id: rate
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
