using Final_Project_1;

public class FinalProject1
{
    public static void Main(String[] args)
    {

        /*  How Porgrammm works?
         *  
         *  We have Cards.json file for the cards that are in the system, so if you want to 
         *  operate on cards you have to input info from Cards.json file.
         *  In case of wrong input, programm wrtites what problem is.
         *  
         *  After successfull verification you can make various operations, which are 
         *  saved into transactions.json.
         * */

        while (true)
        {
            var WholeSystem = new MainSystem();
            if (WholeSystem.ValidateCard())
            {
                WholeSystem.ShowMainMenu();
            }

            Console.WriteLine("");
            Console.WriteLine("Press 'T' to exit, or any other key to Restart...");
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && input.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
        }
    }

}




