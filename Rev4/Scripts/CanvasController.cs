// CanvasController.cs

using UnityEngine;
using System.Collections.Generic;

// This class manages a collection of UI elements and their interactions.
// It should be used within an editor window or similar context where the GUI can be drawn.
public class CanvasController : ScriptableObject
{
    // List to hold all UI elements in the canvas.
    public List<UIElementBase> uiElements = new List<UIElementBase>();

    // Method to add a new element to the canvas.
    public void AddElement(UIElementBase element)
    {
        if (element != null && !uiElements.Contains(element))
        {
            uiElements.Add(element);
        }
    }

    // Method to remove an existing element from the canvas.
    public void RemoveElement(UIElementBase element)
    {
        if (element != null && uiElements.Contains(element))
        {
            uiElements.Remove(element);
        }
    }

    // Method to draw all elements on the canvas.
    public void DrawCanvas()
    {
        foreach (var element in uiElements)
        {
            element.DrawElement();
        }
    }

    // Method to handle element selection, possibly for dragging.
    // It's a stub, details should be filled in based on specific implementation.
    public UIElementBase GetElementAtPosition(Vector2 position)
    {
        // Iterate through all elements to check if the position is within its bounds.
        foreach (var element in uiElements)
        {
            if (element.GetRect().Contains(position))
            {
                return element;
            }
        }

        return null; // Return null if no element is found at the position.
    }

    // Serialize the canvas state to allow saving/loading.
    public string SerializeCanvas()
    {
        // Implement serialization logic here.
        // This could be JSON, XML, or any format you choose.
        // You would serialize the properties of UIElements that are necessary to rebuild the canvas.
        throw new System.NotImplementedException();
    }

    // Deserialize a saved canvas state to restore UI elements.
    public void DeserializeCanvas(string serializedData)
    {
        // Implement deserialization logic here.
        // You would parse the serializedData and recreate the UIElements accordingly.
        throw new System.NotImplementedException();
    }

    // Additional methods related to canvas management can be implemented below.
    // ...
}
