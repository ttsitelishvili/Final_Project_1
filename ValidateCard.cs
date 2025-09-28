namespace Final_Project_1
{
    public class ValidateCard
    {
        private readonly List<Card> cards;

        public ValidateCard(List<Card> cards)
        {
            this.cards = cards;
        }

        public Card? Validate_Card()
        {
            Console.Clear();
            Console.WriteLine("=== Card Validation ===");

            // Get card details from user
            var cardDetails = GetCardDetailsFromUser();
            if (cardDetails == null)
                return null;

            // Find card in the system
            var card = FindCardInSystem(cardDetails);
            if (card == null)
            {
                Console.WriteLine("Card not found in system!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            // Validate PIN
            if (!ValidatePin(card))
            {
                Console.WriteLine("\nIncorrect PIN!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("\nCard validation successful!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return card;
        }

        private CardDetails? GetCardDetailsFromUser()
        {
            // Card number validation
            Console.Write("Enter card number (16 digits): ");
            string? cardNumber = Console.ReadLine();

            if (!IsValidCardNumber(cardNumber))
            {
                Console.WriteLine("Invalid card number format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            // CVC validation
            Console.Write("Enter CVC (3 digits): ");
            string? cvc = Console.ReadLine();

            if (!IsValidCVC(cvc))
            {
                Console.WriteLine("Invalid CVC format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            // Expiration date validation
            Console.Write("Enter expiration date (MM/YY): ");
            string? expirationDate = Console.ReadLine();

            if (!IsValidExpirationDate(expirationDate))
            {
                Console.WriteLine("Invalid expiration date format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            return new CardDetails
            {
                CardNumber = cardNumber,
                CVC = cvc,
                ExpirationDate = expirationDate
            };
        }

        private Card? FindCardInSystem(CardDetails cardDetails)
        {
            var foundCard = cards.FirstOrDefault(c =>
                c.CardNumber == cardDetails.CardNumber &&
                c.CVC == cardDetails.CVC &&
                c.ExpirationDate == cardDetails.ExpirationDate);

            return foundCard;
        }
        private bool ValidatePin(Card card)
        {
            Console.Write("Enter PIN (4 digits): ");
            string? pin = Console.ReadLine();
            return card.PIN == pin;
        }

        #region Methods for Validating inputs

        private bool IsValidCardNumber(string? cardNumber)
        {
            return !string.IsNullOrEmpty(cardNumber) && 
                   cardNumber.Length == 16 && 
                   cardNumber.All(char.IsDigit);
        }
        private bool IsValidCVC(string? cvc)
        {
            return !string.IsNullOrEmpty(cvc) && 
                   cvc.Length == 3 && 
                   cvc.All(char.IsDigit);
        }

        private bool IsValidExpirationDate(string? expirationDate)
        {
            if (string.IsNullOrEmpty(expirationDate))
                return false;

            var parts = expirationDate.Split('/');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid expiration date format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return false;
            }

            if (!int.TryParse(parts[0], out int month) || !int.TryParse(parts[1], out int year))
            {
                Console.WriteLine("Invalid expiration date format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return false;
            }

            if ((month < 1 || month > 12) && year <= 24)
            {
                Console.WriteLine("Invalid expiration date format!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        #endregion
    }

    public class CardDetails
    {
        public string? CardNumber { get; set; }
        public string? CVC { get; set; }
        public string? ExpirationDate { get; set; }
    }
}