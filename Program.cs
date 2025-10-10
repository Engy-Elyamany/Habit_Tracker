using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.UI;
internal class Program
{
    private static void Main(string[] args)
    {
        
        HabitManager.CreateHabit();
        HabitManager.ViewAllHabits();
    }
}
