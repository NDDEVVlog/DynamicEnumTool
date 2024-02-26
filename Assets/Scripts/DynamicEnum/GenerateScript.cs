using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Sirenix.OdinInspector;
[CreateAssetMenu(menuName = "NDTool/EnumGenerater")]
public class GenerateScript : ScriptableObject
{   
    public string scriptName = "ScriptName";
    public string enumName = "EnumName";


    [Button("Generate")]
    public void Generate()
    {
        GeneratorScript(scriptName, enumName);
    }


    public  void GeneratorScript(string scriptName, string enumName)
    {
        string scriptContent = $@"
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class {scriptName} : ScriptableObject
{{
    public List<string> {scriptName.ToLower()}String;

    public enum {enumName}
    {{
        // Add your enum values here
    }}

    public {enumName} {scriptName.ToLower()};
    
    [Button(""FillString"")]
    public void FillString()
    {{
        GenerateEnum.FillStringEnumList({scriptName.ToLower()}String, typeof({enumName}));
    }}

    [Button(""FillEnum"")]
    public void FillEnum()
    {{
        GenerateEnum.FillEnum(this, typeof({enumName}), {scriptName.ToLower()}String);
    }}
}}
";

        string filePath = $"Assets/Enums/{scriptName}.cs";

        // Write the script content to a new file
        File.WriteAllText(filePath, scriptContent);

        // Refresh the AssetDatabase to reflect the changes in the Unity Editor
        AssetDatabase.Refresh();

        Debug.Log($"Script '{scriptName}.cs' created at: {filePath}");
    }
}
