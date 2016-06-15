using UnityEngine;
using System.Collections.Generic;

public class Environment : MonoBehaviour {

    public string mapName;

    private static string villagePrefabPath = "Prefabs/village";

    [SerializeField]
    private DrawMap drawMap;
    [SerializeField]
    private List<VillageManager> villageManagers;

    World world;

    //Im creating a village with a single villager named Pebek
    void Awake() {
        world = new World(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
        world.AddVillage(new Village("PenesLocos", world));
        Village[] villages = world.GetVillages();
        villages[0].Add(new Human("Pebek", new Pos(12, 10),villages[0], Gender.female, Job.hunter));        
    }

	//Now, i call functions to draw everyting
	void Start () {
        drawMap.Init(world.Map);
        drawMap.Draw();
        foreach (Village v in world.GetVillages()) {
            GameObject vm = 
                Instantiate(Resources.Load(villagePrefabPath), Vector3.zero, Quaternion.identity) as GameObject;
            vm.name = v.Name;
            VillageManager vman = vm.GetComponent<VillageManager>();
            vman.Init(this, v);
            villageManagers.Add(vman);
        }
	}

    public Vector2 GetWorldPos(Pos pos) {
        return drawMap.GetWorldPos(pos);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
