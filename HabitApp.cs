using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.UI;

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
                "\n8.Destroy Habit list" +
                "\nTo Exit press 0" +
                "\n"
            );

            while (userChoice != 0)
            {
                if (!HabitInput.GetValidUserChoiceFromMenu(ref userChoice, "Choose from Main Menu", 0, 8))
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
                    case 8:
                        ClearList();
                        break;

                    default:
                        userChoice = 0;
                        break;
                }
            }
        }

        private Habit ChooseHabitByID()
        {
            Console.WriteLine("Choose a Habit by id:");
            int getUserChoiceFromMenu;
            Habit desiredHabit = new();
            getUserChoiceFromMenu = Convert.ToInt32(Console.ReadLine());

            if (!manager.HabitExistinList(getUserChoiceFromMenu, ref desiredHabit))
            {
                Console.WriteLine("Invalid ID! The intended habit doesn't exist");
                ChooseHabitByID();
            }
            return desiredHabit;
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
            var desiredHabit = ChooseHabitByID();
            manager.DeleteHabit(desiredHabit);
        }
        private void EditHabit()
        {
            Console.WriteLine("========= Edit Habit =========");
            var desiredHabit = ChooseHabitByID();
            Console.WriteLine(
              "1.Edit Habit Name" +
              "\n2.Edit Habit Description" +
              "\n3.Edit Habit Frequency" +
              "\nTo Exit press 0"
           );

            manager.DeleteHabit(desiredHabit);
        }
        private void MarkHabitAsDone()
        {
            Console.WriteLine("========= Mark Habits =========");
            HabitOutput.ViewTodayHabits(manager.AllHabits);
            var desiredHabit = ChooseHabitByID();
            manager.MarkHabitAsDone(desiredHabit);
        }
        private void UndoMarkedHabit()
        {
            Console.WriteLine("========= Undo compeletion =========");
            HabitOutput.ViewAllHabits(manager.AllHabits);
            var desiredHabit = ChooseHabitByID();
            manager.UndoMarkedHabit(desiredHabit);
        }

        private void ClearList()
        {
            manager.ClearList(manager.AllHabits);
        }
    }
}