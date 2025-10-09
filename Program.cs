using HabitTracker.Models;
using HabitTracker.Services;
internal class Program
{
    private static void Main(string[] args)
    {
        for (int i = 0; i < 2; i++)
        {

            Console.Write("Enter name:");
            string name = Console.ReadLine();

            Console.Write("Enter desc:");
            string desc = Console.ReadLine();

            Console.Write("Enter Day:");
            Habit.Day freq = (Habit.Day)Convert.ToInt32(Console.ReadLine());

            HabitManager.CreateHabit(name, desc, freq);
        }
        Console.WriteLine();
        HabitManager.ViewAllHabits();
    }
}
