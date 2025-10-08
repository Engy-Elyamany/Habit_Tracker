internal class Program
{
    private static void Main(string[] args)
    {
        Habit.CreateHabit(1200, "Run", "Running all afternoon", Habit.Day.MON);
        Habit.CreateHabit(1000, "Code", "Coding", Habit.Day.TUE);
        Habit.CreateHabit(1010, "Draw", "Drawing", Habit.Day.FRI);
        Habit.ViewAllHabits();
    }
}
