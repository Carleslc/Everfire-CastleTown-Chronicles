using UnityEngine;
using System.Collections;

public class Human : MovingEntity {

    private Gender gender;
    private int bodyType;
    private int hairType;
    public Human(string name, Pos location, Village village, Gender gender, int bodyType, int hairType): base(name, location, village)
    {
        this.gender = gender;
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

