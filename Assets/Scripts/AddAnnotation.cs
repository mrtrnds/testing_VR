using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddAnnotation : MonoBehaviour
{
    bool isOpen = false;
    public string stringToEdit = "Add Comment";

    void OnGUI() //I think this must be used on the camera so you may have to reference a gui controller on the camera
    {
        if (isOpen) //Is it Open?
        {
            stringToEdit = GUI.TextField(new Rect(10, 10, 100, 50), stringToEdit, 300);//Display and use the Yes button
            if (GUI.Button(new Rect(90, 90, 20, 20), "Save"))
            {
                Debug.Log("Yes");
                isOpen = false;
            }
        }
    }

    void OnMouseDown() //Get the mouse click
    {
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        if (theChoiseOfThePlayerIs.buttonSelected == "Annotation")
        {
            isOpen = true;   //Set the buttons to appear
        }
    }


}