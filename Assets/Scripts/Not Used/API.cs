using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    public GameObject gameObjectParent;
    private Vector3 parent_pos;
    private Vector3 parent_scale;
    private Vector3 parent_rotation;

    void Awake()
    {
        // Get Position, Rotation, Scaling of Parent Gameobject in order to init after each instantiate to these values.
        parent_pos.x = gameObjectParent.transform.position.x;
        parent_pos.y = gameObjectParent.transform.position.y;
        parent_pos.z = gameObjectParent.transform.position.z;
        parent_scale.x = gameObjectParent.transform.localScale.x;
        parent_scale.y = gameObjectParent.transform.localScale.y;
        parent_scale.z = gameObjectParent.transform.localScale.z;
        parent_rotation.x = gameObjectParent.transform.localEulerAngles.x;
        parent_rotation.y = gameObjectParent.transform.localEulerAngles.y;
        parent_rotation.z = gameObjectParent.transform.localEulerAngles.z;
    }
    const string BundleFolder = "192.168.1.9/assetbundles/";

    public void GetBundleObject(string assetName, UnityAction<GameObject> callback, Transform bundleParent)
    {
        StartCoroutine(GetDisplayBundleRoutine(assetName, callback, bundleParent));
    }

    IEnumerator GetDisplayBundleRoutine(string assetName, UnityAction<GameObject> callback, Transform bundleParent)
    {

        string bundleURL = BundleFolder + assetName + "-";

        //append platform to asset bundle name
#if UNITY_WEBGL
        bundleURL += "Android";
#else
        bundleURL += "IOS";
#endif

        Debug.Log("Requesting bundle at " + bundleURL);

        //request asset bundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return www.SendWebRequest();
       // yield return www;
        if (www.isNetworkError)
        {
            Debug.Log("Network error");
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            //AssetBundle bundle = www.assetBundle; 
            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
                gameObjectParent.GetComponent<Rigidbody>().useGravity = true;
                bundle.Unload(false);
                callback(arObject);
                GameObject parentOject = GameObject.Find("ContentParent");
                parentOject.transform.position = parent_pos;
                parentOject.transform.localScale = parent_scale;
                parentOject.transform.localEulerAngles = parent_rotation;
            }
            else
            {
                Debug.Log("Not a valid asset bundle");
            }
        }

        //    using (WWW www = new WWW(BundleURL))
        //{
        //    yield return www;
        //    if (www.error != null)
        //        throw new Exception("WWW download had an error:" + www.error);
        //    AssetBundle bundle = www.assetBundle;
        //    if (AssetName == "")
        //        Instantiate(bundle.mainAsset);
        //    else
        //        Instantiate(bundle.LoadAsset(AssetName));
        //    // Unload the AssetBundles compressed contents to conserve memory
        //    bundle.Unload(false);

        //} // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
