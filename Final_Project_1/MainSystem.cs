using System;

namespace Final_Project_1
{
    public class MainSystem
    {
        private Card? currentCard;
        private readonly DataService dataService;
        public static DateTime SessionStart { get; private set; }

        public MainSystem()
        {
            dataService = new DataService();
            dataService.LoadData();
            SessionStart = DateTime.Now;
        }

        public bool ValidateCard()
        {
            var cardValidator = new ValidateCard(DataService.Cards);
            currentCard = cardValidator.Validate_Card();
            return currentCard != null;
        }

        public void ShowMainMenu()
        {
            if (currentCard == null)
            {
                Console.WriteLine("No card validated. Cannot show menu.");
                return;
            }

            var menuService = new MenuService(currentCard);
            menuService.ShowMainMenu();
        }

    }
}
