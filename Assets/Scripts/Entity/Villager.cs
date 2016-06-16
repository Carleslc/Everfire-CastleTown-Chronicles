using UnityEngine;
using System.Collections;

public class Villager : Human {
    private Job job;

    public Villager(string name, Pos location, Village village, Gender gender, int bodyType, int hairType, Job job) :
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
    }
}

public enum Job
{
    labourer, forester, butcher, hunter
}
