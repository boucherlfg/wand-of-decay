using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraFollow : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        var pos = _camera.transform.position;
        var z = pos.z;
        pos = Vector2.Lerp(pos, transform.position, speed * Time.deltaTime);
        pos.z = z;
        _camera.transform.position = pos;
    }
}
