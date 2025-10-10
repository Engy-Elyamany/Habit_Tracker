namespace HabitTracker.Utilities
{
    class InputValidator
    {
        static bool IsContainNullOrWhiteSpace(string? str) => string.IsNullOrWhiteSpace(str);

        static bool IsContainDigitsOrChar(string str)
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
                    return true;
                }
            }
            return false;
        }

        public static void ValidateString(ref string? str, string printStatement)
        {
            do
            {
                Console.Write(printStatement);
                str = Console.ReadLine();
                if (IsContainNullOrWhiteSpace(str) || IsContainDigitsOrChar(str))
                {
                    Console.WriteLine("Please Enter a valid Input");
                }
            } while (IsContainNullOrWhiteSpace(str) || IsContainDigitsOrChar(str));
        }
    }
}