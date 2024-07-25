using UnityEngine;

public class FreezeTimeInMenu : MonoBehaviour
{
    private float regularTime;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}