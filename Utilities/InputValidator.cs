namespace HabitTracker.Utilities
{
    class InputValidator
    {
        public static bool IsContainNullOrWhiteSpace(string? str) => string.IsNullOrWhiteSpace(str);
        public static bool IsContainDigitsOrChar(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetterOrDigit(str[i]))
                {
                    if (char.IsDigit(str[i]))
                        return true;
                }
                else
                {
                    if (char.IsWhiteSpace(str[i]))
                        return false;
                    return true;
                }
            }
            return false;
        }
        public static bool GetValidUserChoiceFromMenu(ref int Choice, string printStatement, int validationRangeStart, int validationRangeEnd)
        {
            bool validChoice = true;
            Console.Write(printStatement + ": ");
            Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice > validationRangeEnd || Choice < validationRangeStart)
            {
                Console.WriteLine("Invalid Input, Please choose from menu ");
                validChoice = false;
            }
            return validChoice;
        }
    }
}