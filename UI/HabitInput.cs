using HabitTracker.Models;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {

        //returns valid choice from Menu 
        //that is within the provided range and is a valid Int input
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
                //statement to prompt the user for the input
                Console.Write(printStatement + ": ");

                str = Console.ReadLine();
                if (str == null)
                {
                    Console.WriteLine("This input can't be empty");
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

            //Dynamic print statement to prompt the user for the input
            Console.WriteLine(printStatement + "\n");

            //print Days of the week menu to choose from
            HabitOutput.PrintDaysMenu();

            //loop to accept multiple days
            while (userDayChoice != 0)
            {
                int dayMenuStartChoice = 1;
                int dayMenuEndChoice = 8;
                userDayChoice = GetValidUserChoiceFromMenu("Your Day Choice", dayMenuStartChoice - 1, dayMenuEndChoice);
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

                // add the choice to the Frequency of the Habit
                // we used '|' because Habit.Day is a flag enum (binary) 
                HabitFrequency |= choosenFreqDay;

            }
            return HabitFrequency;

        }
        public static Habit ReadHabitFromUser()
        {
            string? InputName;
            string? InputDescription;
            Habit.Day HabitFrequency;

            //get data from user
            InputName = GetValidString("Enter Habit Name");
            InputDescription = GetValidString("Enter Habit Description");
            HabitFrequency = GetHabitFrequencyWeekly("Choose Your Habit Frequency");

            Console.WriteLine();

            //return a habit with the new data
            return new Habit(InputName, InputDescription, HabitFrequency);

        }
    }
}