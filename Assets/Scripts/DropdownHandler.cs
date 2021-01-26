using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Web;

public class DropdownHandler : MonoBehaviour
{
    public Text TextBox;
    private ContentController contentController;
    void Awake()
    {
        contentController = GameObject.FindObjectOfType<ContentController>();
    }

    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();


        // Create the dropdown menu options. Get the file names of the assetbundles 
        // from the server and print them to the UI button
       // var dirFIles = Web.HttpContext.Current.Server.MapPath("192.168.1.4/assetbundles");
        string[] fileEntries = Directory.GetFiles("E:/xampp/htdocs/assetbundles", "*-Android");
        dropdown.options.Add(new Dropdown.OptionData() { text = "Add Model" });
        //       var names = AssetDatabase.GetAllAssetBundleNames();
        int counter_1 = fileEntries[0].IndexOf("assetbundles\\") + "assetbundles\\".Length;
        foreach (var file in fileEntries)
        {
            int counter_2 = file.IndexOf("-Android");
            dropdown.options.Add(new Dropdown.OptionData() { text = file.Substring(counter_1, counter_2-counter_1) });
        }

        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBox.text = dropdown.options[index].text;
        contentController.LoadContent(TextBox.text);
    }
}
