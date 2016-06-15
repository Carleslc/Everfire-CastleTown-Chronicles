using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    private static float lerpRate = 15;


    Village village;
    Dictionary<Pos, Entity> entities;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(Village village) {
        this.village = village;
        entities = village.GetEntities();
    }

    private void InitGraphics() {
        //village.GetEntities
    }

}
