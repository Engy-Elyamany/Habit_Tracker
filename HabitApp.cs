using System.Runtime.InteropServices;
using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.UI;
using HabitTracker.Utilities;

namespace HabitTracker
{
    class HabitApp
    {
        HabitManager manager = new HabitManager();
        public void Run()
        {
            int userChoice = 1;

            Console.WriteLine(
                "Choose From Menu" +
                "\n1.Add Habit" +
                "\n2.View All Habits" +
                "\n3.View Today's Habits" +
                "\n4.Mark Habits as Done" +
                "\n5.Undo a habit completion" +
                "\n6.Edit Habit" +
                "\n7.Delete Habit" +
                "\nTo Exit press 0" +
                "\n"
            );

            while(userChoice != 0)
            {
                if (!HabitInput.GetValidUserChoiceFromMenu(ref userChoice, "Choose from Main Menu", 0, 7))
                {
                    continue;
                }
                switch (userChoice)
                {
                    case 1:
                        CreateHabit();
                        break;
                    
                    case 2:
                        HabitOutput.ViewAllHabits(manager.AllHabits);
                        break;
                    
                    case 3:
                        HabitOutput.ViewTodayHabits(manager.AllHabits);
                        break;
                    
                    case 4:
                        MarkHabitAsDone();
                        break;
                    
                    case 5:
                        UndoMarkedHabit();
                        break;
                    
                    case 6:
                        EditHabit();
                        break;
                    case 7:
                        DeleteHabit();
                        break;

                    default:
                        userChoice = 0;
                        break;
                }
            }
        }
        private void CreateHabit()
        {
            Console.WriteLine("========= Create Habit =========");
            var newHabit = HabitInput.ReadHabitFromUser();
            manager.CreateHabit(newHabit);
        }
        private void DeleteHabit()
        {
            Console.WriteLine("========= Delete Habit =========");
            var desiredHabit = manager.ChooseHabitByID();
            manager.DeleteHabit(desiredHabit);
        }
        private void EditHabit()
        {
            Console.WriteLine("========= Edit Habit =========");
            var desiredHabit = manager.ChooseHabitByID();
            HabitInput.EditHabitUI();

            manager.DeleteHabit(desiredHabit);
        }
        private void MarkHabitAsDone()
        {
            Console.WriteLine("========= Mark Habits =========");
            HabitOutput.ViewTodayHabits(manager.AllHabits);
            var desiredHabit = manager.ChooseHabitByID();
            manager.MarkHabitAsDone(desiredHabit);   
        }
        private void UndoMarkedHabit()
        {
            Console.WriteLine("========= Undo compeletion =========");
            HabitOutput.ViewAllHabits(manager.AllHabits);
            var desiredHabit = manager.ChooseHabitByID();
            manager.UndoMarkedHabit(desiredHabit);
        }
        
    }
}