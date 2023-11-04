using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CheckboxUIElement : ModuleUIElement
{
    public bool isChecked;
    public MonoScript scriptToAttach; // The script to attach to the prefab if the checkbox is checked.

    public override void OnGUI()
    {
        isChecked = EditorGUILayout.Toggle(name, isChecked);

        if (isChecked)
        {
            // Allows assigning a MonoScript to this checkbox that represents a script to attach to the prefab
            scriptToAttach = EditorGUILayout.ObjectField("Attach Script", scriptToAttach, typeof(MonoScript), false) as MonoScript;
        }
    }
}
