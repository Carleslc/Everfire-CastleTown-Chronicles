﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject uiDebugPanel;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            uiDebugPanel.SetActive(!uiDebugPanel.activeInHierarchy);
            if (uiDebugPanel.activeInHierarchy)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }


    /*Dictionary<string, List<string>> info = new Dictionary<string, List<string>>();
    List<Village> villages = new List<Village>();
    [SerializeField]
    Text villagesInfoText;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void NewVillageAdded(Village village) {
        villages.Add(village);
        UpdateVillages();
    }
    public void UpdateVillages() {
        info = new Dictionary<string, List<string>>();
        foreach (Village v in villages) {
            List<string> entitiesInfo = new List<string>();
            foreach (Entity e in v.GetEntities()) {
                string entityInfo  = e.GetType().ToString();
                entityInfo += " " + e.Name;
                if (e is Human) {
                    Human h = (Human)e;
                    entityInfo += " " + h.Gender.ToString();
                    if (h is Worker)
                    {
                        Worker vi = (Worker)h;
                        entityInfo += " " + vi.Job.ToString();
                    }
                    //else if (h is Player) {
                    //    Player p = (Player)h;
                    //}
                }
                entitiesInfo.Add(entityInfo);
            }
            info.Add(v.Name, entitiesInfo);
            //Debug.Log(entitiesInfo.Count);
        }
        RewriteInfo();
    }

    private void RewriteInfo() {
        int lines = 0;
        villagesInfoText.text = "";
        foreach (KeyValuePair<string, List<string>> pair in info) {
            villagesInfoText.text += pair.Key;
            villagesInfoText.text += " " + pair.Value.Count + " Entities";
            lines += 1 + pair.Value.Count;
            foreach (string s in pair.Value) {
                villagesInfoText.text += "\n    " + s;
            }
        }
        villagesInfoText.GetComponent<RectTransform>().sizeDelta = new Vector2(175, lines * 20);


    }
    */
}
