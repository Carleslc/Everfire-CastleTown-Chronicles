using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    private static float lerpRate = 15;
    private static string humanPrefabsPath = "Prefabs/Humans/";

    List<EntityManager> entityManagers;
    Village village;
    Dictionary<Pos, Entity> entities;
    Environment environment;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(Environment environment, Village village) {
        this.village = village;
        entities = village.GetEntities();
        this.environment = environment;
    }

    private void InitGraphics() {
        foreach (KeyValuePair<Pos, Entity> pair in entities) {
            if (pair.Value is Human)
            {
                Human h = (Human)pair.Value;
                string gender = h.Gender.ToString();
                Instantiate(Resources.Load(humanPrefabsPath + "_" + gender), environment.GetWorldPos(pair.Key), Quaternion.identity);
            }
        }
    }

}
