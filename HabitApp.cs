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
                userChoice = HabitInput.GetValidUserChoiceFromMenu("Choose from Main Menu", 0, 8);
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
        private int ChooseHabitByID()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose a Habit by id:");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int UserChoiceFromMenu))
                {
                    var check = manager.HabitExistinList(UserChoiceFromMenu);
                    if (check == HabitManager.OperationStatus.INVALID_INPUT)
                    {
                        Console.WriteLine("Invalid ID! The intended habit doesn't exist");
                        continue;
                    }
                    return UserChoiceFromMenu;
                }
                else
                {
                    Console.WriteLine("Invalid Input, please enter only digits");
                    return (int)HabitManager.OperationStatus.INVALID_INPUT;
                }

            }
        }
        private void CreateHabit()
        {
            Console.WriteLine("========= Create Habit =========");
            var newHabit = HabitInput.ReadHabitFromUser();
            HabitManager.OperationStatus status = manager.CreateHabit(newHabit);
            if (status == HabitManager.OperationStatus.NULL_VALUE)
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

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            HabitManager.OperationStatus status = manager.DeleteHabit(desiredHabit);

            if (status == HabitManager.OperationStatus.SUCCESS)
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

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];


            //print Edit Menu
            HabitOutput.PrintEditMenu();


            int userChoice = 1;
            string? newHabitName = "";
            string? newHabitDescription = "";
            Habit.Day newHabitFrequency = 0;
            while (userChoice != 0)
            {
                //user's choice from edit menu
                userChoice = HabitInput.GetValidUserChoiceFromMenu("Your Choice to edit", 0, 3);

                //take new edited habit from user
                switch (userChoice)
                {
                    case 1:
                        newHabitName = HabitInput.GetValidString("Enter The new Habit Name");
                        break;
                    case 2:
                        newHabitDescription = HabitInput.GetValidString("Enter The new Habit Description");
                        break;
                    case 3:
                        newHabitFrequency = HabitInput.GetHabitFrequencyWeekly("Enter The new Habit Frequency");
                        break;
                    default:
                        userChoice = 0;
                        break;
                }
            }

            //delegate to manager to perform logic
            HabitManager.OperationStatus status = manager.EditHabit(desiredHabitId, newHabitName,newHabitDescription,newHabitFrequency);

            //Feedback to user
            if (status == HabitManager.OperationStatus.SUCCESS)
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

            Console.WriteLine($"========= Today's Habits =========");
            List<Habit> TodayHabitList = manager.TodayHabits(manager.AllHabits);
            if (TodayHabitList.Count == 0)
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

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            HabitManager.OperationStatus status = manager.MarkHabitAsDone(desiredHabit);
            if (status == HabitManager.OperationStatus.SUCCESS)
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

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            HabitManager.OperationStatus status = manager.UndoMarkedHabit(desiredHabit);
            if (status == HabitManager.OperationStatus.SUCCESS)
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