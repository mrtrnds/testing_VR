using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelection : MonoBehaviour
{
    public float rotSpeed = 100.0f;
    public float scaleSpeed = 0.1f;
    public float moveSpeed = 1.0f;

    private GameObject parentGameObject;
    private GameObject modelGameObject;

    private Vector3 firstParentGameObject = Vector3.zero;
    private Vector3 tempVector4 = Vector3.zero;

    private string toolSelected = "";
    private float threshold = 7.0f;
    private bool firstcall = true;

    private Vector3 GetMouseScreenPos()
    {
        //Pixel coordinates (x,y)
        Vector2 mousePoint = Input.mousePosition;
        Vector2 clickPosition = -Vector2.one;

        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return clickPosition = Camera.main.WorldToScreenPoint(hit.point);
        }
        return clickPosition;
    }

    private Vector3 GetWorldMousePosition(Vector3 inNormal, Vector3 inPoint)
    {
        Vector3 worldPosition = Vector3.zero;
        Vector3 m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, inPoint.z);
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
        "z_scale_arrow"};

        Vector2 mousePoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (listWithTags.Contains(hit.collider.tag))
            {
                toolSelected = hit.collider.tag;
            }
        }
    }

    private float calcMouseDif(Vector3 worldPosition, Vector3 normalVec)
    {
        return Mathf.Abs((worldPosition.x + 100) * normalVec.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normalVec.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normalVec.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normalVec.y * (tempVector4.y + 100)));
    }

    private Vector3 calcOffset(Vector3 worldPosition, Vector3 normalVec)
    {
        return new Vector3((worldPosition.x + 100) * normalVec.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normalVec.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normalVec.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normalVec.y * (tempVector4.y + 100)), 0.0f, 0.0f).normalized * Time.deltaTime * moveSpeed;
    }

    public void OnMouseDrag()
    {
        parentGameObject = GameObject.FindGameObjectWithTag("selectedParent");
        modelGameObject = GameObject.FindGameObjectWithTag("selectedObject");
        Vector3 offset = Vector3.zero;
        Vector3 worldPosition = GetWorldMousePosition(Vector3.forward, firstParentGameObject);
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        if (firstcall == true)
        {
            firstcall = false;
            firstParentGameObject = parentGameObject.transform.position;
        }

        if (toolSelected == "z_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                if (calcMouseDif(worldPosition, normal) != 0)
                    Debug.Log("Mpike");
                tempVector4 = worldPosition;
                return;
            }
            offset = calcOffset(worldPosition, normal);
        }
        else if (toolSelected == "y_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }
            offset = calcOffset(worldPosition, normal);
        }
        else if (toolSelected == "x_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }
            offset = calcOffset(worldPosition, normal);
        }
        else if (toolSelected == "z_rotate_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);
            parentGameObject.transform.Rotate(Vector3.forward, -rotX * normal.y - rotY * normal.x);           
        }
        else if (toolSelected == "y_rotate_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);
            parentGameObject.transform.Rotate(Vector3.up, -rotX * normal.y - rotY * normal.x);
        }
        else if (toolSelected == "x_rotate_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);
            parentGameObject.transform.Rotate(Vector3.right, -rotX * normal.y - rotY * normal.x);
        }
        else if (toolSelected == "z_scale_arrow")
        {
            Vector3 normal = Vector3.Cross(-parentGameObject.transform.up, parentGameObject.transform.right);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }

            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.z = tempScale.z + tempScale.z * scaleSpeed * Time.deltaTime * ((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)));
            modelGameObject.transform.localScale = tempScale;
        }
        else if (toolSelected == "y_scale_arrow")
        {
            Vector3 normal = Vector3.Cross(-parentGameObject.transform.right, parentGameObject.transform.forward);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }

            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.y = tempScale.y + tempScale.y * scaleSpeed * Time.deltaTime * ((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)));
            modelGameObject.transform.localScale = tempScale;
        }
        else if (toolSelected == "x_scale_arrow")
        {
            Vector3 normal = Vector3.Cross(-parentGameObject.transform.forward, parentGameObject.transform.up);

            if (calcMouseDif(worldPosition, normal) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }

            Vector3 tempScale = modelGameObject.transform.localScale;
            tempScale.x = tempScale.x + tempScale.x * scaleSpeed * Time.deltaTime * ((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)));
            modelGameObject.transform.localScale = tempScale;
        }


        //Vector3 tempScale = gameObject.transform.localScale;
        //tempScale.x = tempScale.x + tempScale.x * scaleSpeed * ((GetMouseWorldPos().x - transform.position.x) / transform.position.x);
        //tempScale.y = tempScale.x;
        //tempScale.z = tempScale.x;
        //if (tempScale.x < 0.5f || tempScale.x > 3.5f)
        //    transform.localScale = previousScale;
        //else
        //{
        //    transform.localScale = tempScale;
        //    previousScale = tempScale;
        //}


        Vector3 target = transform.TransformPoint(offset);
        parentGameObject.transform.position = target;
        tempVector4 = worldPosition;
    }

    void OnMouseUp()
    {

    }
}