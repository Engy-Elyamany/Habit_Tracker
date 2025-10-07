class Habit
{
    public string? Habit_Name;
    public string? Habit_Description;
    public int Habit_Frequency;

    enum Day
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