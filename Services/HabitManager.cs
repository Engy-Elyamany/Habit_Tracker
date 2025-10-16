using System.Text.Json;
using HabitTracker.Models;

namespace HabitTracker.Services
{
    class HabitManager
    {

        private const string JsonFilePath = "AllHabits.json";
        public List<Habit> AllHabits = new List<Habit>();
        private static int idCounter = 0;
        public enum OperationStatus
        {
            SUCCESS = -5,
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
        public OperationStatus HabitExistinList(int userIdChoice)
        {
            if (userIdChoice < 1 || userIdChoice > AllHabits.Count)
            {
                return OperationStatus.INVALID_INPUT;
            }
            return OperationStatus.VALID;
        }
        public List<Habit> TodayHabits(List<Habit> AllHabits)
        {
            List<Habit> TodayHabits = new List<Habit>();
            DayOfWeek today = DateTime.Today.DayOfWeek;

            foreach (var habit in AllHabits)
            {
                bool isTheHabitToday = Convert.ToBoolean(((int)habit.Frequency >> (int)today) & 1);
                if (isTheHabitToday)
                {
                    TodayHabits.Add(habit);
                }
            }

            return TodayHabits;

        }

        public OperationStatus CreateHabit(Habit? newHabit)
        {
            if (newHabit == null)
                return OperationStatus.NULL_VALUE;
            idCounter++;
            newHabit.Id = idCounter;
            AllHabits.Add(newHabit);
            SaveHabitToJSON(AllHabits);
            return OperationStatus.SUCCESS;
        }
        public OperationStatus DeleteHabit(Habit desiredHabit)
        {
            if (AllHabits.Remove(desiredHabit))
            {
                idCounter--;
                //sync all IDs with the new order after deletion
                for (int i = 0; i < AllHabits.Count; i++)
                {
                    AllHabits[i].Id = i + 1;
                }
                SaveHabitToJSON(AllHabits);
                return OperationStatus.SUCCESS;
            }
            else
            {
                return OperationStatus.FAILURE;
            }


        }
        public OperationStatus EditHabit(int id, string newName, string newDescription, Habit.Day newFreq)
        {
            Habit desiredHabit = AllHabits[id - 1];
            desiredHabit.Name = (newName == "") ? desiredHabit.Name : newName;
            desiredHabit.Description = (newDescription == "") ? desiredHabit.Description : newDescription;
            desiredHabit.Frequency = (newFreq == 0) ? desiredHabit.Frequency : newFreq;
            SaveHabitToJSON(AllHabits);
            return OperationStatus.SUCCESS;
        }
        public OperationStatus MarkHabitAsDone(Habit desiredHabit)
        {
            OperationStatus status;
            if (!desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = true;
                status = OperationStatus.SUCCESS;
            }

            else
            {
                status = OperationStatus.FAILURE;
            }

            return status;

        }
        public OperationStatus UndoMarkedHabit(Habit desiredHabit)
        {
            OperationStatus status;
            if (desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = false;
                status = OperationStatus.SUCCESS;
            }

            else
                status = OperationStatus.FAILURE;
            return status;
        }
        public void ClearList(List<Habit> AllHabits)
        {
            AllHabits.Clear();
            SaveHabitToJSON(AllHabits);
        }

        // save all habits in the list to a JSON file
        private void SaveHabitToJSON(List<Habit> AllHabits)
        {
            //convert c# object to json
            string JsonString = JsonSerializer.Serialize(AllHabits, new JsonSerializerOptions { WriteIndented = true });

            //Write to Json file
            File.WriteAllText(JsonFilePath, JsonString);
        }
        private List<Habit> LoadHabitsFromJSON()
        {
            if (!File.Exists(JsonFilePath))
                return new List<Habit>();
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
