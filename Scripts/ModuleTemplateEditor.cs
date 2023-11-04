using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ModuleTemplate))]
public class ModuleTemplateEditor : Editor
{
    private Vector2 scrollPosition;

    public override void OnInspectorGUI()
    {
        ModuleTemplate module = (ModuleTemplate)target;

        EditorGUI.BeginChangeCheck();

        module.moduleName = EditorGUILayout.TextField("Module Name", module.moduleName);

        EditorGUILayout.Space();

        // Start a scroll view to handle lots of elements
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.LabelField("UI Elements", EditorStyles.boldLabel);

        for (int i = 0; i < module.uiElements.Count; i++)
        {
            EditorGUILayout.BeginVertical("box"); // Wrap each element in a box for better visibility

            ModuleUIElement element = module.uiElements[i];
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Element " + (i + 1), GUILayout.Width(70));
            element.name = EditorGUILayout.TextField("Name", element.name);

            // Call the specific UI element's GUI method
            element.OnGUI();

            // Add a button to remove the element
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                module.uiElements.RemoveAt(i);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                continue; // Skip the rest of the loop to avoid errors since we removed an element
            }

            EditorGUILayout.EndHorizontal();

            if (element is CheckboxUIElement checkbox)
            {
                // The corrected property for script
                checkbox.scriptToAttach = (MonoScript)EditorGUILayout.ObjectField(
                    "Attach Script",
                    checkbox.scriptToAttach,
                    typeof(MonoScript),
                    false
                );
            }

            if (element is DropdownUIElement dropdown)
            {
                RenderDropdownElement(dropdown);
            }

            EditorGUILayout.EndVertical(); // End the vertical box
        }

        // End the scroll view
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        // Add buttons to add new elements
        RenderAddElementButtons(module);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(module);
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void RenderDropdownElement(DropdownUIElement dropdown)
    {
        // Dropdown options with a manageable vertical layout
        for (int j = 0; j < dropdown.options.Count; j++)
        {
            EditorGUILayout.BeginHorizontal();
            dropdown.options[j] = EditorGUILayout.TextField("Option " + (j + 1), dropdown.options[j]);
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                dropdown.options.RemoveAt(j);
                dropdown.subElements.RemoveAt(j);
                EditorGUILayout.EndHorizontal();
                break; // Exit the loop as the list has changed
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Option"))
        {
            dropdown.options.Add("");
            dropdown.subElements.Add(new List<ModuleUIElement>());
        }

        EditorGUILayout.Space();

        // Display sub-elements for the selected dropdown option
        if (dropdown.selectedIndex < dropdown.subElements.Count)
        {
            EditorGUILayout.BeginVertical("box"); // Encapsulate the sub-elements in a box
            EditorGUILayout.LabelField("Sub-Elements for: " + dropdown.options[dropdown.selectedIndex]);

            for (int k = 0; k < dropdown.subElements[dropdown.selectedIndex].Count; k++)
            {
                ModuleUIElement subElement = dropdown.subElements[dropdown.selectedIndex][k];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Name", GUILayout.Width(50));
                subElement.name = EditorGUILayout.TextField(subElement.name, GUILayout.Width(100));
                subElement.OnGUI();

                // Removal button for sub-elements
                if (GUILayout.Button("Remove Sub-Element", GUILayout.Width(150)))
                {
                    dropdown.subElements[dropdown.selectedIndex].RemoveAt(k);
                    EditorGUILayout.EndHorizontal();
                    break; // Break out of the loop because the list has changed
                }
                EditorGUILayout.EndHorizontal();
            }

            RenderAddSubElementButtons(dropdown); // Button logic to add sub-elements

            EditorGUILayout.EndVertical(); // End the vertical box
        }
    }

    private void RenderAddElementButtons(ModuleTemplate module)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Checkbox"))
        {
            module.uiElements.Add(new CheckboxUIElement());
        }
        if (GUILayout.Button("Add Text Field"))
        {
            module.uiElements.Add(new TextFieldUIElement());
        }
        if (GUILayout.Button("Add Dropdown"))
        {
            module.uiElements.Add(new DropdownUIElement
            {
                options = new List<string> { "" },
                subElements = new List<List<ModuleUIElement>> { new List<ModuleUIElement>() }
            });
        }
        EditorGUILayout.EndHorizontal();
    }

    private void RenderAddSubElementButtons(DropdownUIElement dropdown)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Checkbox Sub-Element"))
        {
            CheckboxUIElement newCheckbox = new CheckboxUIElement { name = "New Checkbox" };
            dropdown.subElements[dropdown.selectedIndex].Add(newCheckbox);
        }
        // Repeat for other sub-element types as necessary
        EditorGUILayout.EndHorizontal();
    }
}
