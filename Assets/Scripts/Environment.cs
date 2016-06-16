using UnityEngine;
using System.Collections.Generic;

public class Environment : MonoBehaviour {

    public string mapName;

    private static string villagePrefabPath = "Prefabs/village";

    [SerializeField]
    private DrawMap drawMap;
    [SerializeField]
    private List<VillageManager> villageManagers;

    //Im creating a village with a single villager named Pebek
    void Awake() {
        World.Init(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
        World.AddVillage(new Village("PenesLocos"));
        Village village = World.GetVillageAt(0);
        new Human("Pebek", new Pos(12, 10), village, Gender.female, Job.hunter);
        //village.Add(new Human("Pebek", new Pos(12, 10), village, Gender.female, Job.hunter));        
    }

	//Now, i call functions to draw everyting
	void Start () {
        drawMap.Init(World.Map);
        drawMap.Draw();
         
        for (int i = 0; i < World.GetVillageCount(); i++)
        {
            Village currentVillage = World.GetVillageAt(i);
            GameObject villageManagerObject =
                Instantiate(Resources.Load(villagePrefabPath), Vector3.zero, Quaternion.identity) as GameObject;
            villageManagerObject.name = currentVillage.Name;
            VillageManager villageManager = villageManagerObject.GetComponent<VillageManager>();
            villageManager.Init(this, currentVillage);
            villageManagers.Add(villageManager);
        }
	}

    public Vector2 GetWorldPos(Pos pos) {
        return drawMap.GetWorldPos(pos);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
