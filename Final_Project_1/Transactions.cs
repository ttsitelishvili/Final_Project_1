namespace Final_Project_1
{
    public class Transaction
    {
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } 
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal BalanceAfter { get; set; }
        public string Description { get; set; }

        public Transaction()
        {
            Date = DateTime.Now;
        }

        // Method to save the transaction
        public void SaveToJson()
        {
            DataService.Transactions.Add(this);
            DataService.SaveTransactions();
        }

        public static Transaction CreateAndSave(string cardNumber, string type, decimal amount, string currency, decimal balanceAfter, string description)
        {
            var transaction = new Transaction
            {
                CardNumber = cardNumber,
                Date = DateTime.Now,
                Type = type,
                Amount = amount,
                Currency = currency,
                BalanceAfter = balanceAfter,
                Description = description
            };
            
            transaction.SaveToJson();
            
            return transaction;
        }
    }
}
