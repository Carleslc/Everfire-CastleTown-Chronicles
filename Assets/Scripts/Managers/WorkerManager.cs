using UnityEngine;
using System.Collections;

public class WorkerManager : HumanManager
{

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

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnWorkerJobChanged, OnWorkerJobChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnWorkerJobChanged, OnWorkerJobChanged);
    }


    private void DrawWorker()
    {
        clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(worker.Job), Vector2.zero,
        Quaternion.identity) as GameObject;
        clothes.transform.SetParent(transform, false);
    }

    private void OnWorkerJobChanged()
    {
        if (worker.Job != job)
        {
            job = worker.Job;
            Destroy(clothes);
            DrawWorker();
            UpdateAnimators();
        }
    }

    public void UpdateJobEditor()
    {
        worker.Job = job;
        Destroy(clothes);
        DrawWorker();
        UpdateAnimators();
    }

}
