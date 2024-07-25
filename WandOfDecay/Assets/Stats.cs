using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private Dictionary<DecayableType, int> points = new();

    public int this[DecayableType type]
    {
        get
        {
            if (!points.ContainsKey(type)) return 0;
            return points[type];
        }
        set
        {
            points[type] = value;
            Debug.Log($"{type} : {points[type]}");
        }
    }

}