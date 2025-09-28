using Newtonsoft.Json;

namespace Final_Project_1
{
    public class DataService
    {
        private readonly string projectRoot;
        private readonly string cardsFile;
        private readonly string transactionsFile;
        private readonly string exchangeRatesFile;

        public DataService()
        {
            projectRoot = Directory.GetCurrentDirectory();

           string projectSourceDir;

          /// Without this if-else, I was not able to write to the transactions.json
            if (projectRoot.Contains("bin\\Debug") || projectRoot.Contains("bin/Debug"))
            {
                projectSourceDir = Path.Combine(projectRoot, "..", "..", "..");
            }
            else
            {
                projectSourceDir = projectRoot;
            }
            
            projectSourceDir = Path.GetFullPath(projectSourceDir);
            
            // Set all file paths to be in the project source directory
            cardsFile = Path.Combine(projectSourceDir, "Cards.json");
            transactionsFile = Path.Combine(projectSourceDir, "transactions.json");
            exchangeRatesFile = Path.Combine(projectSourceDir, "exchange_rates.json");
        }

        public static List<Card> Cards { get; private set; } = new List<Card>();
        public static List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public static List<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();

        public void LoadData()
        {
            try
            {
                // Load Cards
                if (File.Exists(cardsFile))
                {
                    string cardsJson = File.ReadAllText(cardsFile);
                    Cards = JsonConvert.DeserializeObject<List<Card>>(cardsJson) ?? new List<Card>();
                }
                else
                {
                    Console.WriteLine("Cards.json not found. Starting with empty card list.");
                    Cards = new List<Card>();
                }

                // Load Exchange Rates
                if (File.Exists(exchangeRatesFile))
                {
                    string exchangeRatesJson = File.ReadAllText(exchangeRatesFile);
                    ExchangeRates = JsonConvert.DeserializeObject<List<ExchangeRate>>(exchangeRatesJson) ?? new List<ExchangeRate>();
                }
                else
                {
                    Console.WriteLine("exchange_rates.json not found.");
                }

                // Load Transactions
                if (File.Exists(transactionsFile))
                {
                    string transactionsJson = File.ReadAllText(transactionsFile);
                    Transactions = JsonConvert.DeserializeObject<List<Transaction>>(transactionsJson) ?? new List<Transaction>();
                }
                else
                {
                    Console.WriteLine("transactions.json not found. Starting with empty transaction list and creating file.");
                    Transactions = new List<Transaction>();
                    SaveTransactions();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading data: {ex.Message}");
                Cards = new List<Card>();
                Transactions = new List<Transaction>();
                ExchangeRates = new List<ExchangeRate>();
            }
        }

        public static void SaveCards()
        {
            var dataService = new DataService();
            string cardsJson = JsonConvert.SerializeObject(Cards, Formatting.Indented);
            File.WriteAllText(dataService.cardsFile, cardsJson);
        }

        public static void SaveTransactions()
        {
            var dataService = new DataService();
            var transactionsJson = JsonConvert.SerializeObject(Transactions, Formatting.Indented);
            File.WriteAllText(dataService.transactionsFile, transactionsJson);
        }

    }
}