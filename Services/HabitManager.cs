using HabitTracker.Models;
using HabitTracker.UI;
using HabitTracker.Utilities;

namespace HabitTracker.Services
{
    class HabitManager
    {

        public static readonly List<Habit> AllHabits = new List<Habit>();
        private static int idCounter = 0;


        public static Habit CreateHabit()
        {
            idCounter++;
            Habit newHabit = HabitInput.ReadInputFromUser();
            newHabit.HabitId = idCounter;
            AllHabits.Add(newHabit);
            return newHabit;
        }
        public static void ViewAllHabits()
        {
            Habit.IterateOneHabit();
        }
    }
}
