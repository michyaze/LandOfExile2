using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    
    public static T PickItem<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            throw new System.InvalidOperationException("Cannot select a random item from an empty list.");
        }

        int index = Random.Range(0, list.Count);
        var item = list[index];
        list.RemoveAt(index);
        return item;
    }
    public static T RandomItem<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            throw new System.InvalidOperationException("Cannot select a random item from an empty list.");
        }

        int index = Random.Range(0, list.Count);
        return list[index];
    }

    public static T LastItem<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            throw new System.InvalidOperationException("Cannot select the last item from an empty list.");
        }
        return list[list.Count - 1];
    }
}