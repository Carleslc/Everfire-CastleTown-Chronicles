using UnityEngine;
using System.Collections;

public class Animal : MovingEntity {

    private AnimalType type;
    public Animal(AnimalType type, Pos location, Village village, int hitPoints) : base(location, village, hitPoints) {
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
