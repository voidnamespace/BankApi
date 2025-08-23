namespace BankApi.Entities
{
    public class BankCard
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        
        public Guid OwnerId { get; set; }
        public Client Owner { get; set; } = null!;

        public ICollection<Transaction> TransactionsFrom { get; set; } = new List<Transaction>();
        public ICollection<Transaction> TransactionsTo { get; set; } = new List<Transaction>();

    }
}
