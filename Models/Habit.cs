namespace HabitTracker.Models
{
    class Habit

    {

        public int id = 0;
        public string habitName;
        public string habitDescription;
        public Day habitFrequency;
        public Habit(int id, string habitName, string habitDescription, Day habitFrequency)
        {
            this.id = id;
            this.habitName = habitName;
            this.habitDescription = habitDescription;
            this.habitFrequency = habitFrequency;
        }

        [Flags]
        public enum Day
        {
            SAT = 0b_0000_0001,
            SUN = 0b_0000_0010,
            MON = 0b_0000_0100,
            TUE = 0b_0000_1000,
            WED = 0b_0001_0000,
            THU = 0b_0010_0000,
            FRI = 0b_0100_0000,
        }

    }
}
