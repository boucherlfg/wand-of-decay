using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCondition : MonoBehaviour
{
    [SerializeField]
    private float gameDuration = 3 * 60;
    [SerializeField]
    private GameObject gameOverDisplay;
    // [SerializeField]
    // private Leaderboard leaderboard;
    private Stats stats;
    private void Start()
    {
        stats = ServiceManager.Instance.Get<Stats>();
    }
    // Update is called once per frame
    void Update()
    {
        gameDuration -= Time.deltaTime;
        ServiceManager.Instance.Get<OnEndGameTimer>().Invoke(gameDuration);

        if (gameDuration <= 0)
        {
            var generator = FindObjectOfType<Generator>();
            foreach (Transform t in generator.transform)
            {
                Destroy(t.gameObject);
            }
            // leaderboard.AddEntry((uint)stats[DecayableType.Soil]);
            // gameOverDisplay.SetActive(true);
            ServiceManager.Instance.Get<OnGameEnded>().Invoke();
            gameObject.SetActive(false);
        }
    }
}
