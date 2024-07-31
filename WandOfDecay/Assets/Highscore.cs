using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public GameObject newHighscore;
    public GameObject leaderboard;
    private void Start()
    {
        ServiceManager.Instance.Get<OnGameEnded>().Subscribe(HandleGameEnded);
    }
    private void OnDestroy()
    {
        ServiceManager.Instance.Get<OnGameEnded>().Unsubscribe(HandleGameEnded);
    }
    private void HandleGameEnded()
    {
        StartCoroutine(GameEndedCoroutine());
    }
    IEnumerator GameEndedCoroutine()
    {
        var score = ServiceManager.Instance.Get<Stats>()[DecayableType.Soil];

        var result = new Ext.Ref<List<Ext.Entry>>();
        yield return Ext.GetEntries(result);

        var entries = result.value;
        if (entries.Any(x => x.score < score))
        {
            newHighscore.SetActive(true);
        }
        else
        {
            leaderboard.SetActive(true);
        }
    }
}
