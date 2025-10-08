class Habit
{

    private int id;
    private string Habit_Name;
    private string Habit_Description;
    private Day Habit_Frequency;

    static List<Habit> AllHabits = new List<Habit>();

    private Habit(int id, string name, string desc, Day freq)
    {
        this.id = id;
        this.Habit_Name = name;
        this.Habit_Description = desc;
        this.Habit_Frequency = freq;
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

    public static Habit CreateHabit(int id, string name, string desc, Day freq)
    {
        Habit newHabit = new(id, name, desc, freq);
        AllHabits.Add(newHabit);
        return newHabit;
    }

    public static void ViewAllHabits()
    {
        foreach (var Habit in AllHabits)
        {
            System.Console.WriteLine(
            $"Habit ID = {Habit.id}" +
            $"\nHabit Name = {Habit.Habit_Name}" +
            $"\nHabit Description = {Habit.Habit_Description}" +
            $"\nHabit Frequency = {Habit.Habit_Frequency}"
            );
            System.Console.WriteLine();
        }
    }
}