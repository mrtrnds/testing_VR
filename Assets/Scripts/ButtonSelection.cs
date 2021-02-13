using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelection : MonoBehaviour
{
    public string buttonSelected = "";
    public Texture2D cursorTexture_annotation;
    public Texture2D cursorTexture_move;
    public Texture2D cursorTexture_scale;
    public Texture2D cursorTexture_rotate;
    private CursorMode cursorMode = CursorMode.Auto;

    // This function is called when the buttons are pressed and passed the name of the button
    // as a variable to the function.
    public void SetButton(string buttonName)
    {
        buttonSelected = buttonName;
        //Destroy (clone);

        if (buttonName == "Undo")
        {
            Cursor.SetCursor(cursorTexture_annotation, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Redo")
        {
            Cursor.SetCursor(cursorTexture_scale, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Scale")
        {
            Cursor.SetCursor(cursorTexture_scale, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Rotate")
        {
            Cursor.SetCursor(cursorTexture_rotate, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Move")
        {
            Cursor.SetCursor(cursorTexture_move, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Annotation")
        {
            Cursor.SetCursor(cursorTexture_move, Vector2.zero, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
