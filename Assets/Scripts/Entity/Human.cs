using UnityEngine;
using System.Collections;

public class Human : MovingEntity {

    private Gender gender;
    private int bodyType;
    private int hairType;

    /// <summary>
    /// The name of this entity.
    /// </summary>
    public string Name { get; private set; }

    public Human(string name, Gender gender, int bodyType, int hairType, Pos location, Village village, int hitPoints): base(location, village, hitPoints)
    {
        Name = name;
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
    public override string ToString()
    {
        return Name;
    }
}


public enum Gender
{
    male, female, other
}

