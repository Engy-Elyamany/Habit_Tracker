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

        public operationStatus HabitExistinList(int userIdChoice, ref Habit habit)
        {
            if (userIdChoice < 1 || userIdChoice > AllHabits.Count)
            {
                return operationStatus.INVALID_INPUT;
            }
            habit = AllHabits[userIdChoice - 1];
            return operationStatus.VALID;
        }
        public operationStatus CreateHabit(Habit ?newHabit)
        {
            if (newHabit == null)
                return operationStatus.NULL_VALUE;
            idCounter++;
            newHabit.Id = idCounter;
            AllHabits.Add(newHabit);
            SaveHabitToJSON(AllHabits);
            return operationStatus.SUCCESS;
        }

            public operationStatus DeleteHabit(Habit desiredHabit)
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
                return operationStatus.SUCCESS;
            }
            else
            {
                return operationStatus.FAILURE;
            }


        }
        public operationStatus EditHabit(Habit desiredHabit)
        {
            int userChoice = 1;
            string? newHabitName = " new Name";
            string? newHabitDescription = " new desc";
            Habit.Day newHabitFrequency = 0;

            while (userChoice != 0)
            {
                if (!HabitInput.GetValidUserChoiceFromMenu(ref userChoice, "Your Choice to edit", 0, 3))
                {
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        HabitInput.GetValidString(ref newHabitName, "Enter The new Habit Name");
                        desiredHabit.Name = newHabitName;
                        break;
                    case 2:
                        HabitInput.GetValidString(ref newHabitDescription, "Enter The new Habit Description");
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
            return operationStatus.SUCCESS;
        }
        public static operationStatus MarkHabitAsDone(Habit desiredHabit)
        {
            operationStatus status;
            if (!desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = true;
                status = operationStatus.SUCCESS;
            }

            else
            {
                status = operationStatus.FAILURE;
            }

            return status;

        }
        public static operationStatus UndoMarkedHabit(Habit desiredHabit)
        {
            operationStatus status;
            if (desiredHabit.MarkedAsDone)
            {
                desiredHabit.MarkedAsDone = false;
                status = operationStatus.SUCCESS;
            }

            else
                status = operationStatus.FAILURE;
            return status;
        }

        public void ClearList(List<Habit> AllHabits)
        {
            AllHabits.Clear();
            SaveHabitToJSON(AllHabits);
        }

        // save all habits in the list to a JSON file
        private static void SaveHabitToJSON(List<Habit> AllHabits)
        {
            //convet c# object to json
            string JsonString = JsonSerializer.Serialize(AllHabits, new JsonSerializerOptions { WriteIndented = true });

            //Write to Json file
            File.WriteAllText(JsonFilePath, JsonString);
        }

        private static List<Habit> LoadHabitsFromJSON()
        {
            //read from Json file
            string JsonString = File.ReadAllText(JsonFilePath);

            //Convert to C# object
            List<Habit>? habits = JsonSerializer.Deserialize<List<Habit>>(JsonString);

            //finally, return the list of habits
            //or in case of an empty json file return an empty list
            return habits ?? new List<Habit>();
        }

        public static List<Habit>? TodayHabits(List<Habit> AllHabits)
        {
            List<Habit> TodayHabits = new List<Habit>();
            DayOfWeek today = DateTime.Today.DayOfWeek;

            Console.WriteLine($"========= {today}'s Habits =========");

            foreach (var habit in AllHabits)
            {
                bool isTheHabitToday = Convert.ToBoolean(((int)habit.Frequency >> (int)today) & 1);
                if (isTheHabitToday)
                {
                    TodayHabits.Add(habit);
                }
            }

            return TodayHabits ?? null;

        }

    }
}
