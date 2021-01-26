using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentConstraint : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        if (GetComponent<Transform>().position.y < -0.50f)
        {
            p.y = 0.50f;
            GetComponent<Transform>().position = p;
        }
    }
}
