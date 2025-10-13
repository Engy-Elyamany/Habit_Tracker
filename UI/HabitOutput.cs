using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Utilities;

namespace HabitTracker.UI
{
    class HabitOutput
    {
               public static void ViewAllHabits()
        {
            foreach (var Habit in HabitManager.AllHabits)
            {
                string habitStatusString = Habit.MarkedAsDone ? "Done" : "Not Done";
                Console.WriteLine(
                $"Habit ID = {Habit.Id}" +
                $"\nHabit Name = {Habit.Name}" +
                $"\nHabit Description = {Habit.Description}" +
                $"\nHabit Frequency = {Habit.Frequency}" +
                $"\nHabit Status = {habitStatusString}");
                Console.WriteLine();
            }
        }
        private static void ViewHabitsID(Habit habit)
        {
            string habitStatusString = habit.MarkedAsDone ? "Done" : "Not Done";
            Console.WriteLine(
                $"ID = {habit.Id} , " +
                $"Name = {habit.Name} , "+
                $"Status = {habitStatusString}\n"
                );
        }
        public static void ViewTodayHabits()
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            Console.WriteLine($"========= {today}'s Habits =========");
            bool habitExistToday = false;
            foreach (var habit in HabitManager.AllHabits)
            {
                bool isTheHabitToday = Convert.ToBoolean(((int)habit.Frequency >> (int)today) & 1);
                if (isTheHabitToday)
                {
                    habitExistToday = true;
                    ViewHabitsID(habit);
                }
            }
            if (!habitExistToday)
                Console.WriteLine("No habits Today");

        }
    
    }
}