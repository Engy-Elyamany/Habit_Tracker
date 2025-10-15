using System.ComponentModel.Design;
using System.Net.NetworkInformation;
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


            while (userChoice != 0)
            {
                HabitOutput.PrintMainMenu();
                if (!HabitInput.GetValidUserChoiceFromMenu(ref userChoice, "Choose from Main Menu", 0, 8))
                {
                    HabitOutput.PrintMainMenu();
                    continue;
                }
                switch (userChoice)
                {
                    case 1:
                        CreateHabit();
                        break;

                    case 2:
                        HabitOutput.ViewHabits(manager.AllHabits);
                        break;

                    case 3:
                        ViewTodayHabits();
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
            int getUserChoiceFromMenu;
            Habit desiredHabit = new();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose a Habit by id:");
                Console.ResetColor();
                
                getUserChoiceFromMenu = Convert.ToInt32(Console.ReadLine());
                var check = manager.HabitExistinList(getUserChoiceFromMenu, ref desiredHabit);
                if (check == HabitManager.operationStatus.INVALID_INPUT)
                {
                    Console.WriteLine("Invalid ID! The intended habit doesn't exist");
                    continue;
                }
                return desiredHabit;
            }
        }

        private void CreateHabit()
        {
            Console.WriteLine("========= Create Habit =========");
            var newHabit = HabitInput.ReadHabitFromUser();
            HabitManager.operationStatus status = manager.CreateHabit(newHabit);
            if (status == HabitManager.operationStatus.NULL_VALUE)
            {
                Console.WriteLine("Habit Creation Failed! Please Try again");
            }
            else
            {
                Console.WriteLine("Habit Added Successfuly");
            }
        }
        private void DeleteHabit()
        {
            Console.WriteLine("========= Delete Habit =========");
            HabitOutput.ViewHabits(manager.AllHabits);
            var desiredHabit = ChooseHabitByID();
            HabitManager.operationStatus status = manager.DeleteHabit(desiredHabit);
            if (status == HabitManager.operationStatus.SUCCESS)
            {
                Console.WriteLine("Habit Deleted Successfuly");
            }
            else
            {
                Console.WriteLine("Deletion Failed");
            }

        }
        private void EditHabit()
        {
            Console.WriteLine("========= Edit Habit =========");
            HabitOutput.ViewHabits(manager.AllHabits);
            var desiredHabit = ChooseHabitByID();
            HabitOutput.PrintEditMenu();

            HabitManager.operationStatus status = manager.EditHabit(desiredHabit);
            if (status == HabitManager.operationStatus.SUCCESS)
            {
                Console.WriteLine("Habit Edited Successfuly");
            }
            else
            {
                Console.WriteLine("Editing Failed");
            }

        }

        private void ViewTodayHabits()
        {
            List<Habit>? TodayHabitList = HabitManager.TodayHabits(manager.AllHabits);

            if (TodayHabitList == null)
            {
                Console.WriteLine("No Habits for Today");
                return;
            }

            HabitOutput.ViewHabits(TodayHabitList);
        }
        private void MarkHabitAsDone()
        {
            Console.WriteLine("========= Mark Habits =========");

            ViewTodayHabits();

            var desiredHabit = ChooseHabitByID();
            HabitManager.operationStatus status = HabitManager.MarkHabitAsDone(desiredHabit);
            if (status == HabitManager.operationStatus.SUCCESS)
            {
                Console.WriteLine("Habit Marked As Done");
            }
            else
            {
                Console.WriteLine("Operation Aborted! Habit is already marked as done");
            }

        }
        private void UndoMarkedHabit()
        {
            Console.WriteLine("========= Undo compeletion =========");
            //HabitOutput.ViewHabits(manager.AllHabits);
            var desiredHabit = ChooseHabitByID();
            HabitManager.operationStatus status = HabitManager.UndoMarkedHabit(desiredHabit);
            if (status == HabitManager.operationStatus.SUCCESS)
            {
                Console.WriteLine("Compeletion Undone");
            }
            else
            {
                Console.WriteLine("Habit is not yet marked");
            }

        }

        private void ClearList()
        {
            manager.ClearList(manager.AllHabits);
        }
    }
}