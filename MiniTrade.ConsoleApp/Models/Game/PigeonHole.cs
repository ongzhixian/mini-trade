﻿namespace MiniTrade.ConsoleApp.Models.Game;

internal class PigeonHole
{
    private static bool NextCombination(IList<int> num, int n, int k)
    {
        bool finished;

        var changed = finished = false;

        if (k <= 0) return false;

        for (var i = k - 1; !finished && !changed; i--)
        {
            if (num[i] < n - 1 - (k - 1) + i)
            {
                num[i]++;

                if (i < k - 1)
                    for (var j = i + 1; j < k; j++)
                        num[j] = num[j - 1] + 1;
                changed = true;
            }
            finished = i == 0;
        }

        return changed;
    }

    static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetCombinations(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private static void DoWork()
    {
        int countP = 0, countC = 0;
        const int k = 2;
        var n = new[] { "1", "2", "3" };

        Console.Write("n: ");
        foreach (var item in n)
        {
            Console.Write("{0} ", item);
        }
        Console.WriteLine();
        Console.WriteLine("k: {0}", k);
        Console.WriteLine();
        Console.WriteLine("===============================");
        Console.WriteLine("Permutations");
        Console.WriteLine("===============================");
        foreach (IEnumerable<string> i in GetPermutations(n, k))
        {
            Console.WriteLine(string.Join(" ", i));
            countC++;
        }
        Console.WriteLine("Count : " + countC);

        Console.WriteLine();
        Console.WriteLine("===============================");
        Console.WriteLine("Combinations");
        Console.WriteLine("===============================");
        foreach (IEnumerable<string> i in GetCombinations(n, k))
        {
            Console.WriteLine(string.Join(" ", i));
            countP++;
        }
        Console.WriteLine("Count : " + countP);

        Console.ReadKey();
    }
}
