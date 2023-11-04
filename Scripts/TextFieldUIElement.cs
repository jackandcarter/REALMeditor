using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TextFieldUIElement : ModuleUIElement
{
    public string text;

    public override void OnGUI()
    {
        text = EditorGUILayout.TextField(name, text);
    }
}
