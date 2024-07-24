using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField]
    private bool destroyAfterCorrupt;
    [SerializeField]
    private int corruptStrength = 3;
    // Start is called before the first frame update
    void Start()
    {
        int x = (int)transform.position.x,
            y = (int)transform.position.y;
        var soil = Soil.soils[x, y];
        soil.Corrupt(corruptStrength);
        if (destroyAfterCorrupt) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
