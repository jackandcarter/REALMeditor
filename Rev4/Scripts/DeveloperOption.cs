using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DeveloperOption
{
    public string optionName;
    public OptionType optionType;
    public string logicScriptName;
    public bool isChecked;
    public string logicCode;  // New property to hold logic code

    // Additional properties for UI customization
    public Color color;
    public int fontSize;
    public FontStyle fontStyle;

    // Additional properties for specific option types
    public string textFieldText;

    // New property for foldout state
    public bool isExpanded;

    // Example method to retrieve the selected logic script.
    public string GetSelectedLogicScript()
    {
        return $"Scripts/Logic/{logicScriptName}.cs";
    }
}

[System.Serializable]
public class DropdownDeveloperOption
{
    public string optionName;
    public List<DeveloperOption> options;  // List of developer options for this dropdown
    public int selectedOptionIndex;  // Index of the selected option

    // Other properties as needed
}

public enum OptionType
{
    Checkbox,
    Dropdown,
    TextField,
    // Add more option types as needed.
}
