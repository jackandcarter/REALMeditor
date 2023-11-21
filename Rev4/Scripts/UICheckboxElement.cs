// UICheckboxElement.cs

using UnityEngine;
using UnityEditor;

[System.Serializable]
public class UICheckboxElement : UIElementBase
{
    public string label = "New Checkbox";
    public bool isChecked;
    public System.Action<bool> onValueChanged;

    public UICheckboxElement(string label, bool defaultChecked, System.Action<bool> onValueChangedCallback)
    {
        this.label = label;
        this.isChecked = defaultChecked;
        this.onValueChanged = onValueChangedCallback;
    }

    public override void DrawElement()
    {
        EditorGUI.BeginChangeCheck();
        isChecked = EditorGUILayout.Toggle(label, isChecked);
        if (EditorGUI.EndChangeCheck())
        {
            onValueChanged?.Invoke(isChecked);
        }
    }
}
