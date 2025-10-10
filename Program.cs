using HabitTracker.Models;
using HabitTracker.Services;
internal class Program
{
    private static void Main(string[] args)
    {
        for (int i = 0; i < 2; i++)
        {
            HabitManager.CreateHabit();
        }
        Console.WriteLine();
        HabitManager.ViewAllHabits();
    }
}
