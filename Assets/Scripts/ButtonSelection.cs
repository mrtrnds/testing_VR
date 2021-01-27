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
    //public GameObject move_arrow;
    //private GameObject clone;
    private CursorMode cursorMode = CursorMode.Auto;

    // This function is called when the buttons are pressed and passed the name of the button
    // as a variable to the function.
    public void SetButton(string buttonName)
    {
        buttonSelected = buttonName;
        //Destroy (clone);
 
        if (buttonName == "Annotation")
        {
            Cursor.SetCursor(cursorTexture_annotation, Vector2.zero, cursorMode);
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
            //GameObject theParentObject = GameObject.Find("EditorOnly");
            //GameObject clone = Instantiate(move_arrow, theParentObject.transform.position, theParentObject.transform.rotation);
            //clone.transform.SetParent(theParentObject.transform);
            //clone.transform.Rotate(0, 90, 0);
            Cursor.SetCursor(cursorTexture_move, Vector2.zero, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
