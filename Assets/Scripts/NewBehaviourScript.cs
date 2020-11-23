using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Color colorStart;
    // Start is called before the first frame update
    void Start()
    {
        colorStart = GetComponent<Renderer>().material.color;

    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }


    private void OnMouseUp()
    {
        GetComponent<Renderer>().material.color = colorStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
