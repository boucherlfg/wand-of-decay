
#if UNITY_EDITOR
using UnityEngine;

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
