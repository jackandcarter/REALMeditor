using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MODULEditorWindow : EditorWindow
{
    private List<ModuleTemplate> modules = new List<ModuleTemplate>();
    private ModuleTemplate selectedModule;
    private Vector2 scrollPosition;

    [MenuItem("Tools/MODULEditor")]
    public static void ShowWindow()
    {
        GetWindow<MODULEditorWindow>("MODULEditor");
    }

    private void OnEnable()
    {
        LoadModules();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // Left panel for listing modules
        DrawModulesList();

        // Right panel for selected module editor
        DrawSelectedModuleEditor();

        GUILayout.EndHorizontal();
    }

    private void DrawModulesList()
    {
        GUILayout.BeginVertical(GUILayout.Width(250));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var module in modules)
        {
            if (GUILayout.Button(module.moduleName))
            {
                selectedModule = module;
            }
        }

        if (GUILayout.Button("Create New Module"))
        {
            CreateNewModule();
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void CreateNewModule()
    {
        // Ensure the MODULES directory exists
        string folderPath = "Assets/MODULES";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "MODULES");
        }

        // Create a new ModuleTemplate instance
        var newModule = CreateInstance<ModuleTemplate>();
        newModule.moduleName = "New Module";
        modules.Add(newModule);
        selectedModule = newModule;

        // Generate a unique path within the MODULES directory
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/NewModuleTemplate.asset");

        // Create and save the new module asset
        AssetDatabase.CreateAsset(newModule, assetPath);
        AssetDatabase.SaveAssets();

        // Refresh the AssetDatabase after creation
        AssetDatabase.Refresh();
    }

    private void DrawSelectedModuleEditor()
    {
        GUILayout.BeginVertical();

        if (selectedModule != null)
        {
            // Use the Editor API to draw the selected module's inspector
            Editor moduleEditor = Editor.CreateEditor(selectedModule);
            moduleEditor.OnInspectorGUI();
        }

        GUILayout.EndVertical();
    }

    private void LoadModules()
    {
        modules.Clear();
        string[] guids = AssetDatabase.FindAssets("t:ModuleTemplate", new[] { "Assets/MODULES" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var module = AssetDatabase.LoadAssetAtPath<ModuleTemplate>(path);
            if (module != null)
            {
                modules.Add(module);
            }
        }
    }
}
