using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NewHighscore : MonoBehaviour
{
    private Stats stats;
    public TMPro.TMP_Text score;
    public TMPro.TMP_InputField input;

    public GameObject leaderboard;
    private void OnEnable()
    {
        stats = ServiceManager.Instance.Get<Stats>();
        var score = stats[DecayableType.Soil];
        this.score.text = "" + score;
        input.text = "";
        input.Select();
    }

    public void PostNewHighscore()
    {
        Ext.PostEntry(input.text, stats[DecayableType.Soil]);

        leaderboard.SetActive(true);
        gameObject.SetActive(false);
    }
}
