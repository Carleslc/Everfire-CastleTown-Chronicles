using UnityEngine;
using System.Collections;

public class WorkerManager : HumanManager {

    Worker worker;
    GameObject clothes = null;
    [SerializeField]
    private Job job;

    public void Init(Worker worker)
    {
        job = worker.Job;
        this.worker = worker;
        DrawWorker();
        base.Init(worker);
    }
    private void DrawWorker() {
        clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(worker.Job), Vector2.zero,
        Quaternion.identity) as GameObject;

        clothes.transform.SetParent(transform, false);
    }



    public void UpdateJob()
    {
        worker.Job = job;
        Destroy(clothes);
        DrawWorker();
        UpdateAnimators();
    }

}
