using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    public string mapName;

    [SerializeField]
    private DrawMap drawMap;

    void Awake() {
        World.Init(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
    }

	// Use this for initialization
	void Start () {
        drawMap.Init(World.Map);
        drawMap.Draw();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
