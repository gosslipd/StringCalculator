using System.Reflection.Metadata.Ecma335;

namespace StringCalculatorNS
{
    public class StringCalculator
    {
        /// @brief Method to sum a list of integers.
        /// @param numbers The numbers to add, optionally preceded by a list of delimiter characters.
        /// @return Sum of integers or 0 if none passed.
        public int Add(string numbers)
        {
            int result = 0;

            if(numbers.Length == 0)
                return 0;

            // Extract the delimiter string from before the list of numbers - creates a default list of delimiters if none provided.
            ParseAddInputString(ref numbers, out string delimitersString, out string numbersString);

            char[] delimiterChars = delimitersString.ToCharArray();
            string[] valueStrings = numbersString.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // Handle case: "1,\n", i.e. two delimiters with no value between them
            string[] valueRawStrings = numbersString.Split(delimiterChars);
            if (valueStrings.Length != valueRawStrings.Length)
            {
                Console.WriteLine("Error: list contains extra delimiters");
                return 0;
            }

            List<int> values = new List<int>();
            List<int> negativeValues = new List<int>(); // Receives negative numbers if we find any

            const int MaximumAddValue = 1000;
            const int MinimumAddValue = 0;

            // Convert string values into integers if possible
            foreach ( string valstr in valueStrings)
            {
                if (int.TryParse(valstr, out int value))
                {
                    if (value < MinimumAddValue)
                    {
                        negativeValues.Add(value);
                    }
                    else if (value <= MaximumAddValue)
                    {
                        values.Add(value);
                    }
                    // else ignore number
                }
                else
                {
                    // Parsing failed - could throw another exception here
                    continue;
                }
            }

            if( negativeValues.Count > 0 )
            {
                // Throw exception
                string negativeNumbers = string.Join(",", negativeValues);
                throw new ArgumentException("Negatives not allowed: " + negativeNumbers);
            }
            else
            {
                result = values.Sum();
            }

            return result;
        }

        /// @brief Method to extract the delimiters and numbers list from the Add input string.
        /// @param numbers The list of delimiters and numbers passed to Add().
        /// @param delimitersString Returns all delimiters as a string.
        /// @param numbersString Returns the list of numbers to add.
        /// @return None.
        private void ParseAddInputString(ref string numbers, out string delimitersString, out string numbersString)
        {
            delimitersString = "";
            numbersString = "";

            if (numbers.StartsWith(@"//"))
            {
                // Split the list of delimiters from the list of numbers
                string[] lines = numbers.Substring(2).Split("\n", StringSplitOptions.RemoveEmptyEntries);
                if (lines.Count() == 2)
                {
                    delimitersString = lines[0];
                    numbersString = lines[1];
                }
            }
            else
            {
                // No delimieter list specified so create one to work with
                delimitersString = ",\n";
                numbersString = numbers;
            }

            // Convert to lower case for case insensitive search
            delimitersString = delimitersString.ToLower();
            numbersString = numbersString.ToLower();
        }

        static void Main(string[] args)
        {
            StringCalculator calculator = new StringCalculator();

            int result = 0;

            // See unit tests for other scenarios, but test catching of exception here
            try
            {
                result = calculator.Add("1,2,-3,4,-5,-6");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Exception caught: {0}: {1}", e.GetType().Name, e.Message);
            }

            Console.WriteLine(result);
        }
    }
}
