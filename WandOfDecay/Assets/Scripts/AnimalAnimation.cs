using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimation : MonoBehaviour
{
    private SpriteRenderer rend;
    public bool reverseAnimation = false;
    private float lastX;
    private float lastDX;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = transform.position.x;
        var dx = (x - lastX) / Time.deltaTime;
        lastX = x;

        if (dx * dx > 0)
        {
            lastDX = dx;
            rend.flipX = reverseAnimation ? lastDX < -0.1f : lastDX > 0.1f;
        }
    }
}
