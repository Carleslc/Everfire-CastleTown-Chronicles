using UnityEngine;
using System.Collections;

public class Human : Entity {

    private Gender gender;
    private Job job;
    public Human(string name, Pos location, Village village, Gender gender, Job job): base(name, location, village)
    {
        this.gender = gender;
        this.job = job;
    }

    public Gender Gender
    {
        get
        {
            return gender;
        }
    }

    public Job Job
    {
        get
        {
            return job;
        }
    }
}

public enum Gender
{
    male, female, other
}

public enum Job
{
    labourer, forester, butcher, hunter
}

