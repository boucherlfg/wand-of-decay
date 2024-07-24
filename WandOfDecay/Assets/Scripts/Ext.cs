using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Ext
{
	public static T RandomChoice<T>(this IEnumerable<T> source)
	{
		var index = Random.Range(0, source.Count());
		return source.ElementAt(index);
	}

	public static Vector2 RandomInZone(this Rect rect)
	{
		return new Vector2(Random.Range(rect.x, rect.x + rect.width), Random.Range(rect.y, rect.y + rect.height)); 
	}

	public static Rect ToRect(this Transform transform)
	{
		return new Rect(transform.position, transform.localScale);
	}
}