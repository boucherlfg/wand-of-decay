using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public static DictGrid<Soil> soils = new();

    [SerializeField]
    private Sprite corruptedSprite;
    [SerializeField]
    private int corruptStrength;
    [SerializeField]
    private float corruptTime = 0.5f;
    private void Start()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        soils[x, y] = this;
        if (corruptStrength > 0)
        {
            Corrupt(corruptStrength);
        }
    }
    public void Corrupt(int strength) 
    {
        if (strength <= 0) return;

        ServiceManager.Instance.Get<Stats>()[DecayableType.Soil]++;
        corruptStrength = strength;
        GetComponent<SpriteRenderer>().sprite = corruptedSprite;
        StartCoroutine(CorruptNeighbours());
    }

    IEnumerator CorruptNeighbours()
    {
        yield return new WaitForSeconds(corruptTime);
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((i + j) * (i + j) != 1) continue;
                var soil = soils[x + i, y + j];
                if(soil == null) continue;
                if (soil.corruptStrength > 0) continue;

                soil.Corrupt(corruptStrength - 1);
            }
        }
    }
}
