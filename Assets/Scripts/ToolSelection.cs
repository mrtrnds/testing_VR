using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelection : MonoBehaviour
{
    public float rotSpeed = 1.0f;
    public float scaleSpeed = 5000.0f;
    public float moveSpeed = 1.0f;

    private GameObject parentGameObject;
    private GameObject modelGameObject;

    private Vector3 firstParentPosition = Vector3.zero;
    private Vector3 firstModelScale = Vector3.zero;
    private Vector3 tempVector4 = Vector3.zero;
    private Vector3 previousScale = Vector3.zero;

    private string toolSelected = "";
    private bool firstcall = true;

    private ButtonSelection theChoiseOfThePlayerIs;
    GameObject mainCamera;

    void Start()
    {
        parentGameObject = GameObject.FindGameObjectWithTag("selectedParent");
        modelGameObject = GameObject.FindGameObjectWithTag("selectedObject");
        mainCamera = GameObject.Find("MainCamera");
    }

    private Vector3 GetWorldMousePosition(Vector3 inNormal, Vector3 inPoint)
    {
        Vector3 worldPosition = Vector3.zero;
        Vector3 m_DistanceFromCamera = new Vector3(inPoint.x, inPoint.y, inPoint.z);

        Plane plane = new Plane(inNormal, m_DistanceFromCamera);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            return worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }

    public void OnMouseDown()
    {
        List<string> listWithTags = new List<string>() {
        "x_arrow",
        "y_arrow",
        "z_arrow",
        "x_rotate_arrow",
        "y_rotate_arrow",
        "z_rotate_arrow",
        "x_scale_arrow",
        "y_scale_arrow",
        "z_scale_arrow",
        "scale_box"};

        Vector2 mousePoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            if (listWithTags.Contains(hit.collider.tag))
                toolSelected = hit.collider.tag;

        theChoiseOfThePlayerIs = mainCamera.GetComponent<ButtonSelection>();
        ObjectState currentState = new ObjectState(parentGameObject, modelGameObject, theChoiseOfThePlayerIs.buttonSelected);
        StoreHistory myList = mainCamera.GetComponent<StoreHistory>();
        UndoRedo<ObjectState> secondList = myList.Get();
        secondList.PushToUndoList(currentState);
    }

    private Vector3 calcOffset(Vector3 worldPosition, Vector3 normalVec)
    {
        return new Vector3(worldPosition.x * normalVec.x  + worldPosition.y * normalVec.y + worldPosition.z * normalVec.z  - (tempVector4.x * normalVec.x  + tempVector4.y * normalVec.y  + tempVector4.z * normalVec.z ), 0.0f, 0.0f).normalized * Time.deltaTime * moveSpeed;
    }

    private float calcScale(Vector3 worldPosition, Vector3 normalVec)
    {
        return ((worldPosition.x * normalVec.x + worldPosition.y * normalVec.y + worldPosition.z * normalVec.z) - (tempVector4.x * normalVec.x + tempVector4.y * normalVec.y + tempVector4.z * normalVec.z));
    }

    public void OnMouseDrag()
    {
        Vector3 offset = Vector3.zero;
        Vector3 worldPosition = GetWorldMousePosition(Vector3.forward, firstParentPosition);
        Vector3 normal = (Camera.main.transform.position - firstParentPosition).normalized;

        if (firstcall == true)
        {
            firstcall = false;
            firstParentPosition = parentGameObject.transform.position;
            tempVector4 = GetWorldMousePosition(normal, firstParentPosition);
            previousScale = modelGameObject.transform.localScale;
            return;
        }
        worldPosition = GetWorldMousePosition(normal, firstParentPosition);


        if (toolSelected == "z_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);
            offset = calcOffset(worldPosition, normal2);
        }
        else if (toolSelected == "y_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);
            offset = calcOffset(worldPosition, normal2);
        }
        else if (toolSelected == "x_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);
            offset = calcOffset(worldPosition, normal2);
        }
        else if (toolSelected == "z_rotate_arrow")
        {
            Vector3 normal2 = Vector3.Cross(-parentGameObject.transform.up, parentGameObject.transform.right);
            parentGameObject.transform.Rotate(Vector3.forward, calcScale(worldPosition,normal2) * rotSpeed * Mathf.Deg2Rad);
        }
        else if (toolSelected == "y_rotate_arrow")
        {
            Vector3 normal2 = Vector3.Cross(-parentGameObject.transform.right, parentGameObject.transform.forward);
            parentGameObject.transform.Rotate(Vector3.up, calcScale(worldPosition, normal2) * rotSpeed * Mathf.Deg2Rad);
        }
        else if (toolSelected == "x_rotate_arrow")
        {
            Vector3 normal2 = Vector3.Cross(-parentGameObject.transform.forward, parentGameObject.transform.up);
            parentGameObject.transform.Rotate(Vector3.right, calcScale(worldPosition, normal2) * rotSpeed * Mathf.Deg2Rad);
        }
        else if (toolSelected == "z_scale_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);
            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.z = tempScale.z - tempScale.z * scaleSpeed * Time.deltaTime * calcScale(worldPosition, normal2);
            if (tempScale.z < 0.3f)
                modelGameObject.transform.localScale = previousScale;
            else
            {
                previousScale = tempScale;
                modelGameObject.transform.localScale = tempScale;
            }
        }
        else if (toolSelected == "y_scale_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);
            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.y = tempScale.y - tempScale.y * scaleSpeed * Time.deltaTime * calcScale(worldPosition, normal2);
            if (tempScale.y < 0.3f)
                modelGameObject.transform.localScale = previousScale;
            else
            {
                previousScale = tempScale;
                modelGameObject.transform.localScale = tempScale;
            }
        }
        else if (toolSelected == "x_scale_arrow")
        {
            Vector3 normal2 = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);
            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.x = tempScale.x - tempScale.x * scaleSpeed * Time.deltaTime * calcScale(worldPosition, normal2);
            if (tempScale.x < 0.3f)
                modelGameObject.transform.localScale = previousScale;
            else
            {
                previousScale = tempScale;
                modelGameObject.transform.localScale = tempScale;
            }
        }
        else if (toolSelected == "scale_box")
        {
            Vector3 tempScale = modelGameObject.transform.localScale;

            tempScale.x = tempScale.x - tempScale.x * scaleSpeed * Time.deltaTime * ((worldPosition.x + worldPosition.y + worldPosition.z) - (tempVector4.x + tempVector4.y + tempVector4.z));
            tempScale.y = tempScale.y - tempScale.y * scaleSpeed * Time.deltaTime * ((worldPosition.x + worldPosition.y + worldPosition.z) - (tempVector4.x + tempVector4.y + tempVector4.z));
            tempScale.z = tempScale.z - tempScale.z * scaleSpeed * Time.deltaTime * ((worldPosition.x + worldPosition.y + worldPosition.z) - (tempVector4.x + tempVector4.y + tempVector4.z));

            if (tempScale.x < 0.3f || tempScale.y < 0.3f || tempScale.z < 0.3f)
                modelGameObject.transform.localScale = previousScale;
            else
            {
                previousScale = tempScale;
                modelGameObject.transform.localScale = tempScale;
            }
        }

        Vector3 target = transform.TransformPoint(offset);
        parentGameObject.transform.position = target;
        tempVector4 = worldPosition;
    }

    void OnMouseUp()
    {
        theChoiseOfThePlayerIs = mainCamera.GetComponent<ButtonSelection>();
        ObjectState currentState = new ObjectState(parentGameObject, modelGameObject, theChoiseOfThePlayerIs.buttonSelected);
        StoreHistory myList = mainCamera.GetComponent<StoreHistory>();
        UndoRedo<ObjectState> secondList = myList.Get();
        secondList.PushToUndoList(currentState);
        firstcall = true;
    }
}