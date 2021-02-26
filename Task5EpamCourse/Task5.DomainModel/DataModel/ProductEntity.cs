namespace Task5.DomainModel.DataModel
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int PurchaseId { get; set; }

        public PurchaseEntity Purchase { get; set; }
    }
}