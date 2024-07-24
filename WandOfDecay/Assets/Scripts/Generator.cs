using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Generator))]
public class GeneratorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            ((Generator)target).Generate();
        }
    }
}
#endif

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
        objects.ForEach(x => x.SetActive(Vector2.Distance(player.transform.position, x.transform.position) < 20));
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.DrawSolidRectangleWithOutline(size, Vector4.zero, Color.white);
#endif
    }
}
