using ConsoleTables;
using HabitTracker.Models;

namespace HabitTracker.UI
{
    class HabitOutput
    {
        public static void ViewHabits(List<Habit> AllHabits)
        {

            if (AllHabits == null)
            {
                Console.WriteLine("Empty List! Nothing to display");
                return;
            }

            var table = new ConsoleTable("ID", "Name", "Description", "Frequency", "Status");
            foreach (var Habit in AllHabits)
            {
                string habitStatusString = Habit.MarkedAsDone ? "Done" : "Not Done";

                //add each habit to the table
                table.AddRow(Habit.Id, Habit.Name, Habit.Description, Habit.Frequency, habitStatusString);
            }
            //display the table

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            table.Write(Format.Alternative);
            Console.ResetColor();
        }
        public static void PrintMainMenu()
        {
            Console.WriteLine(
            "\n" +
            "Main Menu" +
            "\n1.Add Habit" +
            "\n2.View All Habits" +
            "\n3.View Today's Habits" +
            "\n4.Mark Habits as Done" +
            "\n5.Undo a habit completion" +
            "\n6.Edit Habit" +
            "\n7.Delete Habit" +
            "\n8.Destroy Habit list");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            "\nTo Exit press 0" +
            "\n");
            Console.ResetColor();

        }
        public static void PrintEditMenu()
        {
            Console.WriteLine(
             "1.Edit Habit Name" +
             "\n2.Edit Habit Description" +
             "\n3.Edit Habit Frequency");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            "\nTo Exit press 0" +
            "\n");
            Console.ResetColor();
        }
       public static void PrintDaysMenu()
        {
            Console.WriteLine(
                "1.Saturday\n" +
                "2.Sunday\n" +
                "3.Monday\n" +
                "4.Tuesday\n" +
                "5.Wednesday\n" +
                "6.Thursday\n" +
                "7.Friday\n" +
                "8.All Week\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            "\nTo Exit press 0" +
            "\n");
            Console.ResetColor();
        }
        private static void ViewHabitsID(Habit habit)
        {
            string habitStatusString = habit.MarkedAsDone ? "Done" : "Not Done";
            Console.WriteLine(
                $"ID = {habit.Id} , " +
                $"Name = {habit.Name} , " +
                $"Status = {habitStatusString}\n"
                );
        }


    }
}