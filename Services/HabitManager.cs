using System.Text.Json;
using HabitTracker.Models;
using HabitTracker.UI;

namespace HabitTracker.Services
{
    class HabitManager
    {

        private const string JsonFilePath = "AllHabits.json";
        public List<Habit> AllHabits = new List<Habit>();
        private static int idCounter = 0;

        public enum operationStatus
        {
            SUCCESS,
            FAILURE,
            VALID,
            INVALID_INPUT,
            NULL_VALUE
        }
        public HabitManager()
        {
            //Load Habits from Json file to AllHabit List
            AllHabits = LoadHabitsFromJSON();

            //Update idCounter to the last id in Json file
            idCounter = AllHabits.Count;

        }

        //function : check if the given ID exists in Habit list or not
        //inputs   : User's choosen id
        //return   : Operation status
        public operationStatus HabitExistinList(int userIdChoice, ref Habit habit)
        {
            if (userIdChoice < 1 || userIdChoice > AllHabits.Count)
            {
                return operationStatus.INVALID_INPUT;
            }
            habit = AllHabits[userIdChoice - 1];
            return operationStatus.VALID;
        }

        //function : ceates a habit, add it to list, save to json
        //inputs   : the new habit data
        //return   :
        public operationStatus CreateHabit(Habit newHabit)
        {
            if (newHabit == null)
                return operationStatus.NULL_VALUE;
            idCounter++;
            newHabit.Id = idCounter;
            AllHabits.Add(newHabit);
            SaveHabitToJSON(AllHabits);
            return operationStatus.NULL_VALUE;
        }
        public void DeleteHabit(Habit desiredHabit)
        {
            if (AllHabits.Remove(desiredHabit))
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
            SaveHabitToJSON(AllHabits);
        }
        public void EditHabit(Habit desiredHabit)
        {
            int userChoice = 1;
            string? newHabitName = " new Name";
            string? newHabitDescription = " new desc";
            Habit.Day newHabitFrequency = 0;

            while (userChoice != 0)
            {
                if (!HabitInput.GetValidUserChoiceFromMenu(ref userChoice, "Your Choice to edit", 0, 3))
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
            SaveHabitToJSON(AllHabits);
        }
        public void MarkHabitAsDone(Habit desiredHabit)
        {
            if (!desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = true;
                Console.WriteLine($"Habit No.{desiredHabit.Id} completed successfuly");
            }

            else
                Console.WriteLine("Habit already completed for today");
        }
        public void UndoMarkedHabit(Habit desiredHabit)
        {
            if (desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = false;
                Console.WriteLine("Compeletion Undone");
            }

            else
                Console.WriteLine("Habit is already not marked");
        }

        public void ClearList(List<Habit> AllHabits)
        {
            AllHabits.Clear();
            SaveHabitToJSON(AllHabits);
        }

        // save all habits in the list to a JSON file
        private void SaveHabitToJSON(List<Habit> AllHabits)
        {
            //convet c# object to json
            string JsonString = JsonSerializer.Serialize(AllHabits, new JsonSerializerOptions { WriteIndented = true });

            //Write to Json file
            File.WriteAllText(JsonFilePath, JsonString);
        }

        private List<Habit> LoadHabitsFromJSON()
        {
            //read from Json file
            string JsonString = File.ReadAllText(JsonFilePath);

            //Convert to C# object
            List<Habit>? habits = JsonSerializer.Deserialize<List<Habit>>(JsonString);

            //finally, return the list of habits
            //or in case of an empty json file return an empty list
            return habits ?? new List<Habit>();
        }


    }
}
