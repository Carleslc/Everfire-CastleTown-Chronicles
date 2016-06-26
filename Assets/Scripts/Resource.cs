using UnityEngine;
using System.Collections;

public class Resource : Entity {
    ResourceType type;
    public Resource(ResourceType type, Pos location, int hitPoints) : base(location, World.Wilderness, hitPoints)  {
        this.type = type;
        InitialisationCompleted();
    }

    public ResourceType Type
    {
        get
        {
            return type;
        }
    }
}
