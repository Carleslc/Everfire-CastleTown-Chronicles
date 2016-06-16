using UnityEngine;
using System.Collections.Generic;

public class Environment : MonoBehaviour {

    public string mapName;
    [SerializeField]
    private MapManager drawMap;
    [SerializeField]
    private List<VillageManager> villageManagers;

    //Im creating a village with some villagers
    void Awake() {
        World.Init(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
        Village v = new Village("Everfire Neverwater", true);
        World.AddVillage(v);
        new Villager("Pebek", new Pos(12, 10), v, Gender.male, 1, 1, Job.forester);
        new Villager("Pobrok", new Pos(15, 11), v, Gender.female, 1, 1, Job.hunter);
        new Player("Wextia", new Pos(16, 11), v, Gender.male, 1, 1, 1);
        villageManagers = new List<VillageManager>();
    }

	//Now, i call functions to draw everyting
	void Start () {
        drawMap.Init(World.Map);
        drawMap.Draw();

        IEnumerator<Village> iterator = World.GetVillages();
        while (iterator.MoveNext())
        {
            Village currentVillage = iterator.Current;
            GameObject villageManagerObject =
                Instantiate(PrefabLoader.GetVillage(), Vector3.zero, Quaternion.identity) as GameObject;
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
