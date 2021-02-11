using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public GameObject moveArrow = null;
    public GameObject rotateArrow = null;
    public GameObject scaleArrow = null;
    public GameObject scaleBox = null;
    public Material transparentMaterial = null;
    private bool firstCalled = true;
    private Material defaultMaterial;
    private GameObject clone, clone2, clone3, clone4;
    private string previousButton = null;
    private float scaleFactor;
    private GameObject mainCamera;
    private ButtonSelection theChoiseOfThePlayerIs;

    private Vector3 GetMouseWorldPos()
    {
        //Pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;
        Vector3 clickPosition = -Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return clickPosition = hit.point;
        }
        return clickPosition;
    }

    public void Awake()
    {
        mainCamera = GameObject.Find("MainCamera");
        theChoiseOfThePlayerIs = mainCamera.GetComponent<ButtonSelection>();
    }

    public void Update()
    {
        scaleFactor = (Camera.main.transform.position - transform.position).magnitude * 1.5f;

        if (theChoiseOfThePlayerIs.buttonSelected != previousButton && previousButton != "" && this.tag == "selectedObject")
        {
            firstCalled = true;
            OnMouseDown();
        }
        if (firstCalled == false)
        {
            clone.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            clone2.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            clone3.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            if (theChoiseOfThePlayerIs.buttonSelected == "Scale")
                clone4.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
        previousButton = theChoiseOfThePlayerIs.buttonSelected;
    }

    public void OnMouseDown() {
        string selectedButton = theChoiseOfThePlayerIs.buttonSelected;

        GameObject arrow = null;

        if (firstCalled == true && selectedButton != "") {
            defaultMaterial = GetComponent<MeshRenderer>().material;
        }
        if ((firstCalled == true && selectedButton != "") || (previousButton != null && previousButton != selectedButton) || (selectedButton == "Undo")) {
            GameObject previousSelectedObject = GameObject.FindGameObjectWithTag("selectedObject");
            if (previousSelectedObject != null && previousSelectedObject.tag == "selectedObject") //clear arrows from other selected gameobjects
            {
                previousSelectedObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                Destroy(previousSelectedObject.GetComponent<Transformation>().clone);
                Destroy(previousSelectedObject.GetComponent<Transformation>().clone2);
                Destroy(previousSelectedObject.GetComponent<Transformation>().clone3);
                previousSelectedObject.GetComponent<Transformation>().firstCalled = true;
                previousSelectedObject.tag = "Untagged";
                previousSelectedObject.layer = LayerMask.NameToLayer("Default");
                previousSelectedObject.transform.parent.gameObject.tag = "Untagged";
                if (previousButton == "Scale")
                {
                    Destroy(previousSelectedObject.GetComponent<Transformation>().clone4.gameObject);
                }
            }
            previousButton = selectedButton;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            this.firstCalled = false;
            this.tag = "selectedObject";
            GameObject parentBox = this.transform.parent.gameObject;
            parentBox.tag = "selectedParent";

            if (selectedButton != "Undo" && selectedButton != "Redo")
            {
                GetComponent<MeshRenderer>().material = transparentMaterial;

                if (selectedButton == "Move")
                {
                    arrow = moveArrow;
                }
                else if (selectedButton == "Rotate")
                {
                    arrow = rotateArrow;
                }
                else if (selectedButton == "Scale")
                {
                    arrow = scaleArrow;
                }

                clone = Instantiate(arrow, transform.position, transform.rotation);
                clone.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                clone.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                clone.transform.SetParent(parentBox.transform);

                clone2 = Instantiate(arrow, transform.position, transform.rotation);
                clone2.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                clone2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                clone2.transform.SetParent(parentBox.transform);

                clone3 = Instantiate(arrow, transform.position, transform.rotation);
                clone3.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                clone3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                clone3.transform.SetParent(parentBox.transform);

                if (selectedButton == "Move")
                {
                    clone.tag = "z_arrow";
                    clone2.tag = "y_arrow";
                    clone3.tag = "x_arrow";
                    clone.transform.Rotate(0, 90, 0);
                    clone2.transform.Rotate(0, 0, -90);
                    clone3.transform.Rotate(0, 180, 0);
                }
                else if (selectedButton == "Rotate")
                {
                    clone.tag = "z_rotate_arrow";
                    clone2.tag = "y_rotate_arrow";
                    clone3.tag = "x_rotate_arrow";
                    clone.transform.Rotate(180, 0, 0);
                    clone2.transform.Rotate(90, 0, 0);
                    clone3.transform.Rotate(0, 270, 0);
                }
                else if (selectedButton == "Scale")
                {
                    clone4 = Instantiate(scaleBox, transform.position, transform.rotation);
                    clone4.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                    clone4.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    clone4.transform.SetParent(parentBox.transform);

                    clone.tag = "z_scale_arrow";
                    clone2.tag = "y_scale_arrow";
                    clone3.tag = "x_scale_arrow";
                    clone4.tag = "scale_box";

                    clone.transform.Rotate(0, 90, 0);
                    clone2.transform.Rotate(0, 0, -90);
                    clone3.transform.Rotate(0, 180, 0);
                }
            }
            else if (selectedButton == "Undo" || selectedButton == "Redo")
            {
                StoreHistory myList = mainCamera.GetComponent<StoreHistory>();
                UndoRedo<ObjectState> secondList = myList.Get();
                this.firstCalled = true;
                if (secondList.GetUndoListCount() > 1 && selectedButton == "Undo")
                {
                    ObjectState previousState = secondList.PopFromUndoList();
                    GameObject Parent = GameObject.Find(previousState.Name);
                    GameObject Model = GameObject.Find(previousState.ModelName);
                    Parent.transform.localPosition = previousState.LocalPosition;
                    Parent.transform.localRotation = previousState.LocalRotation;
                    Model.transform.localScale = previousState.ModelScale;
                    theChoiseOfThePlayerIs.buttonSelected = previousState.ButtonSelected;
                    OnMouseDown();
                }
                else if (secondList.GetRedoListCount() > 0 && selectedButton == "Redo")
                {
                    ObjectState previousState = secondList.PopFromRedoList();
                    GameObject Parent = GameObject.Find(previousState.Name);
                    GameObject Model = GameObject.Find(previousState.ModelName);
                    Parent.transform.localPosition = previousState.LocalPosition;
                    Parent.transform.localRotation = previousState.LocalRotation;
                    Model.transform.localScale = previousState.ModelScale;
                    theChoiseOfThePlayerIs.buttonSelected = previousState.ButtonSelected;
                    OnMouseDown();
                }
            }
        }   
    }

    void OnMouseUp()
    {

    }
}

