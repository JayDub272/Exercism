using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var list = dominoes.ToList();
        if (list.Count == 0) return true;

        var adj = new Dictionary<int, List<int>>();
        foreach (var (a, b) in list)
        {
            if (!adj.ContainsKey(a)) adj[a] = new List<int>();
            if (!adj.ContainsKey(b)) adj[b] = new List<int>();
            adj[a].Add(b);
            adj[b].Add(a);
        }

        foreach (var kv in adj)
            if (kv.Value.Count % 2 != 0) return false;

        var visited = new HashSet<int>();
        var stack = new Stack<int>();
        int start = adj.Keys.First();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var v = stack.Pop();
            if (visited.Contains(v)) continue;
            visited.Add(v);
            foreach (var nb in adj[v])
                if (!visited.Contains(nb)) stack.Push(nb);
        }

        return adj.Keys.All(k => visited.Contains(k));
    }
}

