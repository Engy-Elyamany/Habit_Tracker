using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.UI;

namespace HabitTracker
{
    //connector class that glues UI and Business logic together
    class HabitApp
    {
        // an instance from HabitManger to access its logic
        HabitManager manager = new HabitManager();

        //entry point
        public void Run()
        {
            int userChoice = 1;

            while (userChoice != 0)
            {
                HabitOutput.PrintMainMenu();

                int mainMenuStartChoice = 1;
                int mainMenuEndChoice = 8;
                userChoice = HabitInput.GetValidUserChoiceFromMenu("Choose from Main Menu", mainMenuStartChoice - 1, mainMenuEndChoice);
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

        //returns a valid id that is within the range of the list as well as correct int input
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

            //get the newHabit's data from user
            var newHabit = HabitInput.ReadHabitFromUser();

            //delegate to HabitManager to perform the logic
            HabitManager.OperationStatus status = manager.CreateHabit(newHabit);

            //feedback to user
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

            //view all habits to choose from
            HabitOutput.ViewHabits(manager.AllHabits);

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            //delegate to HabitManager to perform the logic
            HabitManager.OperationStatus status = manager.DeleteHabit(desiredHabit);

            //feedback to user
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

            //view all habits to choose from
            HabitOutput.ViewHabits(manager.AllHabits);

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];


            //view Edit Menu
            HabitOutput.PrintEditMenu();


            int userChoice = 1;
            int editMenuStartChoice = 1;
            int editMenuEndChoice = 3;

            string? newHabitName = "";
            string? newHabitDescription = "";
            Habit.Day newHabitFrequency = 0;

            while (userChoice != 0)
            {
                //user's choice from edit menu
                userChoice = HabitInput.GetValidUserChoiceFromMenu("Your Choice to edit", editMenuStartChoice - 1, editMenuEndChoice);

                //take new edited data from user
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
            HabitManager.OperationStatus status = manager.EditHabit(desiredHabitId, newHabitName, newHabitDescription, newHabitFrequency);

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

            //get Today's habit from the manager
            List<Habit> TodayHabitList = manager.TodayHabits(manager.AllHabits);

            //check for empty list
            if (TodayHabitList.Count == 0)
            {
                Console.WriteLine("No Habits for Today");
                return;
            }

            //if not empty view today's habits
            HabitOutput.ViewHabits(TodayHabitList);
        }
        private void MarkHabitAsDone()
        {
            Console.WriteLine("========= Mark Habits =========");

            //view habits to choose from
            HabitOutput.ViewHabits(manager.AllHabits);

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            //delegate to HabitManager to perform the logic
            HabitManager.OperationStatus status = manager.MarkHabitAsDone(desiredHabit);

            //feedback to user
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

            //view all habits to choose from
            HabitOutput.ViewHabits(manager.AllHabits);

            //get valid id from user 
            int desiredHabitId = ChooseHabitByID();

            //get corresponding Habit to the id
            Habit desiredHabit = manager.AllHabits[desiredHabitId - 1];

            //delegate to HabitManager to perform the logic
            HabitManager.OperationStatus status = manager.UndoMarkedHabit(desiredHabit);

            //feedback to user
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
            HabitManager.OperationStatus status = manager.ClearList(manager.AllHabits);

            if (status == HabitManager.OperationStatus.SUCCESS)
            {
                Console.WriteLine("List Destroyed Successfully");
            }
            else
            {
                Console.WriteLine("List is Already empty");
            }
        }
    }
}