using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeCirlcle : MonoBehaviour
{
    private SpriteRenderer rend;
    private float durationLeft;
    public float radius;
    private float visibleDuration = 5;
    private float diameter => radius * 2;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        durationLeft = visibleDuration;
        transform.localScale = Vector3.one * diameter;
    }
    // Update is called once per frame
    void Update()
    {
        durationLeft -= Time.deltaTime;

        var alpha = Mathf.Max(0, durationLeft / visibleDuration);
        var color = rend.color;
        color.a *= alpha;
        rend.color = color;


        if (durationLeft < 0)
        {
            Destroy(gameObject);
        }
    }
}
