using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public Transform content;
    public GameObject entryPrefab;
    private void OnEnable()
    {
        StartCoroutine(EnableCoroutine());
    }
    IEnumerator EnableCoroutine()
    {
        var result = new Ext.Ref<List<Ext.Entry>>();
        yield return Ext.GetEntries(result);
        var entries = result.value;

        foreach (var entry in entries)
        {
            var comp = Instantiate(entryPrefab, content).GetComponent<EntryScript>();
            comp.Position = entry.position;
            comp.Name = entry.name;
            comp.Score = entry.score;
        }
    }
}
