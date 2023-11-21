# REALMeditor<BR>
& MODULEditor<BR>

Development for REALMeditor, a Unity Game Engine tool to help manage and create reusable RPG/MMO game assets.
<BR><BR>
REALMeditor is a tool that can help developers create prefabs and generate their logic using options in the editor's interface.<BR>
REALMeditor's UI is populated using the listed modules on the left side of the tool's window, these are created using MODULEditor.<BR>
MODULEditor is a sub editor for the creation of REALMeditor's UI and developer options. REALMeditor then loads your created modules and displays them in an organized fashion.<BR>
<BR>
REALMeditor is able to check which options are selected in the active module and compile a master script made up of each option's defined logic. <BR>
Essentially, we have REALMeditor which is the production tool, and MODULEditor which allows developers to create or extend REALMedtior's options and functionality.
<BR><BR>
Lets say you need an NPC Maker that can create more than one type of NPC prefab, this can be achieved by creating the NPC Maker Module using MODULEditor, creating the module's contents and adding elements such as drop down menus, and adding developer option checkboxes for different NPC types and creating a dropdown menu for them, and assigning sub options that appear under those menu items, such as adding a checkbox that adds a vision cone logic, which only appears if selecting the Patrol NPC type in the dropdown.<BR>
<BR>
This is just an example of how you could make an NPC Maker as a sub-editor module for REALMeditor. REALMeditor and MODULEditor are coded to automatically reflect and update modules as they are created, modified, or deleted, and will create the MODULES folder in your assets folder upon creating your first Module, both editors use this folder.<BR>

This is work in progress to implement more functionality and expand on the methods used to generate the C# scripts. Work has been visibly started on the Preview pane to show what the module will look like as you build it. There are two buttons for creating a prefab, they do have different save functions at the moment and are being developed independantly to see which one is more effective, one will eventually be removed or reimplemented.<BR>
Currently, REALMeditor is able to save prefabs and generate a master C# script either by themselves or together.<BR>
When viewing your NPC maker in REALMeditor you will have a button to save the prefab and generate the attached script, these buttons are on the bottom of REALMeditor and stay accross all modules as the MODULEditor makes all developer options serializable and REALMeditor compiles only the options in the currently displayed module, allowing you to bypass any script generation logic when creating your module's options.
<BR><BR>
Included are both REALMeditor and MODULEditor for the creation of a customizable RPG/MMO creation tool created by developers, for developers.
