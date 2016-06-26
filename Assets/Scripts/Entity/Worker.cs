using UnityEngine;
using System.Collections;

public class Worker : Human {
    private Job job;

    public Worker(Job job, string name, Gender gender, int bodyType, int hairType, Pos location, Village village, int hitPoints) :
        base(name, gender, bodyType, hairType, location, village, hitPoints)
    {
        this.job = job;
    }

    public Job Job
    {
        get
        {
            return job;
        }

        set
        {
            job = value;
        }
    }
}