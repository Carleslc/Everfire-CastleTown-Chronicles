using System;


public class Entity {

    Village village;
    /// <summary>
    /// The current position where this entity is located.
    /// </summary>
    public Pos CurrentPosition { get; protected set; }

    /// <summary>
    /// The village where this entity pertains.
    /// </summary>
    public Village Village
    {
        get { return village; }

        set
        {
            village.Remove(CurrentPosition);
            village = value;
            village.Add(this);
        }
    }
    public Entity(Pos location, Village village) {
        if (!World.IsWalkable(location))
            throw new ArgumentException(location + " is not walkable!");
        CurrentPosition = location;
        this.village = village;
        village.Add(this);
        EventManager.TriggerEvent(EventManager.EventType.OnNewEntity);
        DebugLogger.Log(DebugChannel.Entity, "New Entity added", ToString());
    }
}
