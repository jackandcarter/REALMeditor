using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewModule", menuName = "Modules/ModuleTemplate")]
public class ModuleTemplate : ScriptableObject
{
    public string moduleName;
    public string moduleDescription;
    public UIElementBase[] uiElements;
    public List<DeveloperOption> developerOptions = new List<DeveloperOption>();

    public enum OptionType
    {
        Checkbox,
        Dropdown,
        TextField,
        // Add more option types as needed.
    }

    [System.Serializable]
    public class DeveloperOption
    {
        public string name;
        public OptionType optionType;
        public bool isSelected; // Added to track option selection
        public string[] dropdownOptions; // Added for dropdown options
        public int selectedDropdownIndex; // Added for the selected dropdown index

        // Additional properties and methods as needed
        public string ScriptName
        {
            get
            {
                // Generate script name based on the option type and name
                return $"{name.Replace(" ", string.Empty)}{optionType}Script";
            }
        }
    }

    public bool IsDeveloperOptionSelected(string developerOptionName)
    {
        foreach (var option in developerOptions)
        {
            if (option.name == developerOptionName && option.isSelected)
            {
                return true;
            }
        }
        return false;
    }

    // Add more methods or properties as needed
}
