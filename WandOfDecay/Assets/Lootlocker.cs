using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootlocker : MonoBehaviour
{
    private const string LootlockerPlayerId = nameof(LootlockerPlayerId);
    const string leaderboardId = "WandOfDecayHighscores";
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession(res =>
        {
            if (res.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString(LootlockerPlayerId, res.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => !done);
    }

    public IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString(LootlockerPlayerId);
        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardId, res =>
        {
            if (res.success)
            {
                Debug.Log("Successfully uploaded score");
            }
            else
            {
                Debug.Log("Failed : " + res.errorData);
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
