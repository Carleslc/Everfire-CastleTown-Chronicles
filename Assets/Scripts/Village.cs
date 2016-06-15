using System;
using System.Collections.Generic;
using System.Text;

public class Village
{

    Dictionary<Pos, Entity> entities;
    string name;

    /// <summary>
    /// The name of this village.
    /// </summary>
    public string Name
    {
        get { return name; }
    }

    /// <summary>
    /// Constructs new village with a name and without entities.
    /// </summary>
    /// <param name="name"></param>
    public Village(string name)
    {
        this.name = name;
        entities = new Dictionary<Pos, Entity>();
    }

    /// <summary>
    /// Checks if there is an entity of this village at <c>p</c>
    /// tile and therefore that position is not walkable.
    /// </summary>
    /// <param name="p">The position to check.</param>
    /// <returns><c>true</c> if this position is occupied by an entity of this village,
    /// <c>false</c> otherwise.</returns>
    public bool IsOccupied(Pos p)
    {
        return entities.ContainsKey(p);
    }

    /// <summary>
    /// Gets the entity of this village occupying position <c>p</c>.
    /// </summary>
    /// <param name="p">The position of the village to get.</param>
    /// <returns>The entity occupying position <c>p</c> or <c>null</c>
    /// if that position is not occupied by any villager of this village.</returns>
    public Entity GetEntityAt(Pos p)
    {
        Entity e;
        return entities.TryGetValue(p, out e) ? e : null;
    }

    /// <summary>
    /// Adds an entity to this village at one position.
    /// </summary>
    /// <param name="entity">The entity to add to this village.</param>
    /// <exception cref="ArgumentException">If the position of the entity trying to add is already occupied by
    /// other entity of this village.</exception>
    public void Add(Entity entity)
    {
        Pos current = entity.CurrentPosition;
        if (IsOccupied(current))
            throw new ArgumentException(entity.Name + " is on an occupied position: " + current
                + " by " + GetEntityAt(current).Name);
        entities[current] = entity;
    }

    /// <summary>
    /// Removes an entity of this village at position <c>at</c> if there is any entity there.
    /// </summary>
    /// <param name="at">The position of the entity to remove.</param>
    public void Remove(Pos at)
    {
        entities.Remove(at);
    }

    /// <summary>
    /// Moves all entities of this village.
    /// </summary>
    public void MoveAllEntities()
    {
        foreach (Entity e in entities.Values)
            e.Move();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder(name);
        sb.AppendLine();
        foreach (Entity e in entities.Values)
            sb.Append(e.Name).Append(" ").AppendLine(e.CurrentPosition.ToString());
        return sb.ToString();
    }
}
