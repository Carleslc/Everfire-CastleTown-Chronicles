public class Entity
{
    Village village;
    Pos currentPosition;
    string name;

    /// <summary>
    /// The current position where this entity is located.
    /// </summary>
    public Pos CurrentPosition
    {
        get { return currentPosition; }
    }

    /// <summary>
    /// The name of this entity.
    /// </summary>
    public string Name
    {
        get { return name; }
    }

    /// <summary>
    /// The village where this entity pertains.
    /// </summary>
    public Village ResidenceVillage
    {
        get { return village; }

        set
        {
            village.Remove(currentPosition);
            village = value;
            village.Add(this);
        }
    }

    /// <summary>
    /// Constructs a new Enity on a position pertaining a village.
    /// </summary>
    /// <param name="name">The name of this entity.</param>
    /// <param name="location">The location of this entity.</param>
    /// <param name="pertainsAt">The village of this entity.</param>
    public Entity(string name, Pos location, Village village)
    {
        this.name = name;
        currentPosition = location;
        this.village = village;
        village.Add(this);
    }

    /// <summary>
    /// Move this entity to the next position.
    /// </summary>
    public void Move()
    {
        Pos old = currentPosition;

        // TODO: Move the currentPosition

        UpdatePosition(old);
    }

    /// <summary>
    /// Keeps village position up to date.
    /// </summary>
    /// <param name="old">The previous position of this entity (current position in village state).</param>
    private void UpdatePosition(Pos old)
    {
        if (currentPosition != old)
        {
            village.Remove(old);
            village.Add(this);
        }
    }
}
