using System;


public class Entity {


    Village village;
    /// <summary>
    /// The current position where this entity is located.
    /// </summary>
    public Pos CurrentPosition { get; protected set; }

    int hitPoints = 20;

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

    public int HitPoints
    {
        get
        {
            return hitPoints;
        }
        protected set
        {
            hitPoints = value;
            if (hitPoints <= 0)
                Destroy();
        }
    }

    //Note that i'm not calling to the EventManager just yet, because if i DID, the child classes wouldn't have everything
    //initialised when we call to the EventManager, and we don't want that, do we?
    public Entity(Pos location, Village village, int hitPoints) {
        this.hitPoints = hitPoints;        
        CurrentPosition = location;
        this.village = village;
        if (!World.IsWalkable(CurrentPosition))
            throw new ArgumentException(CurrentPosition + " is not walkable!");
        village.Add(this);
        village.LastEntityAdded = this;
    }

    /// <summary>
    /// VERY IMPORTANT TO CALL AT THE END OF A CHILD CLASS CONSTRUCTOR
    /// </summary>
    protected void InitialisationCompleted() {
        EventManager.TriggerEvent(EventManager.EventType.OnEntityAdded);
        DebugLogger.Log(DebugChannel.Entity, "New Entity added", ToString());
    }

    public virtual void Destroy() {        
        village.Destroy(CurrentPosition);
        EventManager.TriggerEvent(EventManager.EventType.OnEntityDestroyed);
    }

    public virtual void Kill()
    {
        HitPoints = 0;
        EventManager.TriggerEvent(EventManager.EventType.OnEntityKilled);
    }

    public void DealDamage(int ammount) {
        HitPoints -= ammount;
    }



}
