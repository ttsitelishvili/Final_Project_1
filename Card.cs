
namespace Final_Project_1
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDate { get; set; }
        public string PIN { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }


        public Card(string cardNumber, string cvc, string expirationDate)
        {
            CardNumber = cardNumber;
            CVC = cvc;
            ExpirationDate = expirationDate;
            PIN = "";
            Balance = 0;
            Currency = "";
        }

        public override string ToString()
        {
            return $"CardNumber: {CardNumber}, CVC: {CVC}, ExpirationDate: {ExpirationDate}, PIN: {PIN}, Balance: {Balance}, Currency: {Currency}";
        }
    }

}