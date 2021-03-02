using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreenToCanvas : MonoBehaviour
{

    public RectTransform m_parent;
    public Camera m_uiCamera;
    public InputField textArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, Input.mousePosition, m_uiCamera, out anchoredPos);
            Vector3 arriveAt = new Vector3(anchoredPos.x, anchoredPos.y, 0);
            GameObject nu = Instantiate(textArea.gameObject, arriveAt, Quaternion.identity) as GameObject;
            nu.transform.SetParent(m_parent.transform, false);
            nu.SetActive(true);
        }
    }
}
