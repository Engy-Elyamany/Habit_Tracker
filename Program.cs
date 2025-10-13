using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.UI;
internal class Program
{
    private static void Main(string[] args)
    {

        // HabitManager.CreateHabit();
        // HabitManager.CreateHabit();
        // HabitManager.CreateHabit();
        // HabitOutput.ViewAllHabits();
        // HabitManager.DeleteHabit();
        var manager = new HabitManager();
        HabitOutput.ViewAllHabits();

        // HabitManager.ViewAllHabits();
        // HabitManager.EditHabit();
        // HabitManager.ViewAllHabits();
    }
}
