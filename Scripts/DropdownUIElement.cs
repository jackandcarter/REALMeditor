using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class DropdownUIElement : ModuleUIElement
{
    public List<string> options = new List<string>();
    public int selectedIndex = 0; // Ensure it's initialized
    public List<List<ModuleUIElement>> subElements = new List<List<ModuleUIElement>>();

    public override void OnGUI()
    {
        // Ensure the dropdown is only drawn if there are options available
        if (options.Count > 0)
        {
            selectedIndex = EditorGUILayout.Popup(name, selectedIndex, options.ToArray());
        }
        else
        {
            EditorGUILayout.LabelField(name, "No options available.");
        }

        // Display sub-elements if any and if the dropdown is selected
        if (selectedIndex >= 0 && selectedIndex < options.Count && selectedIndex < subElements.Count)
        {
            var subList = subElements[selectedIndex];
            for (int i = 0; i < subList.Count; i++)
            {
                // Use isExpanded from the ModuleUIElement to create a foldout
                subList[i].isExpanded = EditorGUILayout.Foldout(subList[i].isExpanded, subList[i].name);
                if (subList[i].isExpanded)
                {
                    EditorGUI.indentLevel++; // Indent the sub-elements for better UI hierarchy
                    subList[i].OnGUI();
                    EditorGUI.indentLevel--; // Reset the indentation
                }
            }
        }
    }

    // Function to add a named CheckboxUIElement to the currently selected dropdown option
    public void AddCheckbox(string checkboxName)
    {
        // Guard clause for a valid selected index
        if (selectedIndex < 0 || selectedIndex >= options.Count) return;

        // Ensure the sub-elements list is properly initialized up to the selected index
        while (subElements.Count <= selectedIndex)
        {
            subElements.Add(new List<ModuleUIElement>());
        }

        CheckboxUIElement checkbox = new CheckboxUIElement();
        checkbox.name = checkboxName;
        subElements[selectedIndex].Add(checkbox); // Add to the correct sublist
    }
}
