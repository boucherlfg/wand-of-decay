using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCondition : MonoBehaviour
{
    [SerializeField]
    private float gameDuration = 3 * 60;
    [SerializeField]
    private GameObject gameOverDisplay;
    // Update is called once per frame
    void Update()
    {
        gameDuration -= Time.deltaTime;
        ServiceManager.Instance.Get<OnEndGameTimer>().Invoke(gameDuration);

        if (gameDuration <= 0)
        {
            gameOverDisplay.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
