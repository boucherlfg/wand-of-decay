using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectWithCount
    {
        public GameObject[] prefabs;
        public int count;
    }
    public Rect size;
    public ObjectWithCount[] objectsWithCounts;

    public GameObject soil;

    private List<GameObject> objects;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
        player = FindObjectOfType<Player>();
    }
    public void Generate()
    {
        objects=new List<GameObject>();
        for (int i = (int)size.x - 1; i < size.x + size.width + 1; i++)
        {
            for (int j = (int)size.y - 1; j < size.y + size.height + 1; j++)
            {
                objects.Add(Instantiate(soil, new Vector2(i, j), Quaternion.identity, transform));
            }
        }
        // var objs = new List<Transform>();
        foreach (var objCount in objectsWithCounts)
        {
            for (int i = 0; i < objCount.count; i++)
            {
                var choice = objCount.prefabs.RandomChoice();
                var randomPos = size.RandomInZone();
                objects.Add(Instantiate(choice, randomPos, Quaternion.identity, transform));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // objects.ForEach(x => x.SetActive(Vector2.Distance(player.transform.position, x.transform.position) < 20));
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.DrawSolidRectangleWithOutline(size, Vector4.zero, Color.white);
#endif
    }
}
