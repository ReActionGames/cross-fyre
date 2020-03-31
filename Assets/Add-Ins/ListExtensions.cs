﻿using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    /// <summary>
    /// Gets a random element from the given list.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    /// <param name="list">The list in which to pick an element.</param>
    /// <returns>A random element from this list.</returns>
    public static T PickRandom<T>(this List<T> list)
    {
        int index;
        index = Random.Range(0, list.Count);
        return list[index];
    }

    public static float GetAverage(this List<float> list)
    {
        if (list.Count <= 0) return 0;

        float sum = 0;
        foreach (float number in list)
        {
            sum += number;
        }

        return sum / list.Count;
    }
}