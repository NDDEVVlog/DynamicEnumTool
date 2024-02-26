using UnityEditor;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnum : MonoBehaviour
{
    [MenuItem("ND/Tools/GenerateEnumFolder")]
    public static void CreateEnumFolder()
    {
        string targetFolder = "Enums";

        if (!AssetDatabase.IsValidFolder(targetFolder))
        {
            string[] folders = targetFolder.Split('/');
            string currentPath = "Assets";

            for (int i = 0; i < folders.Length; i++)
            {
                currentPath = System.IO.Path.Combine(currentPath, folders[i]);

                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    AssetDatabase.CreateFolder(System.IO.Path.GetDirectoryName(currentPath), System.IO.Path.GetFileName(currentPath));
                    Debug.Log("Folder created at: " + currentPath);
                }
            }

            Debug.Log("Enum folder created at: " + targetFolder);
        }
        else
        {
            Debug.Log("Enum folder already exists at: " + targetFolder);
        }

        AssetDatabase.Refresh();

    }
    public static void FillEnum(ScriptableObject script, Type theEnum, List<string> enumEntries)
    {
        if (!theEnum.IsEnum)
        {
            throw new ArgumentException("The provided type is not an enum.");
        }

        string enumName = theEnum.Name;

        // Get the path to the script file
        MonoScript scriptObject = MonoScript.FromScriptableObject(script);
        string scriptPath = AssetDatabase.GetAssetPath(scriptObject);

        // Read existing content
        string existingContent = File.Exists(scriptPath) ? File.ReadAllText(scriptPath) : string.Empty;

        // Find the location of the 'public enum' declaration
        int startIndex = existingContent.IndexOf("public enum " + enumName);

        if (startIndex == -1)
        {
            // If 'public enum' is not found, log an error
            Debug.LogError($"'public enum {enumName}' not found in {scriptPath}");
            return;
        }

        // Find the start and end indices of the 'public enum' block
        int openBraceIndex = existingContent.IndexOf("{", startIndex) + 1;
        int closeBraceIndex = existingContent.IndexOf("}", openBraceIndex) + 1;

        // Copy content before the 'public enum' block
        string beforeEnum = existingContent.Substring(0, startIndex);

        // Copy content after the 'public enum' block
        string afterEnum = existingContent.Substring(closeBraceIndex);

        using (StreamWriter streamWriter = new StreamWriter(scriptPath))
        {
            // Write content before the 'public enum' block
            streamWriter.Write(beforeEnum);

            // Write new or modified enum entries
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");

            for (int i = 0; i < enumEntries.Count; i++)
            {
                streamWriter.WriteLine("    " + enumEntries[i] + ",");
            }

            streamWriter.WriteLine("}");

            // Write content after the 'public enum' block
            streamWriter.Write(afterEnum);
        }

        AssetDatabase.Refresh();
    }


[MenuItem("Tools/GenerateEnum")]
	public static void FillEnum(Type script,Type theEnum, List<string> enumEntries)
	{
        if (!theEnum.IsEnum)
        {
            throw new ArgumentException("The provided type is not an enum.");
        }

        string enumName = theEnum.Name;
        string filePathAndName = "Assets/Enums/" + script.GetType().Name + ".cs";

        // Read existing content
        string existingContent = File.Exists(filePathAndName) ? File.ReadAllText(filePathAndName) : string.Empty;

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            // Write existing content up to the 'public enum' line
            streamWriter.Write(existingContent);

            // Find the location of the 'public enum' declaration
            int startIndex = existingContent.IndexOf("public enum");

            if (startIndex == -1)
            {
                // If 'public enum' is not found, write a new declaration
                streamWriter.WriteLine("public enum " + enumName);
                streamWriter.WriteLine("{");
            }
            else
            {
                // If 'public enum' is found, update the existing declaration
                streamWriter.WriteLine("public enum " + enumName);
                streamWriter.WriteLine("{");

                // Extract and write the existing entries
                int endIndex = existingContent.IndexOf("}", startIndex) + 1;
                string existingEnumContent = existingContent.Substring(startIndex, endIndex - startIndex);
                streamWriter.Write(existingEnumContent);
            }

            // Write new or modified enum entries
            for (int i = 0; i < enumEntries.Count; i++)
            {
                streamWriter.WriteLine("    " + enumEntries[i] + ",");
            }

            streamWriter.WriteLine("}");
        }

        AssetDatabase.Refresh();
    
    }
	public static void FillStringEnumList(List<string> listString, Type enumType)
    {
		if (!enumType.IsEnum)
		{
			throw new ArgumentException("The provided type is not an enum.");
		}

		string[] enumNames = Enum.GetNames(enumType);

		foreach (string enumName in enumNames)
		{
			// Check if the enumName is not already in the list before adding
			if (!listString.Contains(enumName))
			{
				listString.Add(enumName);
			}
		}
	}
    [MenuItem("Sora/Tools")]
    public void SoraPart()
    {

    }
}