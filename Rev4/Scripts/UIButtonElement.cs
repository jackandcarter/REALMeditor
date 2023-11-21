// UIButtonElement.cs

using UnityEngine;
using UnityEditor;

[System.Serializable]
public class UIButtonElement : UIElementBase
{
    public string buttonText = "New Button";
    public Color buttonColor = Color.white;
    public System.Action onButtonPressed;

    public UIButtonElement(string text, Color color, System.Action callback)
    {
        buttonText = text;
        buttonColor = color;
        onButtonPressed = callback;
    }

    public override void DrawElement()
    {
        GUI.backgroundColor = buttonColor;
        if (GUILayout.Button(buttonText))
        {
            onButtonPressed?.Invoke();
        }
        GUI.backgroundColor = Color.white;
    }
}
