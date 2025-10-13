using HabitTracker.Models;
using HabitTracker.UI;
using HabitTracker.Utilities;

namespace HabitTracker.Services
{
    class HabitManager
    {

        public static readonly List<Habit> AllHabits = new List<Habit>();
        private static int idCounter = 0;
        public static void CreateHabit()
        {
            idCounter++;
            Habit newHabit = HabitInput.ReadHabitFromUser();
            newHabit.Id = idCounter;
            AllHabits.Add(newHabit);
        }
        public static void DeleteHabit()
        {
            Console.WriteLine("========= Delete Habit =========");
            Habit desiredHabitToDelete = HabitInput.ChooseHabitByID();
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
            for (int i = 0; i < AllHabits.Count; i++)
            {
                AllHabits[i].Id = i + 1;
            }
        }
        public static void EditHabit()
        {
            Console.WriteLine("========= Edit Habit =========");
            Habit desiredHabit = HabitInput.ChooseHabitByID();
            HabitInput.EditHabitUI();


            int userChoice = 1;
            string? newHabitName = " new Name";
            string? newHabitDescription = " new desc";
            Habit.Day newHabitFrequency = 0;

            while (userChoice != 0)
            {
                if (!InputValidator.GetValidUserChoiceFromMenu(ref userChoice, "Your Choice to edit", 0, 3))
                    continue;
                switch (userChoice)
                {
                    case 1:
                        HabitInput.GetValidString(ref newHabitName, "Enter The new Habit Name: ");
                        desiredHabit.Name = newHabitName;
                        break;
                    case 2:
                        HabitInput.GetValidString(ref newHabitDescription, "Enter The new Habit Description: ");
                        desiredHabit.Description = newHabitDescription;
                        break;
                    case 3:
                        HabitInput.GetHabitFrequencyWeekly(ref newHabitFrequency, "Enter The new Habit Frequency");
                        desiredHabit.Frequency = newHabitFrequency;
                        break;

                    default:
                        userChoice = 0;
                        break;
                }
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
            foreach (var habit in AllHabits)
            {
                Console.WriteLine(
               $"ID = {habit.Id} : " +
                $"Name = {habit.Name}");

                Console.WriteLine();
            }
        }

        public static void ViewHabitDetails(Habit habit)
        {
            Console.WriteLine(
                $"ID = {habit.Id} : " +
                $"Name = {habit.Name}");
                Console.WriteLine();
        }
        public static void ViewTodayHabits()
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            System.Console.WriteLine($"========= {today}'s Habits =========");
            bool habitExistToday = false;
            foreach (var habit in AllHabits)
            {
                bool isTheHabitToday = Convert.ToBoolean(((int)habit.Frequency >> (int)today) & 1);
                if (isTheHabitToday)
                {
                    habitExistToday = true;
                    ViewHabitDetails(habit);
                }
            }
            if (!habitExistToday)
                System.Console.WriteLine("No habits Today");

        }
    }
}
