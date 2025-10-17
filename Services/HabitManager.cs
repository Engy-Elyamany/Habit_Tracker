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

        //checks if the given id is within the list
        public OperationStatus HabitExistinList(int userIdChoice)
        {
            if (userIdChoice < 1 || userIdChoice > AllHabits.Count)
            {
                return OperationStatus.INVALID_INPUT;
            }
            return OperationStatus.VALID;
        }

        //returns a list with all today's habits
        public List<Habit> TodayHabits(List<Habit> AllHabits)
        {
            List<Habit> TodayHabits = new List<Habit>();
            DayOfWeek today = DateTime.Today.DayOfWeek;

            foreach (var habit in AllHabits)
            {
                //GET_BIT operation: retuns the bit (0 or 1) of the given index (today) from a binary value (habit.Frequency)
                //Tuesday = 2 in DayOfWeek (built-in),
                //so, this operation checks the second bit(starting from bit no. 0) in habit.Frequency which is binary
                //isTheHabitToday will be true if Tuesday is in habit.Frequency
                //because TUE = 0b_0000_0100 (index 2 is set to one)

                bool isTheHabitToday = Convert.ToBoolean(((int)habit.Frequency >> (int)today) & 1);
                if (isTheHabitToday)
                {
                    //if the habit is today
                    // uncheck it to (not done) if it was aleady checked as (done)
                    UndoMarkedHabit(habit);

                    //add to Today's Habits list
                    TodayHabits.Add(habit);
                }
            }

            return TodayHabits;

        }

        public OperationStatus CreateHabit(Habit? newHabit)
        {
            if (newHabit == null)
                return OperationStatus.NULL_VALUE;

            //increment the counter from the last habit
            idCounter++;
            newHabit.Id = idCounter;

            // add the habit to the general list
            AllHabits.Add(newHabit);

            //saves the new list to json
            SaveHabitToJSON(AllHabits);
            return OperationStatus.SUCCESS;
        }
        public OperationStatus DeleteHabit(Habit desiredHabit)
        {
            if (AllHabits.Remove(desiredHabit))
            {
                //decrement the overall idCounter
                idCounter--;

                //sync all IDs with the new order after deletion
                for (int i = 0; i < AllHabits.Count; i++)
                {
                    AllHabits[i].Id = i + 1;
                }

                //save the new changes to json
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
            //get the corresponding habit to the povided id
            Habit desiredHabit = AllHabits[id - 1];

            //assign all new data while keeping existing data that wasn't updated as is
            desiredHabit.Name = (newName == "") ? desiredHabit.Name : newName;
            desiredHabit.Description = (newDescription == "") ? desiredHabit.Description : newDescription;
            desiredHabit.Frequency = (newFreq == 0) ? desiredHabit.Frequency : newFreq;

            //save all new changes to json
            SaveHabitToJSON(AllHabits);

            return OperationStatus.SUCCESS;
        }
        public OperationStatus MarkHabitAsDone(Habit desiredHabit)
        {
            OperationStatus status;

            //if the habit is (not done)
            if (!desiredHabit.MarkedAsDone)
            {
                //mark as (done)
                desiredHabit.MarkedAsDone = true;
                status = OperationStatus.SUCCESS;
            }

            //if habit aleasy marked as (done)
            else
            {
                //report that to user
                status = OperationStatus.FAILURE;
            }

            return status;

        }
        public OperationStatus UndoMarkedHabit(Habit desiredHabit)
        {
            OperationStatus status;

            //if the habit is marked as (done)
            if (desiredHabit.MarkedAsDone)
            {
                //undo compeletion
                desiredHabit.MarkedAsDone = false;
                status = OperationStatus.SUCCESS;
            }

            //if the habit already not marked
            else
                status = OperationStatus.FAILURE; //report to user
            return status;
        }

        public OperationStatus ClearList(List<Habit> AllHabits)
        {
            if (AllHabits.Count == 0)
            {
                return OperationStatus.FAILURE;
            }
            AllHabits.Clear();
            SaveHabitToJSON(AllHabits);
            return OperationStatus.SUCCESS;

        }

        // save data form the list to a JSON file
        private void SaveHabitToJSON(List<Habit> AllHabits)
        {
            //convert c# object to json
            string JsonString = JsonSerializer.Serialize(AllHabits, new JsonSerializerOptions { WriteIndented = true });

            //Write to Json file
            File.WriteAllText(JsonFilePath, JsonString);
        }

        //load data from json to list<>
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
