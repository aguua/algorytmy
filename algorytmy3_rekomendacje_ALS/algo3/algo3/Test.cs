/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji
using System;


namespace algo3
{
    class Test
    {
        int usersCount;
        int prodCount;

        ALS Als;
       
        public void Run()
        {
            RunALS(50,20);          //small
            //RunALS(150,100 );       //medium
           // RunALS(1000, 500);      //big
        }

        public void RunALS(int usersCount, int prodCount)
        {
            
            var lambdaSet = new double[] { 0.1, 0.3, 0.5, 0.7, 1.0 };
            var dimensionSet = new int[] { 3, 5, 7, 11, 15, 20 };

            foreach (var reg in lambdaSet)
            {
                foreach (var d in dimensionSet)
                {
                    Als = new ALS(prodCount, usersCount, d, reg);
                    //licz czas trwania 
                }
            }
        }
    }
}
