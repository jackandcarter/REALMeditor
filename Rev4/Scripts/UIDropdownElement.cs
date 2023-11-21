using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UIDropdownElement : UIElementBase
{
    public Dictionary<string, List<string>> optionsDictionary;
    public string selectedKey;
    public int selectedIndex;
    public System.Action<int> onValueChanged;

    public UIDropdownElement(Dictionary<string, List<string>> options, int defaultIndex, System.Action<int> onValueChangedCallback)
    {
        this.optionsDictionary = options;
        this.selectedKey = options.Keys.First(); // Using LINQ First() to get the first key
        this.selectedIndex = defaultIndex;
        this.onValueChanged = onValueChangedCallback;
    }

    public override void DrawElement()
    {
        EditorGUI.BeginChangeCheck();

        // Convert KeyCollection to a List<string> before calling ToList
        List<string> keyList = new List<string>(optionsDictionary.Keys);
        int selectedKeyIndex = EditorGUILayout.Popup("Dropdown", keyList.IndexOf(selectedKey), keyList.ToArray());

        // Convert List<string> to an array before calling ToArray
        string[] selectedValues = optionsDictionary[keyList[selectedKeyIndex]].ToArray();

        selectedIndex = EditorGUILayout.Popup("Select Value", selectedIndex, selectedValues);

        if (EditorGUI.EndChangeCheck())
        {
            onValueChanged?.Invoke(selectedIndex);
        }
    }
}
