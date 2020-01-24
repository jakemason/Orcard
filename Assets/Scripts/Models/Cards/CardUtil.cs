using System;
using System.Collections.Generic;

public static class CardUtil
{
    private static readonly Random Rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k     = Rng.Next(n + 1);
            T   value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}