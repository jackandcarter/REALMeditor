using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class REALMeditorWindow : EditorWindow
{
    private List<ModuleTemplate> modules = new List<ModuleTemplate>();
    private ModuleTemplate selectedModule;
    private Vector2 scrollPosition;
    private string prefabName = "NewPrefab";
    private bool showPrefabNameField = false;
    private string modulesPath = "Assets/MODULES";

    [MenuItem("Tools/REALMeditor")]
    public static void ShowWindow()
    {
        GetWindow<REALMeditorWindow>("REALMeditor");
    }

    private void OnEnable()
    {
        LoadModules();
        if (modules.Count > 0)
        {
            selectedModule = modules.FirstOrDefault(m => m.moduleName == "DefaultModule") ?? modules[0];
        }
    }

    private void LoadModules()
    {
        modules.Clear();
        if (!Directory.Exists(modulesPath))
        {
            Debug.LogWarning($"Modules directory does not exist at {modulesPath}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:ModuleTemplate", new[] { modulesPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModuleTemplate module = AssetDatabase.LoadAssetAtPath<ModuleTemplate>(path);
            if (module != null)
            {
                modules.Add(module);
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        DrawModulesList();
        DrawMainEditorArea();
        GUILayout.EndHorizontal();
    }

    private void DrawModulesList()
    {
        GUILayout.BeginVertical(GUILayout.Width(250), GUILayout.ExpandHeight(true));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var module in modules)
        {
            if (GUILayout.Button(module.moduleName))
            {
                selectedModule = module;
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void DrawMainEditorArea()
    {
        GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        if (selectedModule != null)
        {
            GUILayout.Label(selectedModule.moduleName, EditorStyles.boldLabel);

            foreach (var uiElement in selectedModule.uiElements)
            {
                uiElement.OnGUI();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(selectedModule);
            }

            // Section for prefab generation
            EditorGUILayout.Space();
            showPrefabNameField = EditorGUILayout.BeginToggleGroup("Define Prefab Name", showPrefabNameField);
            if (showPrefabNameField)
            {
                prefabName = EditorGUILayout.TextField("Prefab Name", prefabName);
            }
            EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button("Generate Prefab"))
            {
                if (!string.IsNullOrWhiteSpace(prefabName))
                {
                    PrefabGenerator.GeneratePrefab(selectedModule, prefabName);
                }
                else
                {
                EditorUtility.DisplayDialog("Prefab Name Required", "Please enter a name for the prefab.", "OK");
                }
            }
        }

        GUILayout.EndVertical();
    }
}

// Make sure to also create a PrefabGenerator class with a method matching this signature:
public static class PrefabGenerator
{
    public static void GeneratePrefab(ModuleTemplate selectedModule, string prefabName)
    {
        // Implement prefab generation logic here
    }
}
