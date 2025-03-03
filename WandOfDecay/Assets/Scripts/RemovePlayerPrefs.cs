using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RemovePlayerPrefs))]
public class RemovePlayerPrefsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Remove PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("deleted player prefs!");
        }
    }
}
#endif

public class RemovePlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
