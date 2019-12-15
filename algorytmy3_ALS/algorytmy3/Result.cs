/// Agnieszka Harłozińska
/// Algorytmy Numeryczne
/// Zadanie 3
/// Metoda ALS w systemach rekomendacji

namespace algorytmy3
{
    public class Result
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }

        public Result(int userId, int productId, int rating)
        {
            UserId = userId;
            ProductId = productId;
            Rating = rating;
        }
    }
}
