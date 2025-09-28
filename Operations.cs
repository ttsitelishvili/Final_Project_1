namespace Final_Project_1
{
    internal class Operations
    {
        #region Choice 1 : Check balance
        public static void CheckBalance(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine("=== Check Balance ===");
            Console.WriteLine($"Current Balance: {currentCard.Balance:N2} {currentCard.Currency}");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        #endregion

        #region Choice 2 : Deposit Money
        public static void DepositMoney(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine("=== Deposit Money ===");
            Console.Write("Enter amount to deposit: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                currentCard.Balance += amount;

                Transaction.CreateAndSave(
                    currentCard.CardNumber,
                    "Deposit",
                    amount,
                    currentCard.Currency,
                    currentCard.Balance,
                    "Cash deposit"
                );


               DataService.SaveCards();


                Console.WriteLine($"Successfully deposited {amount:N2} {currentCard.Currency}.");
                Console.WriteLine($"New Balance: {currentCard.Balance:N2} {currentCard.Currency}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Deposit cancelled.");
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        #endregion

        #region Choice 3 : View Transaction History
        public static void ViewTransactionHistory(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine($"=== Last 5 Transactions for Card: {currentCard.CardNumber} ===");

            var cardTransactions = DataService.Transactions
                                              .Where(t => t.CardNumber == currentCard.CardNumber)
                                              .OrderByDescending(t => t.Date)
                                              .Take(5)
                                              .ToList();

            if (cardTransactions.Any())
            {
                Console.WriteLine($"{"Date",-20} | {"Type",-20} | {"Amount",-15} | {"Balance After",-15} | Description");
                Console.WriteLine(new string('-', 90));

                foreach (var t in cardTransactions)
                {
                    string amountDisplay = t.Amount.ToString("N2") + " " + t.Currency;
                    string balanceDisplay = t.BalanceAfter.ToString("N2") + " " + currentCard.Currency;

                    Console.WriteLine($"{t.Date.ToShortDateString() + " " + t.Date.ToShortTimeString(),-20} | {t.Type,-20} | {amountDisplay,-15} | {balanceDisplay,-15} | {t.Description}");
                }
            }
            else
            {
                Console.WriteLine("No transactions found for this card.");
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
        #endregion

        #region Choice 4 : Withdraw Money
        public static void WithdrawMoney(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine("=== Withdraw Money ===");
            Console.Write("Enter amount to withdraw: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (currentCard.Balance >= amount)
                {
                    currentCard.Balance -= amount;

                    Transaction.CreateAndSave(
                        currentCard.CardNumber,
                        "Withdrawal",
                        amount,
                        currentCard.Currency,
                        currentCard.Balance,
                        "Cash withdrawal"
                    );

                   DataService.SaveCards();
                    

                    Console.WriteLine($"Successfully withdrew {amount:N2} {currentCard.Currency}.");
                    Console.WriteLine($"New Balance: {currentCard.Balance:N2} {currentCard.Currency}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds. Withdrawal cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Withdrawal cancelled.");
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
        #endregion

        #region Choice 5 : Change PIN
        public static void ChangePIN(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine("=== Change PIN ===");

            Console.Write("Enter new 4-digit PIN: ");
            string? newPin = Console.ReadLine();

            if (newPin != null && newPin.Length == 4 && newPin.All(char.IsDigit))
            {
                currentCard.PIN = newPin;

                Transaction.CreateAndSave(
                    currentCard.CardNumber,
                    "PIN Change",
                    0.0m,
                    currentCard.Currency,
                    currentCard.Balance,
                    "PIN changed successfully"
                );
            
                DataService.SaveCards();

                Console.WriteLine("PIN successfully changed.");
            }
            else
            {
                Console.WriteLine("Invalid PIN format. PIN must be a 4-digit number. PIN change cancelled.");
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
        #endregion

        #region Choice 6 : Convert Money
        public static void ConvertMoney(Card currentCard)
        {
            Console.Clear();
            Console.WriteLine("=== Convert Money ===");
            string originalCurrency = currentCard.Currency;
            decimal originalBalance = currentCard.Balance;

            Console.WriteLine($"Current Currency: {originalCurrency} | Current Balance: {originalBalance:N2}");
            Console.WriteLine("Available conversion targets (to GEL, EUR, USD, LIRA, etc.):");

            var availableCurrencies = DataService.ExchangeRates
                .Where(er => er.FromCurrency == originalCurrency)
                .Select(er => er.ToCurrency)
                .ToList();

            if (!availableCurrencies.Any())
            {
                Console.WriteLine("No available conversion rates from your current currency.");
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(string.Join(", ", availableCurrencies));
            Console.Write("Enter target currency (e.g., USD): ");
            string? targetCurrency = Console.ReadLine()?.ToUpper();


            ExchangeRate? exchangeRate = DataService.ExchangeRates.FirstOrDefault(
                er => er.FromCurrency == originalCurrency && er.ToCurrency == targetCurrency
            );

            if (exchangeRate == null)
            {
                Console.WriteLine("Invalid target currency or exchange rate not found. Conversion cancelled.");
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Exchange Rate: 1 {originalCurrency} = {exchangeRate.Rate} {targetCurrency}");
            Console.Write("Confirm conversion? (Y/N): ");
            string? confirmation = Console.ReadLine();

            if (confirmation != null && confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                decimal convertedBalance = originalBalance * exchangeRate.Rate;

                currentCard.Balance = convertedBalance;
                currentCard.Currency = targetCurrency;

                Transaction.CreateAndSave(
                    currentCard.CardNumber,
                    "Currency Conversion",
                    originalBalance,
                    originalCurrency,
                    convertedBalance,
                    $"Converted from {originalBalance:N2} {originalCurrency} to {convertedBalance:N2} {targetCurrency}"
                );

                DataService.SaveCards();

                Console.WriteLine($"\nConversion Details:");
                Console.WriteLine($"Original Balance: {originalBalance:N2} {originalCurrency}");
                Console.WriteLine($"Exchange Rate: 1 {originalCurrency} = {exchangeRate.Rate} {targetCurrency}");
                Console.WriteLine($"New Balance: {convertedBalance:N2} {targetCurrency}");
            }
            else
            {
                Console.WriteLine("Currency conversion cancelled.");
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }

        #endregion
    }
}
