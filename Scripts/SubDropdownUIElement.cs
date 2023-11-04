[System.Serializable]
public class SubDropdownUIElement : ModuleUIElement
{
    public List<string> options = new List<string>();
    public List<MonoScript> attachedScripts = new List<MonoScript>();

    public override void OnGUI()
    {
        // Implement the GUI for the sub-dropdown here
        for (int i = 0; i < options.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            options[i] = EditorGUILayout.TextField("Option " + (i + 1), options[i]);
            attachedScripts[i] = (MonoScript)EditorGUILayout.ObjectField(
                "Attach Script",
                attachedScripts[i],
                typeof(MonoScript),
                false
            );
            EditorGUILayout.EndHorizontal();
        }
    }
}
