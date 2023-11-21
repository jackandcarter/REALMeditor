# REALMeditor
Development for REALMeditor, a Unity Game Engine tool to help manage and create reusable RPG/MMO game assets.
<BR><BR>
REALMeditor is a tool that can help create prefabs, generate scripts, and assets based on the UI options you select. REALMeditor's UI is populated using modules that are created using MODULEditor.<BR>
REALMeditor is able to check which options are selected in the active module and compile a master script made up of each option's defined logic. Essentially we have REALMeditor, the production tool, and MODULEditor which allows developers to create or extend REALMedtior's options and functionality.
<BR>
<BR>
MODULEditor is a sub editor for the creation of REALMeditor's UI and developer options. REALMeditor then loads your created modules and displays them in an organized fashion.
<BR><BR>
An example would be using an NPC maker module within REALMeditor, this can be achieved by creating the Module for the NPC maker using MODULEditor, organizing the modules UI elements and adding elements such as drop down menus, and adding developer options.<BR>
<BR>
This is work in progress to implement more functionality and expand on the methods used to generate the C# scripts. Currently, REALMeditor is able to save prefabs and generate a master C# script either by themselves or together.

<BR><BR>
Lets say you want to create an NPC Maker module, you can define the elements in your module using MODULEditor's interface and attach logic scripts to those options. When viewing your NPC maker in REALMeditor you will have a button to save the prefab and generate the attached script waiting at the bottom accross all modules as the MODULEditor makes all developer options serializable, allowing you to bypass any script generation logic when creating your module's options.
<BR><BR>
Included are both REALMeditor and MODULEditor for the creation of a customizable RPG/MMO creation tool created by developers, for developers.
