// UITextFieldElement.cs

using UnityEngine;
using UnityEditor;

[System.Serializable]
public class UITextFieldElement : UIElementBase
{
    public string text;
    public System.Action<string> onValueChanged;

    public UITextFieldElement(string defaultText, System.Action<string> onValueChangedCallback)
    {
        this.text = defaultText;
        this.onValueChanged = onValueChangedCallback;
    }

    public override void DrawElement()
    {
        EditorGUI.BeginChangeCheck();
        text = EditorGUILayout.TextField("Text Field", text);
        if (EditorGUI.EndChangeCheck())
        {
            onValueChanged?.Invoke(text);
        }
    }
}
