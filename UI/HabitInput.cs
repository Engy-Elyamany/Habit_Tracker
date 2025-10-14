using HabitTracker.Models;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {

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
        public static void GetValidString(ref string? str, string printStatement)
        {
            do
            {
                Console.Write(printStatement);
                str = Console.ReadLine();
                if (InputValidator.IsContainNullOrWhiteSpace(str) || InputValidator.IsContainDigitsOrChar(str))
                {
                    Console.WriteLine("Please Enter a valid Input");
                }
            } while (InputValidator.IsContainNullOrWhiteSpace(str) || InputValidator.IsContainDigitsOrChar(str));
        }
        public static void GetHabitFrequencyWeekly(ref Habit.Day HabitFrequency, string printStatement)
        {
            Console.WriteLine(printStatement);
            //Habit.Day HabitFrequency = 0;
            Habit.Day choosenFreqDay = 0;
            int userChoice = 1;

            while (userChoice != 0)
            {
                if (!GetValidUserChoiceFromMenu(ref userChoice, "Your Day Choice", 0, 8))
                    continue;
                switch (userChoice)
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
                        userChoice = 0;
                        break;
                    default:
                        break;
                }
                HabitFrequency |= choosenFreqDay;
            }
        }
        public static Habit ReadHabitFromUser()
        {
            string? InputName = "Default Habit Name";
            string? InputDescription = "Default Habit Description";
            Habit.Day HabitFrequency = 0;

            GetValidString(ref InputName, "Enter Habit Name: ");
            GetValidString(ref InputDescription, "Enter Habit Description: ");

            Console.WriteLine(
                "1.Saturday\n" +
                "2.Sunday\n" +
                "3.Monday\n" +
                "4.Tuesday\n" +
                "5.Wednesday\n" +
                "6.Thursday\n" +
                "7.Friday\n" +
                "8.All Week\n" +
                "Press 0 When Done Choosing"
            );
            GetHabitFrequencyWeekly(ref HabitFrequency, "Choose Your Habit Frequency");

            Console.WriteLine();

            return new Habit(InputName, InputDescription, HabitFrequency) ?? null;

        }


    }
}