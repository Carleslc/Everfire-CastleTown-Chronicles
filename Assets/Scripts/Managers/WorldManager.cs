using UnityEngine;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

    public string mapName;
    [SerializeField]
    private MapManager drawMap;
    [SerializeField]
    private List<VillageManager> villageManagers;

    //Im creating a village with some Workers
    void Start() {
        World.Init(MapLoader.loadMap(Application.dataPath + @"/Resources/" + mapName + ".csv"));
        Village v = new Village("Everfire Neverwater", true);        
        World.AddVillage(v);
        new Animal(AnimalType.deer, new Pos(), World.Wilderness, 20);
        new Animal(AnimalType.deer, new Pos(), World.Wilderness, 20);
        new Animal(AnimalType.deer, new Pos(), World.Wilderness, 20);
        new Animal(AnimalType.deer, new Pos(), World.Wilderness, 20);

        CreateDefaultWorker(Job.forester, "Pebek", Gender.male, v);
        CreateDefaultWorker(Job.hunter, "Pobrok", Gender.female, v);
        CreateDefaultWorker(Job.forester, "Gogol", Gender.female, v);

        new Player(1, "Wextia", Gender.male, 1, 1, new Pos(16, 11), v, 20);
        villageManagers = new List<VillageManager>();
        //uiManager.NewVillageAdded(v);
        DrawMap();
    }

    private Worker CreateDefaultWorker(Job job, string name, Gender gender, Village v)
    {
        Pos p = new Pos();
        return new Worker(job, name, gender, 1, 1, p, v, 20);
    }

    //Now, i call functions to draw everyting
    private void DrawMap() {
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
            villageManager.Init(currentVillage);
            villageManagers.Add(villageManager);
        }
	}
	
	// Update is called once per frame
	void Update() {
	    
	}
}
