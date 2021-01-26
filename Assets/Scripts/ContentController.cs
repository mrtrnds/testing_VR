using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{

    public API api;
    public Text toucherText;

    public void LoadContent(string name)
    {
        DestroyAllChildren();
        GetComponent<Rigidbody>().useGravity = false;
        api.GetBundleObject(name, OnContentLoaded, transform);
    }

    void OnContentLoaded(GameObject content)
    {
        //do something cool here
        Debug.Log("Loaded: " + content.name);
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}