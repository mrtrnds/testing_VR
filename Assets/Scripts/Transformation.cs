﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Transformation : MonoBehaviour
{
    public float scaleSpeed = 0.01f;
    private Vector3 previousScale;
    private Vector3 firstPosition;
    private bool firstCalled = true;
    public Camera thisCam;
    public GameObject moveArrow;
    public GameObject rotateArrow;
    public GameObject scaleArrow;
    public Material transparentMaterial;
    private Material defaultMaterial;
    private GameObject clone, clone2, clone3;
    private string previousButton = null;

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

    public void OnMouseDown()
    {
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        Transformation [] tests = FindObjectsOfType<Transformation>();
        if (firstCalled == true)
        {
            defaultMaterial = GetComponent<MeshRenderer>().material;
        }
        if (firstCalled == true || (previousButton != null && previousButton != theChoiseOfThePlayerIs.buttonSelected))
        {
            previousButton = theChoiseOfThePlayerIs.buttonSelected;
            //clear arrow from other selected models
            for (int i = 0; i < tests.Length; i++)
            {
                tests[i].GetComponent<MeshRenderer>().material = defaultMaterial;
                Destroy(tests[i].GetComponent<Transformation>().clone);
                Destroy(tests[i].GetComponent<Transformation>().clone2);
                Destroy(tests[i].GetComponent<Transformation>().clone3);
                tests[i].firstCalled = true;
            }
            GetComponent<MeshRenderer>().material = transparentMaterial;
            firstCalled = false;

            if (theChoiseOfThePlayerIs.buttonSelected == "Move")
            {
                //arrow aksona Z
                clone = Instantiate(moveArrow, transform.position, transform.rotation);
                clone.transform.SetParent(transform);
                clone.tag = "z_arrow";
                clone.transform.Rotate(0, 90, 0);
                clone.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

                //arrow aksona Y
                clone2 = Instantiate(moveArrow, transform.position, transform.rotation);
                clone2.transform.SetParent(transform);
                clone2.tag = "y_arrow";
                clone2.transform.Rotate(0, 0, -90);
                clone2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                //arrow aksona X
                clone3 = Instantiate(moveArrow, transform.position, transform.rotation);
                clone3.transform.SetParent(transform);
                clone3.tag = "x_arrow";
                clone3.transform.Rotate(0, 180, 0);
                clone3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            if (theChoiseOfThePlayerIs.buttonSelected == "Rotate")
            {

                //arrow aksona Z
                clone = Instantiate(rotateArrow, transform.position, transform.rotation);
                clone.transform.SetParent(transform);

                Mesh mesh = GetComponent<MeshFilter>().mesh;
                Vector3 bounds = mesh.bounds.max - mesh.bounds.min;

                float maxLength = .0f;
                for (int i = 0; i < 3; i++)
                {
                    if (maxLength < bounds[i])
                        maxLength = bounds[i];
                }

                float initScalingFactor = clone.transform.localScale[0];
                Vector3 boundsOfArrows = clone.GetComponent<MeshFilter>().mesh.bounds.max * initScalingFactor - clone.GetComponent<MeshFilter>().mesh.bounds.min * initScalingFactor;
                float maxLengthOfArrows = .0f;
                for (int i = 0; i < 3; i++)
                {
                    if (maxLengthOfArrows < boundsOfArrows[i])
                        maxLengthOfArrows = boundsOfArrows[i];
                }

                initScalingFactor = initScalingFactor * (maxLength / maxLengthOfArrows) + initScalingFactor * (maxLength / maxLengthOfArrows) * 0.8f;

                clone.tag = "z_rotate_arrow";
                clone.transform.localScale = new Vector3(initScalingFactor, initScalingFactor, initScalingFactor * 0.5f);
                //clone.transform.localPosition = Vector3.zero; // Or desired position
                clone.transform.Rotate(180, 0, 0);
                clone.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

                //arrow aksona Y
                clone2 = Instantiate(rotateArrow, transform.position, transform.rotation);
                clone2.transform.SetParent(transform);
                clone2.transform.localScale = new Vector3(initScalingFactor, initScalingFactor, initScalingFactor);
                clone2.tag = "y_rotate_arrow";
                clone2.transform.Rotate(90, 0, 0);
                clone2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                //arrow aksona X
                clone3 = Instantiate(rotateArrow, transform.position, transform.rotation);
                clone3.transform.SetParent(transform);
                clone3.transform.localScale = new Vector3(initScalingFactor, initScalingFactor, initScalingFactor);
                clone3.tag = "x_rotate_arrow";
                clone3.transform.Rotate(0, 270, 0);
                clone3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            if (theChoiseOfThePlayerIs.buttonSelected == "Scale")
            {
                //arrow aksona Z
                clone = Instantiate(scaleArrow, transform.position, transform.rotation);
                clone.transform.SetParent(transform);
                clone.tag = "z_scale_arrow";
                clone.transform.Rotate(0, 90, 0);

                //arrow aksona Y
                clone2 = Instantiate(scaleArrow, transform.position, transform.rotation);
                clone2.transform.SetParent(transform);
                clone2.tag = "y_scale_arrow";
                clone2.transform.Rotate(0, 0, -90);

                //arrow aksona X
                clone3 = Instantiate(scaleArrow, transform.position, transform.rotation);
                clone3.transform.SetParent(transform);
                clone3.tag = "x_scale_arrow";
                clone3.transform.Rotate(0, 180, 0);
            }
        }
    }

    private void OnMouseDrag()
    {
        GameObject thePlayerIs = GameObject.Find("MainCamera");
        ButtonSelection theChoiseOfThePlayerIs = thePlayerIs.GetComponent<ButtonSelection>();
        ToolSelection theToolSelected = thePlayerIs.GetComponent<ToolSelection>();

        //Scale
        if (theChoiseOfThePlayerIs.buttonSelected == "Scale")
        {
            print("Scale");
        }
        //Rotate
        else if (theChoiseOfThePlayerIs.buttonSelected == "Rotate")
        {
            print("paokara");
        }
        // Move
        else if (theChoiseOfThePlayerIs.buttonSelected == "Move")
        {
            print("aekaar");
        }
        else
            Destroy(clone);
    }
    void OnMouseUp()
    {
    }
}

