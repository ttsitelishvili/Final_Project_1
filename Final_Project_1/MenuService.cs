namespace Final_Project_1
{
      public class MenuService
    {
        private readonly Card _currentCard;

        public MenuService(Card currentCard)
        {
            _currentCard = currentCard ?? throw new ArgumentNullException(nameof(currentCard));
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                // Display Main menu
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine($"Welcome to our amazing banking system...");
                Console.WriteLine();

                //Display Meny options
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. View Last 5 Transactions");
                Console.WriteLine("4. Withdraw Money");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Convert Money");
                Console.WriteLine("7. Exit");
                Console.WriteLine();

                //Get user Choice
                string? choice = GetUserChoice();
                
                if (!ProcessMenuChoice(choice))
                    break;
            }
        }

       
        private string? GetUserChoice()
        {
            Console.Write("Select an option (1-7): ");
            return Console.ReadLine();
        }


        private bool ProcessMenuChoice(string? choice)
        {
            switch (choice)
            {
                case "1":
                    Operations.CheckBalance(_currentCard);
                    return true;
                case "2":
                    Operations.DepositMoney(_currentCard);
                    return true;
                case "3":
                    Operations.ViewTransactionHistory(_currentCard);
                    return true;
                case "4":
                    Operations.WithdrawMoney(_currentCard);
                    return true;
                case "5":
                    Operations.ChangePIN(_currentCard);
                    return true;
                case "6":
                    Operations.ConvertMoney(_currentCard);
                    return true;
                case "7":
                    DisplayExitMessage();
                    return false;
                default:
                    DisplayInvalidOptionMessage();
                    return true;
            }
        }


        private void DisplayExitMessage()
        {
            Console.WriteLine("Thank you for using our Banking System. Goodbye!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


        private void DisplayInvalidOptionMessage()
        {
            Console.WriteLine("Invalid option. Press any key to continue...");
            Console.ReadKey();
        }
    }
}
