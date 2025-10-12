using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {
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
        public static Habit.Day ChooseHabitFrequencyWeekly()
        {
            Habit.Day HabitFrequency = 0;
            Habit.Day choosenFreqDay = 0;
            int getUserChoiceFromMenu = 1;

            while (getUserChoiceFromMenu != 0)
            {
                Console.Write("Your Choice: ");
                getUserChoiceFromMenu = Convert.ToInt32(Console.ReadLine());
                if (getUserChoiceFromMenu > 8 || getUserChoiceFromMenu < 0)
                {
                    Console.WriteLine("Invalid Input, Pleasw choose from menu");
                    continue;
                }
                switch (getUserChoiceFromMenu)
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
                        getUserChoiceFromMenu = 0;
                        break;
                    default:
                        break;
                }
                HabitFrequency |= choosenFreqDay;
            }
            return HabitFrequency;
        }
        public static Habit ReadInputFromUser()
        {
            string? InputName = "Default Habit Name";
            string? InputDescription = "Default Habit Description";
            Habit.Day HabitFrequency;

            GetValidString(ref InputName, "Enter Habit Name: ");
            GetValidString(ref InputDescription, "Enter Habit Description: ");

            Console.WriteLine(
                "Choose Habit Frequency:\n" +
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
            HabitFrequency = ChooseHabitFrequencyWeekly();

            Console.WriteLine();

            return new Habit(InputName, InputDescription, HabitFrequency);

        }

        public static Habit ChooseHabitIDToDelete()
        {
            Console.WriteLine("Choose a Habit by id to delete:");
            HabitManager.ViewAllHabitsById();
            int getUserChoiceFromMenu;
            do
            {
                Console.Write("Your Choice: ");
                getUserChoiceFromMenu = Convert.ToInt32(Console.ReadLine());
            } while (!HabitManager.AllHabits.Contains(HabitManager.AllHabits[getUserChoiceFromMenu - 1]));
            return HabitManager.AllHabits[getUserChoiceFromMenu - 1];

        }
    }
}