using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private SpriteRenderer rend;
    public float floatSpeed = 0.05f;
    public float floatAmplitude = 0.05f;
    private float lastX;
    private float lastDX;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        lastX = transform.position.x;
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
            rend.flipX = lastDX > 0.1f;
        }

        transform.localPosition = floatAmplitude * Mathf.Sin(Time.time * floatSpeed * 2 * Mathf.PI) * Vector2.up;
    }
}
