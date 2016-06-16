using UnityEngine;
using System.Collections;

public class Human : Entity {

    private Gender gender;
    private Job job;
    private int bodyType;
    private int hairType;
    public Human(string name, Pos location, Village village, Gender gender, Job job, int bodyType, int hairType): base(name, location, village)
    {
        this.gender = gender;
        this.job = job;
        this.bodyType = bodyType;
        this.hairType = hairType;
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

    public int HairType
    {
        get
        {
            return hairType;
        }
    }

    public int BodyType
    {
        get
        {
            return bodyType;
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

