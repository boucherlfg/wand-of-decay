using UnityEngine;

public class GameDurationDisplay : MonoBehaviour
{
    private TMPro.TMP_Text label;

    private void Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
        ServiceManager.Instance.Get<OnEndGameTimer>().Subscribe(HandleTimer);
    }
    private void OnDestroy()
    {
        ServiceManager.Instance.Get<OnEndGameTimer>().Unsubscribe(HandleTimer);
    }

    void HandleTimer(float timer)
    {
        var minutes = ((int)timer) / 60;
        var seconds = ((int)timer) % 60;
        label.text = $"{ minutes } : { seconds }";
    }
}