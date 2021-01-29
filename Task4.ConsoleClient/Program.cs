using Task4.DAL.Repositories.Context;
using Task4.DomainModel.DataModel;

namespace Task4.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PurchaseContext())
            {
                var product = new ProductEntity() {ProductName = "cheese", ProductId = 1};

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
