using HabitTracker.Services;

namespace HabitTracker.Models
{
    class Habit
    {
        private int id;
        private string habitName;
        private string habitDescription;
        //private Day habitFrequency;
        int[] habitFrequency;

        public int HabitId { get { return id; } set { id = value; } }
        public Habit(string habitName, string habitDescription, int[] habitFrequency, int id = 0)
        {
            this.id = id;
            this.habitName = habitName;
            this.habitDescription = habitDescription;
            this.habitFrequency = habitFrequency;
        }

        [Flags]
        public enum Day
        {
            NONE = 0b_0000_0000,
            SAT = 0b_0000_0001,
            SUN = 0b_0000_0010,
            MON = 0b_0000_0100,
            TUE = 0b_0000_1000,
            WED = 0b_0001_0000,
            THU = 0b_0010_0000,
            FRI = 0b_0100_0000,
            ALLWEEK = 0b_0111_1111
        }

        public static void IterateOneHabit()
        {
            foreach (var Habit in HabitManager.AllHabits)
            {
                Console.WriteLine(
                $"Habit ID = {Habit.id}" +
                $"\nHabit Name = {Habit.habitName}" +
                $"\nHabit Description = {Habit.habitDescription}" 
                );
                for (int i = 1; i < Habit.habitFrequency.Length; i++)
                {
                    System.Console.Write($"(Day)habitFrequency[i] ");
                }
                Console.WriteLine();
            }
        }

    }
}
