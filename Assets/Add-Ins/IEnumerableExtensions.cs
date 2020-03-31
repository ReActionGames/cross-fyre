using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source)
            action(item);
    }

    /// <summary>
    /// Gets a random element from the given IEnumerable.
    /// </summary>
    /// <typeparam name="T">The type of the IEnumerable.</typeparam>
    /// <param name="list">The IEnumerable in which to pick an element.</param>
    /// <returns>A random element from this IEnumerable.</returns>
    public static T PickRandom<T>(this IEnumerable<T> list)
    {
        int index;
        index = UnityEngine.Random.Range(0, list.Count());
        return list.ElementAt(index);
    }
}