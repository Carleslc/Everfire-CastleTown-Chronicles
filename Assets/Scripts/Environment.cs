using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    public string mapName;

    private Map map;
    [SerializeField]
    private DrawMap drawMap;

    void Awake() {
        map = MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv");
        map.SetTile(new Pos(0, 1), new Tile(Tile.Ground.Grass)); // Testing where is [0,1]
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
