using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnumScriptableObject), true)]
public class EnumScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnumScriptableObject enumScriptableObject = (EnumScriptableObject)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Fill String"))
        {
            enumScriptableObject.FillString();
            EditorUtility.SetDirty(enumScriptableObject);
        }

        if (GUILayout.Button("Fill Enum"))
        {
            enumScriptableObject.FillEnum();
            EditorUtility.SetDirty(enumScriptableObject);
        }
    }
}
