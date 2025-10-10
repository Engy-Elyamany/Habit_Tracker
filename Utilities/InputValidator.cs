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
                    return true;
                }
            }
            return false;
        }
    }
}