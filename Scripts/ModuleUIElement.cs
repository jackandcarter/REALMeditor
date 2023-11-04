using UnityEngine;

[System.Serializable]
public abstract class ModuleUIElement
{
    public string name;
    public bool isExpanded = true; // Property to manage foldout state in the inspector

    public abstract void OnGUI();
}
