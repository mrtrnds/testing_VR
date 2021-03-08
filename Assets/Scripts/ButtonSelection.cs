//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class ButtonSelection : MonoBehaviour
{
    public string buttonSelected = "";
    //public Texture2D cursorTexture_annotation;
    //public Texture2D cursorTexture_move;
    //public Texture2D cursorTexture_scale;
    //public Texture2D cursorTexture_rotate;
    //private CursorMode cursorMode = CursorMode.Auto;

    public Button buttonUndo;
    public Button buttonRedo;
    public Button buttonScale;
    public Button buttonRotate;
    public Button buttonMove;
    public Button buttonAnnotation;

    public Color initColor;
    public Color selectedColor;

    private ButtonSelection theChoiseOfThePlayerIs;
    private GameObject mainCamera;
    private bool isButtonMoveEnable = true;
    private bool isButtonRotateEnable = false;
    private bool isButtonScaleEnable = false;

    // This function is called when the buttons are pressed and passed the name of the button
    // as a variable to the function.
    public void Awake()
    {
        buttonMove.image.color = selectedColor;
        mainCamera = GameObject.Find("MainCamera");
        theChoiseOfThePlayerIs = mainCamera.GetComponent<ButtonSelection>();
    }

    public void SetButton(string buttonName)
    {
        buttonUndo.image.color = initColor;
        buttonRedo.image.color = initColor;
        buttonScale.image.color = initColor;
        buttonRotate.image.color = initColor;
        buttonMove.image.color = initColor;
        buttonAnnotation.image.color = initColor;

        if (buttonName == "Undo")
        {
            isButtonMoveEnable = false;
            isButtonRotateEnable = false;
            isButtonScaleEnable = false;
            buttonSelected = buttonName;
            buttonUndo.image.color = selectedColor;
            StoreHistory myList = mainCamera.GetComponent<StoreHistory>();
            UndoRedo<ObjectState> secondList = myList.Get();
            if (secondList.GetUndoListCount() > 0)
            {
                buttonUndo.image.color = selectedColor;

                ObjectState previousState = secondList.PopFromUndoList();
                GameObject Parent = GameObject.Find(previousState.Name);
                GameObject Model = GameObject.Find(previousState.ModelName);
                Parent.transform.localPosition = previousState.LocalPosition;
                Parent.transform.localRotation = previousState.LocalRotation;
                Model.transform.localScale = previousState.ModelScale;
                Model.GetComponent<Transformation>().OnMouseDown();
                Model.GetComponent<Transformation>().disableTools();
            }
            //Cursor.SetCursor(cursorTexture_annotation, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Redo")
        {
            isButtonMoveEnable = false;
            isButtonRotateEnable = false;
            isButtonScaleEnable = false;
            buttonSelected = buttonName;
            buttonRedo.image.color = selectedColor;
            StoreHistory myList = mainCamera.GetComponent<StoreHistory>();
            UndoRedo<ObjectState> secondList = myList.Get();
            if (secondList.GetRedoListCount() > 0)
            {
                ObjectState previousState = secondList.PopFromRedoList();
                GameObject Parent = GameObject.Find(previousState.Name);
                GameObject Model = GameObject.Find(previousState.ModelName);
                Parent.transform.localPosition = previousState.LocalPosition;
                Parent.transform.localRotation = previousState.LocalRotation;
                Model.transform.localScale = previousState.ModelScale;
                Model.GetComponent<Transformation>().OnMouseDown();
                Model.GetComponent<Transformation>().disableTools();

            }
            //Cursor.SetCursor(cursorTexture_scale, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Scale")
        {
            if (isButtonScaleEnable == false)
            {
                buttonSelected = buttonName;
                buttonScale.image.color = selectedColor;
                isButtonScaleEnable = true;
                isButtonMoveEnable = false;
                isButtonRotateEnable = false;
}
            else
            {
                GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                isButtonScaleEnable = false;
                buttonSelected = "None";
                if (Model != null)
                    Model.GetComponent<Transformation>().OnMouseDown();
            }

            //Cursor.SetCursor(cursorTexture_scale, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Rotate")
        {

            if (isButtonRotateEnable == false)
            {
                buttonSelected = buttonName;
                buttonRotate.image.color = selectedColor;
                isButtonRotateEnable = true;
                isButtonScaleEnable = false;
                isButtonMoveEnable = false;
            }
            else
            {
                GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                isButtonRotateEnable = false;
                buttonSelected = "None";
                if (Model != null)
                    Model.GetComponent<Transformation>().OnMouseDown();
            }
            //Cursor.SetCursor(cursorTexture_rotate, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Move")
        {
            if (isButtonMoveEnable == false)
            {
                buttonSelected = buttonName;
                buttonMove.image.color = selectedColor;
                isButtonMoveEnable = true;
                isButtonRotateEnable = false;
                isButtonScaleEnable = false;
            }
            else
            {
                GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                isButtonMoveEnable = false;
                buttonSelected = "None";
                if (Model != null)
                    Model.GetComponent<Transformation>().OnMouseDown();
            }

            //Cursor.SetCursor(cursorTexture_move, Vector2.zero, cursorMode);
        }
        else if (buttonName == "Annotation")
        {
            isButtonMoveEnable = false;
            isButtonRotateEnable = false;
            isButtonScaleEnable = false;
            buttonSelected = buttonName;
            buttonAnnotation.image.color = selectedColor;

            //Cursor.SetCursor(cursorTexture_move, Vector2.zero, cursorMode);
        }
        else
        {
            //Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
