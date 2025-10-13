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
        public static void MarkHabitAsDone()
        {
            Console.WriteLine("========= Mark Habits =========");
            HabitOutput.ViewTodayHabits();
            Habit desiredHabit = HabitInput.ChooseHabitByID();
            if (!desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = true;
                Console.WriteLine($"Habit No.{desiredHabit.Id} completed successfuly");
            }

            else
                Console.WriteLine("Habit already completed for today");
        }
        public static void UndoMarkedHabit()
        {
            Console.WriteLine("========= Undo compeletion =========");
            HabitOutput.ViewAllHabits();
            Habit desiredHabit = HabitInput.ChooseHabitByID();
            if (desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = false;
                Console.WriteLine("Compeletion Undone");
            }

            else
                Console.WriteLine("Habit is already not marked");
        }
    }
}
