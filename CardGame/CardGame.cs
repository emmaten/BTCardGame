using System.Text.RegularExpressions;

namespace CardGame
{
    public static class CardGame
    {
        // Exit command to exit the program.
        private const string EXIT_COMMAND = "EXIT";

        // Regexes to validate input.
        private static Regex ValidCardListFormat = new Regex(@"^[0-9A-Z]{2}(?:\s*,\s*[0-9A-Z]{2})*$", RegexOptions.IgnoreCase);
        private static Regex ValidCardFormat = new Regex(@"^(?:[2-9TJQKA][SCDH])|JK$", RegexOptions.IgnoreCase);

        // Dictionary containing card values.
        private static Dictionary<string, int> CardValues = new Dictionary<string, int>(){
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "T", 10 },
            { "J", 11 },
            { "Q", 12 },
            { "K", 13 },
            { "A", 14 },
            { "C", 1 },
            { "D", 2 },
            { "H", 3 },
            { "S", 4 }
        };

        /// <summary>
        /// Main method - entry point of the application.
        /// </summary>
        /// <param name="args">Args.</param>
        static void Main(string[] args)
        {
            try
            {
                // Show the informational message to the user.
                Console.WriteLine("Welcome to the BT card game! To play, please enter a list of cards in the following format:\n" +
                    "AS, 1C, 2D, 3H...\n" +
                    "where the first character is the value of the card (2-9 for 2-9, T for 10, J for Jack, Q for Queen, K for King, A for Ace) " +
                    "and the second character is the suit (S for Spades, C for Clubs, D for Diamonds, H for Hearts). JK is a Joker.\n" +
                    "When you've entered your list of cards, I'll tell you the final score!\n" +
                    "To exit this game, please type the word EXIT (not case sensitive).\n" +
                    "Are you ready? Let's start!");
                Console.WriteLine("Please type a list of cards and then press Enter.");

                // Wait for user input (their list of cards). Convert to uppercase for ease.
                string input = Console.ReadLine().ToUpper();

                // Run the program until the user exits.
                while (input != EXIT_COMMAND)
                {
                    // User has inputted a list of cards. Validate the input. Returns error.
                    string error = ValidateInput(input);
                    if(!string.IsNullOrEmpty(error))
                    {
                        // Error with input - display error.
                        Console.WriteLine($"There is an error with your input: {error}");
                    }
                    else
                    {
                        // No error with input - calculate the score.
                        int score = CalculateScore(input);
                        Console.WriteLine($"Your final score is {score.ToString()}!");
                    }

                    // Prompt user for next input.
                    Console.WriteLine("Please type a list of cards and then press Enter.");

                    // Read user's input. Convert to uppercase for ease.
                    input = Console.ReadLine().ToUpper();
                }
            }
            catch (Exception ex)
            {
                // Write the exception to the console.
                ReportException(ex);
            }            
        }

        /// <summary>
        /// Validates the user input and returns an error. If no error then empty string returned.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <returns>Error message or empty string.</returns>
        public static string ValidateInput(string input)
        {
            try
            {
                // Check card list string is in a valid format.
                if(!ValidCardListFormat.IsMatch(input))
                {
                    // Error.
                    return "Invalid input string";
                }

                List<string> cardList = GetCardListFromUserInput(input);

                // Check each card is valid.
                foreach(string card in cardList)
                {
                    if (!ValidCardFormat.IsMatch(card))
                    {
                        // Error.
                        return "Card not recognised";
                    }
                }

                // Are there more than 2 Jokers?
                if (cardList.Count(c => c == "JK") > 2)
                {
                    return "A hand cannot contain more than two Jokers";
                }

                // Remove Jokers before checking for duplicates.
                if(cardList.Contains("JK"))
                {
                    cardList.RemoveAll(c => c == "JK");
                }

                // Are there any duplicates? 
                if (cardList.Distinct().Count() != cardList.Count())
                {
                    return "Cards cannot be duplicated";
                }               
            }
            catch(Exception ex)
            {
                // Write the exception to the console.
                ReportException(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// Calculates the score of the user's cards.
        /// </summary>
        /// <param name="input">User input string.</param>
        /// <returns>Integer final score.</returns>
        public static int CalculateScore(string input)
        {
            // Keep track of score.
            int score = 0;
            try
            {
                // Get list of cards.
                List<string> cardList = GetCardListFromUserInput(input);

                // Keep track of the number of Jokers.
                int jokerCount = 0;

                // Loop through each card.
                foreach(string card in cardList)
                {
                    if(card == "JK")
                    {
                        // JK is Joker which doubles the final score.
                        jokerCount++;
                    }
                    else
                    {
                        // Get the card's value and add to the final score.
                        string cardValue = card.Substring(0, 1);
                        string cardMultiplier = card.Substring(1, 1);
                        int cardScore = CardValues[cardValue] * CardValues[cardMultiplier];
                        score += cardScore;
                    }
                }

                // Update score if Jokers present.
                if(jokerCount > 0)
                {
                    score = score * 2 * jokerCount;
                }
            }
            catch (Exception ex)
            {
                // Write the exception to the console.
                ReportException(ex);
            }
            // Return score.
            return score;
        }

        /// <summary>
        /// Returns user input as a List of strings.
        /// </summary>
        /// <param name="input">User input string.</param>
        /// <returns>List of strings.</returns>
        private static List<string> GetCardListFromUserInput(string input)
        {
            try
            {
                // Cards are separated by commas. Trim each entry.
                return input.Split(',', StringSplitOptions.TrimEntries).ToList<string>();
            }
            catch (Exception ex)
            {
                // Write the exception to the console.
                ReportException(ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// Report an exception to the console. Ideally developer would be notified and not the user.
        /// </summary>
        /// <param name="ex">Exception.</param>
        private static void ReportException(Exception ex)
        {
            Console.WriteLine($"Oh no - an exception has occurred! Here's the details of the exception:\n{ex.Message}");
        }
    }
}