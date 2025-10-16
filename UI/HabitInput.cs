using HabitTracker.Models;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {

        //returns valid choice from Menu
        public static int GetValidUserChoiceFromMenu(string printStatement, int validationRangeStart, int validationRangeEnd)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(printStatement + ": ");
            Console.ResetColor();

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int Choice))
                {
                    if (Choice > validationRangeEnd || Choice < validationRangeStart)
                    {
                        Console.WriteLine("Invalid Input, out of range, Please choose from menu ");
                        continue;
                    }
                    return Choice;
                }
                else
                {
                    Console.WriteLine("Invalid Input, please enter only digits");
                }
            }


        }
        public static string GetValidString(string printStatement)
        {
            string? str;
            bool notValidString;

            while (true)
            {
                Console.Write(printStatement + ": ");
                str = Console.ReadLine();
                if(str == null)
                {
                    System.Console.WriteLine("This input can't be empty");
                    continue;
                }
                notValidString = InputValidator.IsContainNullOrWhiteSpace(str) || InputValidator.IsContainDigitsOrChar(str);
                if (notValidString)
                {
                    Console.WriteLine("Please Enter a valid Input");
                    continue;
                }
                return str;

            }
        }
        public static Habit.Day GetHabitFrequencyWeekly(string printStatement)
        {
            Habit.Day HabitFrequency = 0;
            Habit.Day choosenFreqDay = 0;
            int userDayChoice = 1;

            //Dynamic print statement to use it in Create and Edit for better UI
            Console.WriteLine(printStatement + "\n");

            //print Days of the week menu to choose from
            HabitOutput.PrintDaysMenu();

            //loop to accept multiple days
            while (userDayChoice != 0)
            {
                userDayChoice = GetValidUserChoiceFromMenu("Your Day Choice", 0, 8);
                switch (userDayChoice)
                {
                    case 1:
                        choosenFreqDay = Habit.Day.SAT;
                        break;
                    case 2:
                        choosenFreqDay = Habit.Day.SUN;
                        break;
                    case 3:
                        choosenFreqDay = Habit.Day.MON;
                        break;
                    case 4:
                        choosenFreqDay = Habit.Day.TUE;
                        break;
                    case 5:
                        choosenFreqDay = Habit.Day.WED;
                        break;
                    case 6:
                        choosenFreqDay = Habit.Day.THU;
                        break;
                    case 7:
                        choosenFreqDay = Habit.Day.FRI;
                        break;
                    case 8:
                        choosenFreqDay = Habit.Day.ALLWEEK;
                        userDayChoice = 0;
                        break;
                    default:
                        break;
                }

                HabitFrequency |= choosenFreqDay;

            }
            return HabitFrequency;

        }
        public static Habit? ReadHabitFromUser()
        {
            string? InputName;
            string? InputDescription;
            Habit.Day HabitFrequency;

            InputName = GetValidString("Enter Habit Name") ?? "Default Name";
            InputDescription = GetValidString("Enter Habit Description") ?? "Default Description";
            HabitFrequency = GetHabitFrequencyWeekly("Choose Your Habit Frequency");

            Console.WriteLine();

            return new Habit(InputName, InputDescription, HabitFrequency);

        }
    }
}