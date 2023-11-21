// REALMeditorWindow.cs

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class REALMeditorWindow : EditorWindow
{
    private List<ModuleTemplate> modules = new List<ModuleTemplate>();
    private ModuleTemplate selectedModule;
    private Vector2 moduleScrollPosition;
    private Vector2 developerOptionsScrollPosition;
    private string prefabName = "NewPrefab";
    private bool showPrefabNameField = false;
    private string modulesPath = "Assets/MODULES";
    private string generatedFolderPath = "Assets/Generated";

    // Added static buttons for GameObject and C# Script generation
    private static readonly GUIContent createGameObjectButtonContent = new GUIContent("Create Game Object");
    private static readonly GUIContent generateScriptButtonContent = new GUIContent("Generate C# Script");

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
            selectedModule = modules.FirstOrDefault(m => m.name == "DefaultModule") ?? modules[0];
        }

        // Ensure the "Generated" folder exists
        if (!AssetDatabase.IsValidFolder(generatedFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Generated");
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

        // Left panel for listing modules
        DrawModulesList();

        // Right panel for selected module editor
        DrawSelectedModuleEditor();

        GUILayout.EndHorizontal();

        // Draw static buttons at the bottom
        DrawStaticButtons();
    }

    private void DrawModulesList()
    {
        GUILayout.BeginVertical(GUILayout.Width(250), GUILayout.ExpandHeight(true));
        moduleScrollPosition = GUILayout.BeginScrollView(moduleScrollPosition);

        foreach (var module in modules)
        {
            if (GUILayout.Button(module.name))
            {
                selectedModule = module;
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void DrawSelectedModuleEditor()
    {
        GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        if (selectedModule != null)
        {
            GUILayout.Label(selectedModule.name, EditorStyles.boldLabel);

            // Display developer options of the selected module
            developerOptionsScrollPosition = GUILayout.BeginScrollView(developerOptionsScrollPosition);
            foreach (var developerOption in selectedModule.developerOptions)
            {
                developerOption.isSelected = GUILayout.Toggle(developerOption.isSelected, developerOption.name);

                if (developerOption.optionType == ModuleTemplate.OptionType.Dropdown)
                {
                    // Display dropdown options if the option is a dropdown
                    int selectedIndex = EditorGUILayout.Popup("Select Option:", 0, developerOption.dropdownOptions);
                    // Handle the selected index as needed
                }
            }
            GUILayout.EndScrollView();

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

            if (GUILayout.Button(createGameObjectButtonContent))
            {
                CreateGameObject();
            }
        }

        GUILayout.EndVertical();
    }

    private void DrawStaticButtons()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(createGameObjectButtonContent))
        {
            CreateGameObject();
        }

        if (GUILayout.Button(generateScriptButtonContent))
        {
            GenerateScript();
        }

        GUILayout.EndHorizontal();
    }

   private void CreateGameObject()
{
    if (selectedModule != null && !string.IsNullOrEmpty(prefabName))
    {
        // Implement prefab generation logic here
        // Example: Instantiate prefab using selectedModule and prefabName

        GameObject newGameObject = new GameObject(prefabName);

        // Iterate through selected developer options and apply their logic
        foreach (var developerOption in selectedModule.developerOptions)
        {
            if (developerOption.isSelected)
            {
                // Apply logic based on the selected developer option
                // Example: Attach a script based on the developer option
                // Note: You might need to adjust this part based on your specific implementation
                string scriptName = developerOption.ScriptName;
                System.Type scriptType = System.Type.GetType(scriptName);
                newGameObject.AddComponent(scriptType);
            }
        }

        // Save the new game object in the "Generated" folder
        PrefabUtility.SaveAsPrefabAsset(newGameObject, $"{generatedFolderPath}/{prefabName}.prefab");
        DestroyImmediate(newGameObject); // Destroy the temporary game object
    }
    else
    {
        EditorUtility.DisplayDialog("Prefab Name Required", "Please enter a name for the prefab.", "OK");
    }
}


    private void GenerateScript()
    {
        if (selectedModule != null)
        {
            // Implement script generation logic here
            // Example: Generate a C# script based on the selectedModule

            // Iterate through selected developer options and generate code
            string scriptContent = GenerateScriptContent(selectedModule);
            foreach (var developerOption in selectedModule.developerOptions)
            {
                if (developerOption.isSelected)
                {
                    // Generate code based on the selected developer option
                    // Example: Add code to the scriptContent
                    scriptContent += $"\t// Developer option: {developerOption.name}\n";
                    // Add code based on the developer option configuration
                }
            }

            string scriptPath = $"{generatedFolderPath}/{selectedModule.name}Script.cs";
            File.WriteAllText(scriptPath, scriptContent);
            AssetDatabase.Refresh();
            Debug.Log($"Script generated at: {scriptPath}");
        }
        else
        {
            EditorUtility.DisplayDialog("Module Not Selected", "Please select a module before generating the script.", "OK");
        }
    }

    private string GenerateScriptContent(ModuleTemplate module)
    {
      
        string scriptContent = $"public class {module.name}Script : MonoBehaviour\n" +
                               "{\n" +
                               "\t// Add your generated code here\n";
        foreach (var developerOption in module.developerOptions)
        {
            scriptContent += $"\t// Developer option: {developerOption.name}\n";
            // Add code based on developerOption configuration
        }
        scriptContent += "}\n";
        return scriptContent;
    }
}
