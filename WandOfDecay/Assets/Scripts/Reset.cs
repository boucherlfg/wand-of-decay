using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }
}
