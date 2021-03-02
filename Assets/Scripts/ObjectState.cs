using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState
{
    // The transform this data belongs to
    private Transform transform;
    private Vector3 localPosition;
    private Quaternion localRotation;
    private Vector3 localScale;
    private Vector3 modelScale;
    private string tag;
    private bool active;
    private string _buttonSelected;
    private string name;
    private string model_name;

    public ObjectState(GameObject obj, GameObject model, string buttonSelected)
    {
        transform = obj.transform;
        localPosition = transform.localPosition;
        localRotation = transform.localRotation;
        localScale = transform.localScale;
        modelScale = model.transform.localScale;
        tag = transform.tag;
        active = obj.activeSelf;
        name = obj.name;
        _buttonSelected = buttonSelected;
        model_name = model.name;
    }

    public void Apply()
    {
        GameObject modelGameObject = GameObject.FindGameObjectWithTag("selectedObject");
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
        transform.localScale = localScale;
        modelGameObject.transform.localScale = modelScale;
        transform.tag = tag;
        transform.gameObject.SetActive(active);
        transform.name = name;
    }

    public string Name   // property
    {
        get { return name; }   // get method
        set { name = value; }  // set method
    }

    public string ModelName
    {
        get { return model_name; }   // get method
        set { model_name = value; }  // set method
    }

    public Quaternion LocalRotation   // property
    {
        get { return localRotation; }   // get method
        set { localRotation = value; }  // set method
    }

    public Vector3 LocalPosition   // property
    {
        get { return localPosition; }   // get method
        set { localPosition = value; }  // set method
    }

    public Vector3 ModelScale
    {
        get { return modelScale; }   // get method
        set { modelScale = value; }  // set method
    }

public string ButtonSelected
    {
        get { return _buttonSelected; }   // get method
        set { _buttonSelected = value; }
    }
}