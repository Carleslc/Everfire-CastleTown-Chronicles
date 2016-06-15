using UnityEngine;
using System.Collections;

public class Animal : Entity {

    private AnimalType type;
    public Animal(string name, Pos location, Village village, AnimalType type) : base(name, location, village) {
        this.type = type;
    }

    public AnimalType Type
    {
        get
        {
            return type;
        }
    }
}

public enum AnimalType
{
    deer, wolf
}
