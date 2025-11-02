using System;
using System.Collections.Generic;

public static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in input)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0)
                    return false;

                char open = stack.Pop();

                if (!IsMatchingPair(open, c))
                    return false;
            }
        }

        return stack.Count == 0;
    }

    private static bool IsMatchingPair(char open, char close)
    {
        return (open == '(' && close == ')') ||
               (open == '[' && close == ']') ||
               (open == '{' && close == '}');
    }
}
