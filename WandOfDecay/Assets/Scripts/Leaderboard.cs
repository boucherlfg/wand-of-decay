using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public Transform content;
    public GameObject entryPrefab;
    private void OnEnable()
    {
        var entries = Ext.GetEntries().OrderBy(x => x.position).ToArray();

        foreach (Transform child in content) Destroy(child.gameObject);

        for (var i = 0; i < entries.Length; i++)
        {
            var entry = entries[i];
            var comp = Instantiate(entryPrefab, content).GetComponent<EntryScript>();
            comp.Position = i + 1;
            comp.Name = entry.name;
            comp.Score = entry.score;
        }
    }
}
