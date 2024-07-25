using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private static bool firstOpening = true;
    // Start is called before the first frame update
    void Start()
    {
        if (firstOpening)
        {
            firstOpening = false;
            return;
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
