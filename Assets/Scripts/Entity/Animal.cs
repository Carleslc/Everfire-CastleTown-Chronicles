using UnityEngine;
using System.Collections;

public class Animal : MovingEntity {

    private AnimalType type;
    public Animal(AnimalType type, Pos location, Village village, int hitPoints) : base(location, village, hitPoints) {
        this.type = type;
        InitialisationCompleted();
    }

    public AnimalType Type
    {
        get
        {
            return type;
        }
    }

    public override void Kill()
    {
        base.Kill();
        ResourceType resourceToDrop = ResourceType.bread;
        switch (type)
        {
            case AnimalType.deer:
                resourceToDrop = ResourceType.deer;
                break;
            case AnimalType.wolf:
                break;
            default:
                break;
        }
        new Resource(resourceToDrop, CurrentPosition, 20);
    }
}

public enum AnimalType
{
    deer, wolf
}