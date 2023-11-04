using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[System.Serializable]
public class CompoundUIElement : ModuleUIElement
{
    public List<ModuleUIElement> children = new List<ModuleUIElement>();

    public override void OnGUI()
    {
        EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
        foreach (var child in children)
        {
            child.OnGUI();
        }
    }

    public void AddChild(ModuleUIElement child)
    {
        children.Add(child);
    }
}
