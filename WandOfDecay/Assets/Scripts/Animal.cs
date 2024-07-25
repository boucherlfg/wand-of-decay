using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rect terrain;
    private Vector2 origin;
    private Vector2 destination;
    
    // Start is called before the first frame update
    void Start()
    {
        terrain = FindObjectOfType<Generator>(true).size;
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, destination) < speed * Time.deltaTime)
        {
            transform.position = destination;
            SetNewDestination();
        }
        var direction = (destination - origin).normalized;
        transform.position += speed * Time.deltaTime * (Vector3)direction;
    }
    void SetNewDestination()
    {
        origin = transform.position;
        destination = terrain.RandomInZone();
    }
}
