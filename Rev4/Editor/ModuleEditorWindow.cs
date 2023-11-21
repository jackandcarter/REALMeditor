using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MODULEditorWindow : EditorWindow
{
    private List<ModuleTemplate> modules = new List<ModuleTemplate>();
    private ModuleTemplate selectedModule;
    private Vector2 modulesScrollPosition;
    private Vector2 optionsScrollPosition;
    private bool showPreview = true;
    private string newModuleName = "New Module"; // Default name if not changed by the user

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

        // Middle panel for selected module editor
        DrawSelectedModuleEditor();

        // Right panel for module preview
        DrawModulePreview();

        GUILayout.EndHorizontal();
    }

    private void DrawModulesList()
    {
        GUILayout.BeginVertical(GUILayout.Width(250));
        modulesScrollPosition = GUILayout.BeginScrollView(modulesScrollPosition);

        GUILayout.Label("Module List", EditorStyles.boldLabel);

        foreach (var module in modules)
        {
            if (GUILayout.Button(module.moduleName, GUILayout.ExpandWidth(true)))
            {
                selectedModule = module;
            }
        }

        GUILayout.Label("Create New Module", EditorStyles.boldLabel);

        // Draw module name
        GUILayout.BeginHorizontal();
        GUILayout.Label("Module Name:", GUILayout.Width(120));
        newModuleName = EditorGUILayout.TextField(newModuleName, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        // Draw Create Module button
        if (GUILayout.Button("Create Module"))
        {
            CreateNewModule();
        }

        if (GUILayout.Button("Delete Module"))
        {
            DeleteModule();
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void DrawSelectedModuleEditor()
    {
        GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

        GUILayout.Label("Module Editor", EditorStyles.boldLabel);

        if (selectedModule != null)
        {
            // Draw module name
            GUILayout.BeginHorizontal();
            GUILayout.Label("Module Name:", GUILayout.Width(120));
            newModuleName = EditorGUILayout.TextField(newModuleName, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            // Draw module description and usage tooltip
            GUILayout.Label("Module Description", EditorStyles.boldLabel);
            selectedModule.moduleDescription = EditorGUILayout.TextArea(selectedModule.moduleDescription, GUILayout.Height(50)); // Adjusted height

            // Draw developer options
            DrawDeveloperOptions();

            // Save module button
            if (GUILayout.Button("Save Module"))
            {
                SaveModule();
            }
        }

        GUILayout.EndVertical();
    }

    private void DrawDeveloperOptions()
    {
        GUILayout.Label("Developer Options", EditorStyles.boldLabel);

        optionsScrollPosition = GUILayout.BeginScrollView(optionsScrollPosition);

        if (selectedModule != null)
        {
            for (int i = 0; i < selectedModule.developerOptions.Count; i++)
            {
                ModuleTemplate.DeveloperOption option = selectedModule.developerOptions[i];

                GUILayout.BeginVertical(EditorStyles.helpBox);

                GUILayout.BeginHorizontal();

                GUILayout.Label("Option Name", GUILayout.Width(120));
                option.name = EditorGUILayout.TextField(option.name, GUILayout.ExpandWidth(true));

                GUILayout.Label("Option Type", GUILayout.Width(80));
                option.optionType = (ModuleTemplate.OptionType)EditorGUILayout.EnumPopup("", (ModuleTemplate.OptionType)option.optionType, GUILayout.ExpandWidth(true));

                if (option.optionType == ModuleTemplate.OptionType.Dropdown)
                {
                    // Draw dropdown options
                    GUILayout.Label("Dropdown Options (comma-separated)", GUILayout.Width(200));
                    string dropdownOptions = string.Join(",", option.dropdownOptions ?? new string[0]);
                    dropdownOptions = EditorGUILayout.TextField(dropdownOptions);
                    option.dropdownOptions = dropdownOptions.Split(',');
                    option.selectedDropdownIndex = EditorGUILayout.Popup("Selected Option", option.selectedDropdownIndex, option.dropdownOptions); // Fixed property name
                }

                GUILayout.EndHorizontal();

                // Rest of your existing code for drawing developer options...

                GUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Developer Option"))
            {
                selectedModule.developerOptions.Add(new ModuleTemplate.DeveloperOption());
            }

            if (GUILayout.Button("Remove Last Developer Option") && selectedModule.developerOptions.Count > 0)
            {
                selectedModule.developerOptions.RemoveAt(selectedModule.developerOptions.Count - 1);
            }
        }

        GUILayout.EndScrollView();
    }

    private void DrawModulePreview()
    {
        GUILayout.BeginVertical(GUILayout.Width(250));

        GUILayout.Label("Module Preview", EditorStyles.boldLabel);

        // Draw module preview content (non-interactive)

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

        // Set the module name based on user input
        newModule.moduleName = newModuleName;

        // Add the new module to the list
        modules.Add(newModule);

        // Select the newly created module
        selectedModule = newModule;

        // Generate a unique path within the MODULES directory
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{newModuleName}.asset");

        // Create and save the new module asset
        AssetDatabase.CreateAsset(newModule, assetPath);
        AssetDatabase.SaveAssets();

        // Refresh the AssetDatabase after creation
        AssetDatabase.Refresh();
    }

    private void DeleteModule()
    {
        if (selectedModule != null && modules.Contains(selectedModule))
        {
            modules.Remove(selectedModule);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(selectedModule));
            selectedModule = null;
        }
    }

    private void SaveModule()
    {
        if (selectedModule != null)
        {
            // Set the module name based on user input before saving
            selectedModule.moduleName = newModuleName;

            EditorUtility.SetDirty(selectedModule);
            AssetDatabase.SaveAssets();
        }
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
