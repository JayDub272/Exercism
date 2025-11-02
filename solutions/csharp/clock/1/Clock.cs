using System;

public class Clock
{
    private int hours;
    private int minutes;

    public Clock(int hours, int minutes)
    {
        Normalize(hours * 60 + minutes);
    }

    private void Normalize(int totalMinutes)
    {
        totalMinutes %= 24 * 60;
        if (totalMinutes < 0)
            totalMinutes += 24 * 60;

        hours = totalMinutes / 60;
        minutes = totalMinutes % 60;
    }

    public Clock Add(int minutesToAdd)
    {
        return new Clock(hours, minutes + minutesToAdd);
    }

    public Clock Subtract(int minutesToSubtract)
    {
        return new Clock(hours, minutes - minutesToSubtract);
    }

    public override string ToString()
    {
        return $"{hours:D2}:{minutes:D2}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Clock other)
            return hours == other.hours && minutes == other.minutes;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(hours, minutes);
    }
}
