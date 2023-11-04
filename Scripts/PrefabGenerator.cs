using UnityEngine;
using UnityEditor;
using System.IO;

public static class PrefabGenerator
{
    public static void GeneratePrefab(ModuleTemplate currentModule, string prefabName)
    {
        GameObject moduleGameObject = new GameObject(prefabName);
        
        foreach (var uiElement in currentModule.uiElements)
        {
            if (uiElement is CheckboxUIElement checkbox && checkbox.isChecked && checkbox.scriptToAttach != null)
            {
                var componentType = checkbox.scriptToAttach.GetClass();
                if (componentType != null)
                {
                    moduleGameObject.AddComponent(componentType);
                }
            }
        }

        // Save as a prefab
        string localPath = "Assets/CreatedPrefabs/" + prefabName + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        
        if (!Directory.Exists("Assets/CreatedPrefabs"))
        {
            Directory.CreateDirectory("Assets/CreatedPrefabs");
        }

        PrefabUtility.SaveAsPrefabAsset(moduleGameObject, localPath);
        GameObject.DestroyImmediate(moduleGameObject); // Clean up

        Debug.Log($"Prefab {prefabName} has been created at {localPath}");
    }
}
