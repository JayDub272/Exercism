using System;
using System.Collections.Generic;
using System.Linq;

public static class Wordy
{
    public static int Answer(string question)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Invalid question");

        if (!question.StartsWith("What is ") || !question.EndsWith("?"))
            throw new ArgumentException("Unknown operation");

        question = question.Substring(8, question.Length - 9).Trim();

        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Syntax error");

        var tokens = question.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        var combined = new List<string>();
        for (int i = 0; i < tokens.Count; i++)
        {
            var word = tokens[i];

            if ((word == "multiplied" || word == "divided") && i + 1 < tokens.Count && tokens[i + 1] == "by")
            {
                combined.Add(word + " by");
                i++;
            }
            else
            {
                combined.Add(word);
            }
        }

        int result;
        if (!int.TryParse(combined[0], out result))
            throw new ArgumentException("Syntax error");

        for (int i = 1; i < combined.Count; i += 2)
        {
            if (i + 1 >= combined.Count)
                throw new ArgumentException("Syntax error");

            var op = combined[i];
            if (!int.TryParse(combined[i + 1], out int num))
                throw new ArgumentException("Syntax error");

            result = op switch
            {
                "plus" => result + num,
                "minus" => result - num,
                "multiplied by" => result * num,
                "divided by" => num == 0 ? throw new DivideByZeroException() : result / num,
                _ => throw new ArgumentException("Unknown operation")
            };
        }

        return result;
    }
}
