using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    private static Random Rng = new Random();

    public static T Random<T>(this List<T> list, Random random = null)
    {
        if(random == null)
            random = Rng;
        return list.ElementAtOrDefault(random.Next(list.Count));
    }
}
