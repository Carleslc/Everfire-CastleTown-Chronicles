using UnityEngine;
using System.Collections;

public class Worker : Human {
    private Job job;

    public Worker(string name, Pos location, Village village, Gender gender, int bodyType, int hairType, Job job) :
        base(name, location, village, gender, bodyType, hairType)
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