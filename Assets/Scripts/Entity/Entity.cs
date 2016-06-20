using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity
{
    Village village;

    protected Queue<Movement> route;
    protected Pos target;

    /// <summary>
    /// The current position where this entity is located.
    /// </summary>
    public Pos CurrentPosition { get; private set; }

    /// <summary>
    /// The name of this entity.
    /// </summary>
    public string Name { get; private set; }

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

    /// <summary>
    /// The current targeted position (current route will attempt to reach this position).
    /// <para/><c>null</c> if there isn't any current target.
    /// </summary>
    public Pos Target
    {
        get
        {
            if (!HasTarget())
                return null;
            return target;
        }
    }

    /// <summary>
    /// Constructs a new named Enity on a position pertaining a village.
    /// <para/>This entity has no initial target.
    /// </summary>
    /// <param name="name">The name of this entity.</param>
    /// <param name="location">The location of this entity.</param>
    /// <param name="pertainsAt">The village of this entity.</param>
    public Entity(string name, Pos location, Village village)
    {
        Name = name;
        if (!World.IsWalkable(location))
            throw new ArgumentException(location + " is not walkable!");
        CurrentPosition = location;
        this.village = village;
        village.Add(this);
        route = new Queue<Movement>();
        target = null;
        EventManager.TriggerEvent(EventManager.EventType.OnNewEntity);
    }

    /// <summary>
    /// Move this entity to the next position of the route
    /// (if there isn't any target then movement will be calculated randomly).
    /// </summary>
    /// <returns><c>true</c> if the movement was successfull (position was walkable),
    /// <c>false</c> otherwise.</returns>
    public virtual bool Move()
    {
        EnsureThereIsNextMovement();
        return Move(route.Dequeue());
    }

    /// <summary>
    /// Move this entity.
    /// </summary>
    /// <returns><c>true</c> if the movement was successfull, <c>false</c> otherwise.</returns>
    public virtual bool Move(Movement movement)
    {
        bool moved = (movement == Movement.WAIT);

        Pos old = CurrentPosition;
        Pos next = movement.Next(old);

        if (!moved)
        {
            if (World.IsWalkable(next))
            {
                CurrentPosition = next;
                UpdatePositionOnVillage(old);
                if (CurrentPosition.Equals(Target))
                    target = null;
                moved = true;
            }
        }

        //Debug.Log(Name + " move " + movement + ": " + old + " -> " + next + " " + (moved ? "Success" : "Failed"));
        return moved;
    }

    private void EnsureThereIsNextMovement()
    {
        if (!EnsureThereIsNextMovementWithTarget() && route.Count == 0)
            PathFinding(1);
    }

    private bool EnsureThereIsNextMovementWithTarget()
    {
        return HasTarget() &&
            (route.Count == 0 || World.IsOccupied(route.Peek().Next(CurrentPosition)) ?
                PathFinding(Target) : true);
    }

    /// <summary>
    /// Clears the movements route of this entity and his target, if exists.
    /// </summary>
    public void ClearRoute()
    {
        route.Clear();
        target = null;
    }

    /// <summary>
    /// Checks if this entity has a target position to reach with path finding.
    /// </summary>
    /// <returns>If this entity has a target position to reach with path finding.</returns>
    public bool HasTarget()
    {
        return target != null;
    }

    /// <summary>
    /// Sets a target position to this entity.
    /// This entity will attempt to reach that position.
    /// </summary>
    /// <param name="target">The target position to reach with path finding.</param>
    /// <returns><c>true</c> if target was accessible and route has been added,
    /// <c>false</c> otherwise.</returns>
    public bool SetTarget(Pos target)
    {
        return PathFinding(target);
    }

    /// <summary>
    /// Calculates a route to a targeted position and adds it as current route.
    /// <para/>If target is the current target then calculates the route with walkable (non-occupied) positions.
    /// <para/>Otherwise, if the target is new and is accessible then clears previous target and route and calculates
    /// the route with walkable positions (may be occupied, but tiles are walkables).
    /// </summary>
    /// <param name="target">The target position.</param>
    /// <returns><c>true</c> if target was accessible and route has been added,
    /// <c>false</c> otherwise.</returns>
    protected virtual bool PathFinding(Pos target)
    {
        if (target == null)
            throw new ArgumentNullException("Target cannot be null!");

        Utils.Diagnosis.StartTimer();

        if (CurrentPosition.Equals(target))
        {
            ClearRoute();
            this.target = target;
            route.Enqueue(Movement.WAIT);
            Debug.Log("PATHFINDING " + Utils.Diagnosis.StopTimer() + " ms");
            return true;
        }

        bool isCurrentTarget = HasTarget() && Target.Equals(target);
        HashSet<Pos> checkedPositions = new HashSet<Pos>();
        Queue<Pos> BFS = new Queue<Pos>();
        Dictionary<Pos, KeyValuePair<Pos, Movement>> steps = new Dictionary<Pos, KeyValuePair<Pos, Movement>>();
        List<Movement> movesWithoutWait = (((IEnumerable<Movement>)Enum.GetValues(typeof(Movement))))
            .Where(m => m != Movement.WAIT)
            .ToList();

        foreach (Movement m in movesWithoutWait)
        {
            Pos p = m.Next(CurrentPosition);
            if (isCurrentTarget ? World.IsWalkable(p) : World.Map.IsWalkable(p))
            {
                BFS.Enqueue(p);
                steps[p] = new KeyValuePair<Pos, Movement>(CurrentPosition, m);
            }
        }

        while (BFS.Count > 0)
        {
            Pos current = BFS.Dequeue();

            if (current.Equals(target))
            {
                ClearRoute();
                this.target = target;

                // Reconstruct path
                List<Movement> reversedPath = new List<Movement>();

                while (current != CurrentPosition)
                {
                    KeyValuePair<Pos, Movement> step = steps[current];
                    reversedPath.Add(step.Value);
                    current = step.Key;
                }

                for (int i = reversedPath.Count - 1; i >= 0; --i)
                    route.Enqueue(reversedPath[i]);

                Debug.Log("PATHFINDING " + Utils.Diagnosis.StopTimer() + " ms");
                return true;
            }

            foreach (Movement m in movesWithoutWait)
            {
                Pos p = m.Next(current);
                if (!checkedPositions.Contains(p) && (isCurrentTarget ? World.IsWalkable(p) : World.Map.IsWalkable(p)))
                {
                    BFS.Enqueue(p);
                    steps[p] = new KeyValuePair<Pos, Movement>(current, m);
                }
            }
            checkedPositions.Add(current);
            World.Map.GetTile(current).GroundType = Tile.Ground.Sand;
        }
        Debug.Log("PATHFINDING " + Utils.Diagnosis.StopTimer() + " ms");
        return false;
    }

    /// <summary>
    /// Calculate random <c>moves</c> movements as route.
    /// <para/>Note that this method clears target and previous route.
    /// </summary>
    /// <param name="moves">The number of moves to calculate.</param>
    protected virtual void PathFinding(int moves)
    {
        ClearRoute();
        Pos oldPos = CurrentPosition;

        List<Movement> movesWithoutWait = (((IEnumerable<Movement>)Enum.GetValues(typeof(Movement))))
                .Where(m => m != Movement.WAIT)
                .ToList();

        for (int i = 0; i < moves; ++i)
        {
            movesWithoutWait.Shuffle();
            foreach (Movement randomMove in /*randomized*/movesWithoutWait)
            {
                Pos nextPos = randomMove.Next(oldPos);
                if (World.IsWalkable(nextPos))
                {
                    //Debug.Log("Enqueue: " + randomMove);
                    route.Enqueue(randomMove);
                    oldPos = nextPos;
                    break;
                }
            }

            if (route.Count <= i) // If any movement was possible
                route.Enqueue(Movement.WAIT);
        }
    }

    public virtual Movement NextMovement()
    {
        EnsureThereIsNextMovement();
        return route.Peek();
    }

    /// <summary>
    /// Gets the next position this entity will try to move to.
    /// </summary>
    /// <returns>Next movement position.</returns>
    public Pos NextPosition()
    {
        return NextMovement().Next(CurrentPosition);
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
        return Name + " " + CurrentPosition + " (" + village.Name + ")";
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
