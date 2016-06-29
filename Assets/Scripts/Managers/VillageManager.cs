using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{

    private Village village;

    /// <summary>
    /// This structure stores all the Entities of the associated village and its prefab
    /// </summary>
    private Dictionary<Entity, GameObject> prefabs;

    [Header("Add new Worker")]
    [SerializeField]
    string newWorkerName = "NewWorker";
    [SerializeField]
    Pos newWorkerPosition;
    [SerializeField]
    Gender newWorkerGender = Gender.other;
    [SerializeField]
    Job newWorkerJob = Job.farmer;
    [SerializeField]
    int newWorkerHealth = 20;

    void Awake()
    {
        newWorkerPosition = new Pos(0, 0);
    }

    public void Init(Village village)
    {
        this.village = village;
        InitGraphics();
    }

    public void AddNewWorker()
    {
        Worker w = new Worker(newWorkerJob, newWorkerName, newWorkerGender, 1, 1, newWorkerPosition, village, newWorkerHealth);
        InstantiateEntity(w);
    }

    public void AddPlayer()
    {

    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnEntityDestroyed, OnEntityDestroyed);
        EventManager.StartListening(EventManager.EventType.OnEntityKilled, OnEntityKilled);
        EventManager.StartListening(EventManager.EventType.OnEntityAdded, OnEntityAdded);

    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnEntityDestroyed, OnEntityDestroyed);
        EventManager.StopListening(EventManager.EventType.OnEntityKilled, OnEntityKilled);
        EventManager.StopListening(EventManager.EventType.OnEntityAdded, OnEntityAdded);
    }

    private void OnEntityDestroyed() {
        Entity e = village.LastEntityLost;
        if (e != null)
        {
            //We destroy the gameObject associated with it.
            DestroyPrefab(e);
            //We just destroy it. No calling for Kill.
            village.LastEntityLost = null;
        }

    }

    private void OnEntityKilled() {
        Entity e = village.LastEntityLost;
        if (e != null)
        {
            DestroyPrefab(e);
            village.LastEntityLost = null;
        }
    }

    private void DestroyPrefab(Entity e) {
        Destroy(prefabs[e]);
        prefabs.Remove(e);
    }

    private void OnEntityAdded() {
        Entity e = village.LastEntityAdded;
        if (e != null)
        {
            Debug.Log("Adding new entity: " + e.ToString());
            InstantiateEntity(e);
            village.LastEntityAdded = null;
        }
    }

    private void InitGraphics()
    {
        Entity[] entities = village.GetEntities();
        prefabs = new Dictionary<Entity, GameObject>();
        foreach (Entity e in entities)
        {
            InstantiateEntity(e);
        }
        village.LastEntityAdded = null;
        village.LastEntityLost = null;
    }

    /// <summary>
    /// This is so dirty i cant even look through it, possibly i'll optimize it in the future. TODO
    /// </summary>
    /// <param name="e"></param>
    private void InstantiateEntity(Entity e)
    {
        GameObject entityPrefab = Instantiate(PrefabLoader.GetEntityPrefab(), e.CurrentPosition.GetWorldPos(),
    Quaternion.identity) as GameObject;
        entityPrefab.transform.SetParent(transform);
        if (e is Animal) {
            Animal a = (Animal)e;
            entityPrefab.AddComponent<AnimalManager>().Init(a);
            entityPrefab.name = "Animal " + a.Type.ToString();
        }
        if (e is Worker)
        {
            Worker w = (Worker)e;
            entityPrefab.AddComponent<WorkerManager>().Init(w);
            entityPrefab.name = "Worker " + w.Name;
        }
        else if (e is Player)
        {
            Player p = (Player)e;
            entityPrefab.AddComponent<PlayerManager>().Init(p);
            entityPrefab.AddComponent<DialogueManager>();
            entityPrefab.name = "Player " + p.Name;
        }
        else if (e is Human)
        {
            Human h = (Human)e;
            entityPrefab.AddComponent<HumanManager>().Init(h);
            entityPrefab.name = "Human " + h.Name;
        }
        else if (e is Resource) {
            Resource r = (Resource)e;
            Debug.Log(r.Type);
            entityPrefab.AddComponent<ResourceManager>().Init(r);
            entityPrefab.name = "Resource " + r.Type;
        }
        else
        {
            entityPrefab.AddComponent<EntityManager>().Init(e);
            entityPrefab.name = "Entity " + e.ToString();
        }
        prefabs.Add(e, entityPrefab);
    }
}
