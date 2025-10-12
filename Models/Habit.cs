using HabitTracker.Services;

namespace HabitTracker.Models
{
    class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Day Frequency { get; set; }
        public Habit(string habitName, string habitDescription, Day habitFrequency, int habitId = 0)
        {
            Id = habitId;
            Name = habitName;
            Description = habitDescription;
            Frequency = habitFrequency;
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
            ALLWEEK = 0b_0111_1111
        }

    }
}
