using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Transformation : MonoBehaviour
{
    public float scaleSpeed = 0.01f;
    private Vector3 previousScale;
    private Vector3 firstPosition;
    private bool firstCalled = true;
    public Camera thisCam;
    public GameObject moveArrow = null;
    public GameObject rotateArrow = null;
    public GameObject scaleArrow = null;
    public Material transparentMaterial = null;
    private Material defaultMaterial;
    private GameObject clone, clone2, clone3;
    private string previousButton = null;
    private Vector3 scaleVector;
    private float scaleFactor;

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

    public void Update()
    {
        scaleFactor = (Camera.main.transform.position - transform.position).magnitude;
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();

        if (theChoiseOfThePlayerIs.buttonSelected != previousButton && previousButton != "" && this.tag == "selectedObject")
        {
            firstCalled = true;
            OnMouseDown();  
        }
        if (firstCalled == false)
        {
            scaleVector = transform.localScale;
            clone.transform.localScale = new Vector3((scaleFactor * 1.5f) / scaleVector[0], (scaleFactor * 1.5f) / scaleVector[1], (scaleFactor * 1.5f) / scaleVector[2]);
            clone2.transform.localScale = new Vector3((scaleFactor * 1.5f) / scaleVector[0], (scaleFactor * 1.5f) / scaleVector[1], (scaleFactor * 1.5f) / scaleVector[2]);
            clone3.transform.localScale = new Vector3((scaleFactor * 1.5f) / scaleVector[0], (scaleFactor * 1.5f) / scaleVector[1], (scaleFactor * 1.5f) / scaleVector[2]);
        }
        previousButton = theChoiseOfThePlayerIs.buttonSelected;
    }

    public void OnMouseDown() {
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        string selectedButton = theChoiseOfThePlayerIs.buttonSelected;
        GameObject[] tests = GameObject.FindGameObjectsWithTag("selectedObject");
        GameObject arrow = null;

        if (firstCalled == true && selectedButton != "") {
            defaultMaterial = GetComponent<MeshRenderer>().material;
        }
        if ((firstCalled == true && selectedButton != "") || (previousButton != null && previousButton != selectedButton)) {
            previousButton = selectedButton;
            //clear arrows from other selected gameobjects
            for (int i = 0; i < tests.Length; i++) {
                if (tests[i].tag == "selectedObject"){
                    tests[i].GetComponent<MeshRenderer>().material = defaultMaterial;
                    Destroy(tests[i].GetComponent<Transformation>().clone);
                    Destroy(tests[i].GetComponent<Transformation>().clone2);
                    Destroy(tests[i].GetComponent<Transformation>().clone3);
                    tests[i].GetComponent<Transformation>().firstCalled = true;
                    tests[i].tag = "Untagged";
                    tests[i].layer = LayerMask.NameToLayer("Default");
                }
                else
                    continue;
            }
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            GetComponent<MeshRenderer>().material = transparentMaterial;
            this.firstCalled = false;
            this.tag = "selectedObject";

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
            clone.transform.SetParent(transform);

            clone2 = Instantiate(arrow, transform.position, transform.rotation);
            clone2.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            clone2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            clone2.transform.SetParent(transform);

            clone3 = Instantiate(arrow, transform.position, transform.rotation);
            clone3.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            clone3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            clone3.transform.SetParent(transform);

            if (selectedButton == "Move") {
                clone.tag = "z_arrow";
                clone2.tag = "y_arrow";
                clone3.tag = "x_arrow";
                clone.transform.Rotate(0, 90, 0);
                clone2.transform.Rotate(0, 0, -90);
                clone3.transform.Rotate(0, 180, 0);
            }
            else if (selectedButton == "Rotate") {
                clone.tag = "z_rotate_arrow";
                clone2.tag = "y_rotate_arrow";
                clone3.tag = "x_rotate_arrow";
                clone.transform.Rotate(180, 0, 0);
                clone2.transform.Rotate(90, 0, 0);
                clone3.transform.Rotate(0, 270, 0);

            }
            else if (selectedButton == "Scale") {
                clone.tag = "z_scale_arrow";
                clone2.tag = "y_scale_arrow";
                clone3.tag = "x_scale_arrow";
                clone.transform.Rotate(0, 90, 0);
                clone2.transform.Rotate(0, 0, -90);
                clone3.transform.Rotate(0, 180, 0);

            }
        }
    }

    void OnMouseUp()
    {

    }
}

