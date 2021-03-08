using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateFunction : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            List<string> listWithTags = new List<string>() {
            "ModelTag",
            "x_arrow",
            "y_arrow",
            "z_arrow",
            "x_rotate_arrow",
            "y_rotate_arrow",
            "z_rotate_arrow",
            "x_scale_arrow",
            "y_scale_arrow",
            "z_scale_arrow",
            "scale_box",
            "selectedObject" };
            Vector2 mousePoint = Input.mousePosition;
            int layer_mask = LayerMask.GetMask("Ignore Raycast");
            int layer_mask2 = LayerMask.GetMask("Default");

            Ray ray = Camera.main.ScreenPointToRay(mousePoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, layer_mask))
            {
                if (!listWithTags.Contains(hit.collider.tag))
                {
                    GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                    if (Model != null)
                    {
                        Model.GetComponent<Transformation>().SetDefaultCalled(true);
                        Model.GetComponent<Transformation>().disableTools();

                    }
                }
            }
            else if(Physics.Raycast(ray, out hit, 1000, layer_mask2))
            {
                if (!listWithTags.Contains(hit.collider.tag))
                {
                    GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                    if (Model != null)
                    {
                        Model.GetComponent<Transformation>().SetDefaultCalled(true);
                        Model.GetComponent<Transformation>().disableTools();

                    }
                }
            }
            else
            {
                GameObject Model = GameObject.FindGameObjectWithTag("selectedObject");
                if (Model != null)
                {
                    if (Model.GetComponent<Transformation>().GetCheckBool() == true)
                    {
                        Model.GetComponent<Transformation>().SetDefaultCalled(true);
                        Model.GetComponent<Transformation>().SetCheckBool(false);
                        Model.GetComponent<Transformation>().disableTools();
                    }
                }
            }
        }
    }
}
