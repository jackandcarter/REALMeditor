// UIElementBase.cs

using UnityEngine;

// Base class for all UI elements to be used within the editor tool.
public abstract class UIElementBase : ScriptableObject
{
    // This Rect is used for managing positions within the editor canvas.
    // It could be in a local space that the canvas controller translates into a window space.
    public Rect elementRect;

    // Draw the element on the canvas.
    public abstract void DrawElement();

    // This method returns the Rect that represents the element's position and size on the canvas.
    public Rect GetRect()
    {
        return elementRect;
    }

    // Call this method to update the element's position.
    public void SetPosition(Vector2 newPosition)
    {
        elementRect.position = newPosition;
    }

    // Additional abstract or virtual methods that derived classes must implement could be added here.
    // For example, serialization/deserialization methods for saving/loading state.
}
