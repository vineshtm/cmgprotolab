using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Util Script
/// For utility Methods
/// </summary>
public class Util
{
    /// <summary>
    /// Randomly Shuffle List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
