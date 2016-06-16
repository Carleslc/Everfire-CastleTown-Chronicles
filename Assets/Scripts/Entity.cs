using System;
using System.Collections.Generic;
using UnityEngine;

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
        if (!World.IsWalkable(location))
            throw new ArgumentException(location + " is not walkable!");
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
        ensureThereIsNextMovement();
        return Move(route.Dequeue());
    }

    /// <summary>
    /// Move this entity.
    /// </summary>
    /// <returns><c>true</c> if the movement was successfull, <c>false</c> otherwise.</returns>
    public bool Move(Movement movement)
    {
        bool moved = (movement == Movement.WAIT);

        Pos old = currentPosition;
        Pos next = movement.Next(old);

        if (!moved)
        {
            if (World.IsWalkable(next))
            {
                currentPosition = next;
                UpdatePositionOnVillage(old);
                moved = true;
            }
        }

        Debug.Log(name + " move " + movement + ": " + old + " -> " + next + " " + (moved ? "Success" : "Failed"));
        return moved;
    }

    private void ensureThereIsNextMovement()
    {
        if (route.Count == 0)
            PathFinding(1);
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
        Pos oldPos = currentPosition;
        for (int i = 0; i < moves; ++i)
        {
            IEnumerable<Movement> randomizedMovements = ((IEnumerable<Movement>)Enum.GetValues(typeof(Movement))).Shuffle();
            foreach (Movement randomMove in randomizedMovements)
            {
                Pos nextPos = randomMove.Next(oldPos);
                if (World.IsWalkable(nextPos))
                {
                    Debug.Log("Enqueue: " + randomMove);
                    route.Enqueue(randomMove);
                    oldPos = nextPos;
                    break;
                }
            }
        }
    }

    public Movement NextMovement()
    {
        ensureThereIsNextMovement();
        return route.Peek();
    }

    /// <summary>
    /// Gets the next position this entity will try to move to.
    /// </summary>
    /// <returns>Next movement position.</returns>
    public Pos NextPosition()
    {
        return NextMovement().Next(currentPosition);
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
    public static Pos Next(this Movement move, Pos from)
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
