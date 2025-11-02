using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if (inputBase < 2)
            throw new ArgumentException("input base must be >= 2");
        if (outputBase < 2)
            throw new ArgumentException("output base must be >= 2");
        if (inputDigits.Length == 0)
            return new int[] { 0 };
        if (inputDigits.Any(d => d < 0 || d >= inputBase))
            throw new ArgumentException("all digits must be in the range [0, inputBase)");

        // Remove leading zeros
        inputDigits = inputDigits.SkipWhile(d => d == 0).ToArray();
        if (inputDigits.Length == 0)
            return new int[] { 0 };

        // Convert input digits to an integer (base 10)
        int value = 0;
        foreach (var digit in inputDigits)
        {
            value = value * inputBase + digit;
        }

        // Convert that integer to output base
        var result = new List<int>();
        while (value > 0)
        {
            result.Insert(0, value % outputBase);
            value /= outputBase;
        }

        return result.ToArray();
    }
}
