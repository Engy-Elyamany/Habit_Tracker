using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitInput
    {
        public static Habit ReadInputFromUser()
        {
            string? InputName = "Default Habit Name";
            string? InputDescription = "Default Habit Description";

            InputValidator.ValidateString(ref InputName, "Enter Habit Name: ");
            InputValidator.ValidateString(ref InputDescription, "Enter Habit Description: ");

            Console.Write("Enter Day:");
            Habit.Day freq = (Habit.Day)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            return new Habit(InputName, InputDescription, freq);

        }

    }
}