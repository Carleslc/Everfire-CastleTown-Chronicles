using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class UIDebugPanel : MonoBehaviour {

    [SerializeField]
    GameObject content;
    //[SerializeField]
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    
    public void ChangePanel(string panelName) {
        DeactivatePanels();
        panels[panelName].SetActive(true);
    }

    private void DeactivatePanels()
    {
        foreach (KeyValuePair<string, GameObject> pair in panels)
        {
            pair.Value.SetActive(false);
        }
    }

        // Use this for initialization
        void Start () {

        //Here we add all the objects that are our children to the panels structure.
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            panels.Add(child.name, child);
        }

        GameObject button = PrefabLoader.GetUIComponent(1);
        float xPos = 10;
        bool first = true;
        //Here we create the tabs to iterate between panels
        foreach (KeyValuePair<string, GameObject> pair in panels) {

            GameObject buttonObj = Instantiate(button, new Vector2(xPos, -10), Quaternion.identity) as GameObject;
            string buttonName = pair.Key;
            //Automatically selecting the first button as the selected panel
            if (first) {
                ChangePanel(buttonName);
                first = false;
            }
            buttonObj.name = buttonName;
            buttonObj.GetComponentInChildren<Text>().text = buttonName;
            xPos += 100;
            buttonObj.transform.SetParent(content.transform, false);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
