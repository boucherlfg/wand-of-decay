using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
public static class Ext
{
    const string address = "http://deraju.ddns.net:3000";

    [System.Serializable]
    public class Entry
    {
        public string name;
        public int score;
        public int position;
    }

    public class Ref<T> { public T value; }

    
    public static IEnumerator PostEntry(string name, int score, Ref<List<Entry>> result)
    {
        var entry = new Entry { name = name, score = score, position = -1 };
        var json = JsonConvert.SerializeObject(entry);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        var uwr = new UnityWebRequest(address, "POST");
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "text/plain");
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            string responseBody = uwr.downloadHandler.text;
            var intermediate = JsonConvert.DeserializeObject<List<string>>(responseBody);
            var body = intermediate.Select(i => JsonConvert.DeserializeObject<Entry>(i));
            result.value = body?.OrderBy(x => x.position)?.ToList();
        }
    }

    public static IEnumerator GetEntries(Ref<List<Entry>> result)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(address);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            string responseBody = uwr.downloadHandler.text;
            var intermediate = JsonConvert.DeserializeObject<List<string>>(responseBody);
            var body = intermediate.Select(i => JsonConvert.DeserializeObject<Entry>(i));
            result.value = body?.OrderBy(x => x.position)?.ToList();
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
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