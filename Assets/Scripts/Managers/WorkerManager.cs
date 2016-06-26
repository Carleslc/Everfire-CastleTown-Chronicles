using UnityEngine;
using System.Collections;

public class WorkerManager : HumanManager {

    Worker worker;
    GameObject clothes = null;



    public Job Job
    {
        get
        {
            return worker.Job;
        }

        set
        {
            worker.Job = value;
            Destroy(clothes);
            DrawWorker();
        }
    }

    public void Init(Worker worker)
    {
        this.worker = worker;
        DrawWorker();
        base.Init(worker);
    }
    private void DrawWorker() {
        clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(worker.Job), Vector2.zero,
        Quaternion.identity) as GameObject;

        clothes.transform.SetParent(transform, false);
    }

}
