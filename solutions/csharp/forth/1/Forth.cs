using System;
using System.Collections.Generic;
using System.Linq;

public static class Forth
{
    public static string Evaluate(string[] instructions)
    {
        var stack = new Stack<int>();
        var definitions = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        var tokens = Tokenize(instructions);
        Execute(tokens, stack, definitions);

        return string.Join(" ", stack.Reverse());
    }

    private static List<string> Tokenize(string[] instructions)
    {
        var tokens = new List<string>();
        foreach (var line in instructions)
        {
            tokens.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(t => t.ToLower()));
        }
        return tokens;
    }

    private static void Execute(List<string> tokens, Stack<int> stack, Dictionary<string, List<string>> definitions)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];

            if (token == ":")
            {
                if (i + 2 >= tokens.Count)
                    throw new InvalidOperationException("Invalid definition");

                var name = tokens[++i];
                if (int.TryParse(name, out _))
                    throw new InvalidOperationException("Cannot redefine numbers");

                var definition = new List<string>();
                while (++i < tokens.Count && tokens[i] != ";")
                    definition.Add(tokens[i]);

                if (i == tokens.Count)
                    throw new InvalidOperationException("Missing ';' in definition");

                // Expand any existing words inside the definition
                var expanded = new List<string>();
                foreach (var t in definition)
                {
                    if (definitions.ContainsKey(t))
                        expanded.AddRange(definitions[t]);
                    else
                        expanded.Add(t);
                }

                definitions[name] = expanded;
            }
            else
            {
                ProcessToken(token, stack, definitions);
            }
        }
    }

    private static void ProcessToken(string token, Stack<int> stack, Dictionary<string, List<string>> definitions)
    {
        if (int.TryParse(token, out int n))
        {
            stack.Push(n);
            return;
        }

        if (definitions.ContainsKey(token))
        {
            foreach (var subToken in definitions[token])
                ProcessToken(subToken, stack, definitions);
            return;
        }

        switch (token)
        {
            case "+": BinaryOp(stack, (a, b) => a + b); break;
            case "-": BinaryOp(stack, (a, b) => a - b); break;
            case "*": BinaryOp(stack, (a, b) => a * b); break;
            case "/":
                BinaryOp(stack, (a, b) =>
                {
                    if (b == 0) throw new DivideByZeroException();
                    return a / b;
                });
                break;
            case "dup":
                if (stack.Count < 1) throw new InvalidOperationException("Stack empty");
                stack.Push(stack.Peek());
                break;
            case "drop":
                if (stack.Count < 1) throw new InvalidOperationException("Stack empty");
                stack.Pop();
                break;
            case "swap":
                if (stack.Count < 2) throw new InvalidOperationException("Stack underflow");
                var a = stack.Pop();
                var b = stack.Pop();
                stack.Push(a);
                stack.Push(b);
                break;
            case "over":
                if (stack.Count < 2) throw new InvalidOperationException("Stack underflow");
                var top = stack.Pop();
                var next = stack.Peek();
                stack.Push(top);
                stack.Push(next);
                break;
            default:
                throw new InvalidOperationException($"Unknown word: {token}");
        }
    }

    private static void BinaryOp(Stack<int> stack, Func<int, int, int> op)
    {
        if (stack.Count < 2) throw new InvalidOperationException("Stack underflow");
        var b = stack.Pop();
        var a = stack.Pop();
        stack.Push(op(a, b));
    }
}

