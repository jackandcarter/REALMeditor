using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewModule", menuName = "MODULEditor/ModuleTemplate", order = 1)]
public class ModuleTemplate : ScriptableObject
{
    public string moduleName;
    public List<ModuleUIElement> uiElements;

    private void OnEnable()
    {
        if (uiElements == null)
        {
            uiElements = new List<ModuleUIElement>();
        }
    }
    
    // Methods to manage the UI elements...
}
