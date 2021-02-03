using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Transformation : MonoBehaviour
{
    public GameObject moveArrow = null;
    public GameObject rotateArrow = null;
    public GameObject scaleArrow = null;
    public Material transparentMaterial = null;
    private bool firstCalled = true;
    private Material defaultMaterial;
    private GameObject clone, clone2, clone3;
    private string previousButton = null;
    private Vector3 scaleVector;
    private float scaleFactor;
    private Vector3 initScaleVector;

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
    public void Start()
    {
        initScaleVector = transform.localScale;
    }

    public void Update()
    {
        scaleFactor = (Camera.main.transform.position - transform.position).magnitude;
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        scaleVector = initScaleVector;//arxiko scale 2,2,2 h 1,1,1 h 4,3,4

        if (theChoiseOfThePlayerIs.buttonSelected != previousButton && previousButton != "" && this.tag == "selectedObject")
        {
            firstCalled = true;
            OnMouseDown();  
        }
        if (firstCalled == false)
        {
            scaleVector[0] = (scaleFactor * 1.5f);
            scaleVector[1] = (scaleFactor * 1.5f);
            scaleVector[2] = (scaleFactor * 1.5f);

            clone.transform.localScale = new Vector3(scaleVector[0],  scaleVector[1], scaleVector[2]);
            clone2.transform.localScale = new Vector3(scaleVector[0], scaleVector[1], scaleVector[2]);
            clone3.transform.localScale = new Vector3(scaleVector[0], scaleVector[1], scaleVector[2]);
        }
        previousButton = theChoiseOfThePlayerIs.buttonSelected;
    }

    public void OnMouseDown() {
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        string selectedButton = theChoiseOfThePlayerIs.buttonSelected;
        GameObject arrow = null;

        if (firstCalled == true && selectedButton != "") {
            defaultMaterial = GetComponent<MeshRenderer>().material;
        }
        if ((firstCalled == true && selectedButton != "") || (previousButton != null && previousButton != selectedButton)) {
            GameObject[] tests = GameObject.FindGameObjectsWithTag("selectedObject");
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
                    tests[i].transform.parent.gameObject.tag = "Untagged";
                }
                else
                    continue;
            }
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            GetComponent<MeshRenderer>().material = transparentMaterial;
            this.firstCalled = false;
            this.tag = "selectedObject";
            GameObject parentBox = this.transform.parent.gameObject;
            parentBox.tag = "selectedParent";


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
            float rescale_clone_x = (scaleFactor * 1.5f);
            float rescale_clone_y = (scaleFactor * 1.5f);
            float rescale_clone_z = (scaleFactor * 1.5f);

            clone.transform.localScale = new Vector3(rescale_clone_x, rescale_clone_y, rescale_clone_z);
            clone.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            clone.transform.SetParent(parentBox.transform);

            clone2 = Instantiate(arrow, transform.position, transform.rotation);
            clone2.transform.localScale = new Vector3(rescale_clone_x, rescale_clone_y, rescale_clone_z);
            clone2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            clone2.transform.SetParent(parentBox.transform);

            clone3 = Instantiate(arrow, transform.position, transform.rotation);
            clone3.transform.localScale = new Vector3(rescale_clone_x, rescale_clone_y, rescale_clone_z);
            clone3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            clone3.transform.SetParent(parentBox.transform);

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

