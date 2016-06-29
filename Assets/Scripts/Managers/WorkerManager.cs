using UnityEngine;
using System.Collections;

public class WorkerManager : HumanManager
{
    Worker worker;
    GameObject clothes = null;
    public Job jobInEditor;

    public void Init(Worker worker)
    {
        jobInEditor = worker.Job;
        this.worker = worker;
        DrawWorker();
        base.Init(worker);
    }

    void OnEnable()
    {
        //This event will be triggered when any worker's job has been changed.
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

    /// <summary>
    /// This will be called when the the job of the entity we're managing is changed.
    /// </summary>
    private void OnWorkerJobChanged()
    {
        if (worker.Job != jobInEditor)
        {
            jobInEditor = worker.Job;
            Destroy(clothes);
            DrawWorker();
            UpdateAnimators();
        }
    }
}
