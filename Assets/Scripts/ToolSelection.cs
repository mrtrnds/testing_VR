using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelection : MonoBehaviour
{
    public string toolSelected = "";
    private GameObject parentGameObject;
    private Vector3 v3Pos;
    private float threshold = 0.1f;
    private Vector3 tempVector4 = Vector3.zero;
    private bool firstcall = true;
    private Vector3 firstParentGameObject = Vector3.zero;
    public float rotSpeed = 80f;

    private Vector3 GetMouseScreenPos()
    {
        //Pixel coordinates (x,y)
        Vector2 mousePoint = Input.mousePosition;
        Vector2 clickPosition = -Vector2.one;
        v3Pos = Input.mousePosition;

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

        Vector3 mousePoint = Input.mousePosition;
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

    public void OnMouseDrag()
    {
        parentGameObject = transform.parent.gameObject;
        Vector3 offset = Vector3.zero;

        if (firstcall == true)
        {
            firstcall = false;
            firstParentGameObject = parentGameObject.transform.position;
        }

        Vector3 worldPosition = GetWorldMousePosition(Vector3.forward, firstParentGameObject);
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        if (toolSelected == "z_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);

            if (Mathf.Abs((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100))) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }
            offset = new Vector3((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)), 0.0f, 0.0f).normalized * Time.deltaTime * (0.5f);
        }
        else if (toolSelected == "y_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);

            if (Mathf.Abs((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100))) < threshold)
            {
                tempVector4 = worldPosition;
                return;
            }
            offset = new Vector3((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)), 0.0f, 0.0f).normalized * Time.deltaTime * (0.5f);
        }
        else if (toolSelected == "x_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);

            if (Mathf.Abs((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100))) < threshold)
            {
                Debug.Log("kanei return");
                tempVector4 = worldPosition;
                return;
            }
            offset = new Vector3((worldPosition.x + 100) * normal.x * (worldPosition.x + 100) + (worldPosition.y + 100) * normal.y * (worldPosition.y + 100) - ((tempVector4.x + 100) * normal.x * (tempVector4.x + 100) + (tempVector4.y + 100) * normal.y * (tempVector4.y + 100)), 0.0f, 0.0f).normalized * Time.deltaTime * (0.5f);
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
            Vector3 normal = Vector3.Cross(parentGameObject.transform.up, parentGameObject.transform.right);
            parentGameObject.transform.Rotate(Vector3.forward, -rotX * normal.x - rotY * normal.y);
        }
        else if (toolSelected == "y_scale_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.right, parentGameObject.transform.forward);
            parentGameObject.transform.Rotate(Vector3.up, -rotX * normal.x - rotY * normal.y);
        }
        else if (toolSelected == "x_scale_arrow")
        {
            Vector3 normal = Vector3.Cross(parentGameObject.transform.forward, parentGameObject.transform.up);
            parentGameObject.transform.Rotate(Vector3.right, -rotX * normal.x - rotY * normal.y);
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