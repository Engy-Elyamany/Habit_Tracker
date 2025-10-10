using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {
        public static int[] ChooseHabitFrequencyWeekly()
        {
            System.Console.Write("How many days of the week? ");
            int daysNumber = Convert.ToInt32(Console.ReadLine());
            int[] HabitFreqChoice = { (int)Habit.Day.NONE };
            foreach (Habit.Day day in Enum.GetValues(typeof(Habit.Day)))
            {
                Console.WriteLine($"{day} : {(int)day}");
            }

            for (int i = 1; i <= daysNumber; i++)
            {
                HabitFreqChoice[i] = Convert.ToInt32(Console.ReadLine());
            }

            return HabitFreqChoice;

        }
        public static Habit ReadInputFromUser()
        {
            string? InputName = "Default Habit Name";
            string? InputDescription = "Default Habit Description";

            InputValidator.ValidateString(ref InputName, "Enter Habit Name: ");
            InputValidator.ValidateString(ref InputDescription, "Enter Habit Description: ");

            Console.Write("Choose Habit Frequency:");
            int[] HabitFrequency = ChooseHabitFrequencyWeekly();

            Console.WriteLine();

            return new Habit(InputName, InputDescription, HabitFrequency);

        }

    }
}