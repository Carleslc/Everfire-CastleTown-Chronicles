using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{

    private Village village;
    private Entity[] entities;

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
        entities = this.village.GetEntities();
        //this.environment = environment;
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

    private void InitGraphics()
    {
        foreach (Entity e in entities)
        {

            InstantiateEntity(e);
        }
    }

    private void InstantiateEntity(Entity e)
    {
        GameObject entityPrefab = Instantiate(PrefabLoader.GetEntityPrefab(), e.CurrentPosition.GetWorldPos(),
    Quaternion.identity) as GameObject;
        entityPrefab.transform.SetParent(transform);
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
            entityPrefab.name = "Player " + p.Name;
        }
        else if (e is Human)
        {
            Human h = (Human)e;
            entityPrefab.AddComponent<HumanManager>().Init(h);
            entityPrefab.name = "Human " + h.Name;
        }
    }
}
