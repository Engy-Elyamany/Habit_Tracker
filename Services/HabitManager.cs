using HabitTracker.Models;

namespace HabitTracker.Services
{
    class HabitManager
    {

        static readonly List<Habit> AllHabits = new List<Habit>();
        private static int idCounter = 0;
        public static Habit CreateHabit(string name, string desc, Habit.Day freq)
        {
            idCounter++;
            Habit newHabit = new Habit(idCounter, name, desc, freq);
            AllHabits.Add(newHabit);
            return newHabit;
        }
        public static void ViewAllHabits()
        {
            foreach (var Habit in AllHabits)
            {
                Console.WriteLine(
                $"Habit ID = {Habit.id}" +
                $"\nHabit Name = {Habit.habitName}" +
                $"\nHabit Description = {Habit.habitDescription}" +
                $"\nHabit Frequency = {Habit.habitFrequency}"
                );
                Console.WriteLine();
            }
        }
    }
}
