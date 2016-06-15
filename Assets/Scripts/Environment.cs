using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    public string mapName;

    [SerializeField]
    private DrawMap drawMap;

    World world;

    void Awake() {
        world = new World(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
        
    }

	// Use this for initialization
	void Start () {
        drawMap.Init(world.Map);
        drawMap.Draw();
	}

    public Vector2 GetWorldPos(Pos pos) {
        return drawMap.GetWorldPos(pos);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
