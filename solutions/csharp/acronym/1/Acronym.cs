using System;
using System.Text.RegularExpressions;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        if (string.IsNullOrWhiteSpace(phrase))
            return string.Empty;

        var words = Regex.Split(phrase.Replace('-', ' '), @"[\s,]+");
        string acronym = "";

        foreach (var word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                foreach (var c in word)
                {
                    if (char.IsLetter(c))
                    {
                        acronym += char.ToUpper(c);
                        break;
                    }
                }
            }
        }

        return acronym;
    }
}

