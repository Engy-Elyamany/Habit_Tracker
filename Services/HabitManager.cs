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
            newHabit.Id = idCounter;
            AllHabits.Add(newHabit);
            return newHabit;
        }
        public static void DeleteHabit()
        {
            Habit desiredHabitToDelete = HabitInput.ChooseHabitIDToDelete();
            if (AllHabits.Remove(desiredHabitToDelete))
            {
                idCounter--;
                Console.WriteLine("Habit Removed Successfully");
            }
            else
            {
                Console.WriteLine("Couldn't Remove This Habit,Please Try again");
            }

            //sync all IDs with the new order after deletion
            //TODO : Could be improved a bit more 
            for (int i = 0; i<AllHabits.Count; i++)
            {
                AllHabits[i].Id = i + 1;
            }
        }
        public static void ViewAllHabits()
        {
            foreach (var Habit in AllHabits)
            {
                Console.WriteLine(
                $"Habit ID = {Habit.Id}" +
                $"\nHabit Name = {Habit.Name}" +
                $"\nHabit Description = {Habit.Description}" +
                $"\nHabit Frequency = {Habit.Frequency}");
                Console.WriteLine();
            }
        }
        public static void ViewAllHabitsById()
        {
            foreach (var Habit in AllHabits)
            {
                Console.WriteLine(
                $"Habit Name = {Habit.Name}" +
                $"\nHabit ID = {Habit.Id}"
                );
              
                Console.WriteLine();
            }
        }
    }
}
