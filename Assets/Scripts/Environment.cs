using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    private Map map;
    [SerializeField]
    private DrawMap drawMap;
    void Awake() {
        map = new Map(20, 20, Tile.Grass);
    }
	// Use this for initialization
	void Start () {
        drawMap.Init(map);
        drawMap.Draw();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
