using System;

public static class Bob
{
    public static string Response(string statement)
    {
        if (statement == null)
            return "Fine. Be that way!";

        string trimmed = statement.Trim();

        if (trimmed == "")
            return "Fine. Be that way!";

        bool isQuestion = trimmed.EndsWith("?");
        bool isYelling = IsYelling(trimmed);

        if (isYelling && isQuestion)
            return "Calm down, I know what I'm doing!";
        else if (isYelling)
            return "Whoa, chill out!";
        else if (isQuestion)
            return "Sure.";
        else
            return "Whatever.";
    }

    private static bool IsYelling(string s)
    {
        bool hasLetters = false;
        foreach (char c in s)
        {
            if (char.IsLetter(c))
            {
                hasLetters = true;
                if (!char.IsUpper(c))
                    return false;
            }
        }
        return hasLetters;
    }
}
