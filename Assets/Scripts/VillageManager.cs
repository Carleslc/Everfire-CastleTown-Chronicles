using UnityEngine;
using System.Collections;

public class VillageManager : MonoBehaviour {

    private static float lerpRate = 15;


    Village village;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(Village village) {
        this.village = village;
    }

    private void InitGraphics() {

    }

}
