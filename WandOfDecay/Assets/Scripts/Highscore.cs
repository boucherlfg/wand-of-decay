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
        var score = ServiceManager.Instance.Get<Stats>()[DecayableType.Soil];

        var entries = Ext.GetEntries();

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
