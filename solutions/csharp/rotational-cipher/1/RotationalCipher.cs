using System;
using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char baseChar = char.IsUpper(c) ? 'A' : 'a';
                char rotated = (char)((((c - baseChar) + shiftKey) % 26) + baseChar);
                result.Append(rotated);
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}
