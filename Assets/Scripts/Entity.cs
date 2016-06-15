using System;
using System.Collections.Generic;

public class Entity
{
    Village village;
    Pos currentPosition;
    string name;

    Queue<Movement> route;

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
    public Village Village
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
    /// Constructs a new named Enity on a position pertaining a village.
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
        route = new Queue<Movement>();
    }

    /// <summary>
    /// Move this entity to the next position of the route
    /// (if there isn't any route then movement will be calculated randomly).
    /// </summary>
    /// <returns><c>true</c> if the movement was successfull, <c>false</c> otherwise.</returns>
    public bool Move()
    {
        if (route.Count == 0) // Ensures there is at least one next movement
            PathFinding(1);

        return Move(route.Dequeue());
    }

    /// <summary>
    /// Move this entity.
    /// </summary>
    /// <returns><c>true</c> if the movement was successfull, <c>false</c> otherwise.</returns>
    public bool Move(Movement movement)
    {
        bool moved = (movement == Movement.WAIT);

        if (!moved)
        {
            Pos old = currentPosition;
            Pos next = movement.next(old);

            if (village.IsWalkable(next))
            {
                currentPosition = next;
                UpdatePositionOnVillage(old);
                moved = true;
            }
        }

        return moved;
    }

    /// <summary>
    /// Clears the movements route of this entity.
    /// </summary>
    public void ClearRoute()
    {
        route.Clear();
    }

    /// <summary>
    /// Calculate a route to a targeted position and appends it to previous routes (if they exist).
    /// </summary>
    /// <param name="target">The target position.</param>
    /// <returns><c>true</c> if target was accessible and route has been added,
    /// <c>false</c> otherwise.</returns>
    public bool PathFinding(Pos target)
    {
        // TODO
        return true;
    }

    /// <summary>
    /// Calculate random <c>moves</c> movements to add to the route queue.
    /// </summary>
    /// <param name="moves">The number of moves to calculate.</param>
    private void PathFinding(int moves)
    {
        Array moveValues = Enum.GetValues(typeof(Movement));
        Random random = new Random();
        for (int i = 0; i < moves; ++i)
        {
            Movement randomMove = (Movement)moveValues.GetValue(random.Next(moveValues.Length));
            route.Enqueue(randomMove);
        }
    }

    /// <summary>
    /// Gets the next position this entity will try to move to.
    /// </summary>
    /// <returns>Next movement position.</returns>
    public Pos NextPosition()
    {
        Pos next = currentPosition;
        if (route.Count > 0)
            next = route.Peek().next(currentPosition);
        return next;
    }

    /// <summary>
    /// Keeps village position up to date.
    /// </summary>
    /// <param name="old">The previous position of this entity (current position in village state).</param>
    private void UpdatePositionOnVillage(Pos old)
    {
        village.Remove(old);
        village.Add(this);
    }

    public override string ToString()
    {
        return name + " " + currentPosition + " (" + village.Name + ")";
    }

}

public enum Movement
{
    WAIT,
    UP,
    DOWN,
    RIGHT,
    LEFT
}

public static class MoveExtensions
{
    public static Pos next(this Movement move, Pos from)
    {
        switch (move)
        {
            case Movement.UP: return from.Up();
            case Movement.DOWN: return from.Down();
            case Movement.RIGHT: return from.Right();
            case Movement.LEFT: return from.Left();
            default: return from;
        }
    }
}
