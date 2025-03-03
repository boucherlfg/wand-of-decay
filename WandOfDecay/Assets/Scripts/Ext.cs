using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
public static class Ext
{
    private const string HighScore = nameof(HighScore);

    [System.Serializable]
    public class Entry
    {
        public string name;
        public int score;
        public int position;
    }

    public static void PostEntry(string name, int score)
    {
        var result = GetEntries();
        Entry positionToReplace = result.FirstOrDefault(entry => entry.score < score);
        if (positionToReplace == null) return; // if there is no entry, it means that all scores are bigger than given score

        foreach (var entry in result.FindAll(x => x.position >= positionToReplace.position))
        {
	        entry.position++;
        }
        result.Insert(positionToReplace.position - 2, new Entry { name = name, score = score, position = positionToReplace.position - 1 });
        
        var json = JsonConvert.SerializeObject(result);
        PlayerPrefs.SetString(HighScore, json);
    }

    private static List<Entry> CreateNewListOfEntries()
    {
	    var entries = new List<Entry>();
	    for (int i = 0; i < 10; i++)
	    {
		    entries.Add(new Entry { name = "AAA", score = 0, position = i+1 });
	    }

	    return entries;
    }
    public static List<Entry> GetEntries()
    {
	    var json = PlayerPrefs.GetString(HighScore, JsonConvert.SerializeObject(CreateNewListOfEntries()));
	    return JsonConvert.DeserializeObject<List<Entry>>(json);
    }
    
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