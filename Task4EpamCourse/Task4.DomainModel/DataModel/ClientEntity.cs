namespace Task4.DomainModel.DataModel
{
    public class ClientEntity
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public int PurchaseId { get; set; }

        public PurchaseEntity Purchase { get; set; }
    }
}
