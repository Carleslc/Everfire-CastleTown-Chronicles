﻿using System.Collections.Generic;

public static class World
{

    public static Map Map { get; private set; }

    static List<Village> villages = new List<Village>();

    /// <summary>
    /// Initializes the world with a map.
    /// </summary>
    /// <param name="map">The world map.</param>
    public static void Init(Map map)
    {
        Map = map;
    }

    /// <summary>
    /// Gets the entity occupying the position p.
    /// </summary>
    /// <param name="p">The position to check for entities on the whole world.</param>
    /// <returns>The entity occupying position <c>p</c>
    /// or <c>null</c> if there is not an entity at that position.</returns>
    public static Entity GetEntityAt(Pos p)
    {
        foreach (Village v in villages)
        {
            Entity at = v.GetEntityAt(p);
            if (at != null)
                return at;
        }
        return null;
    }

    /// <summary>
    /// Checks if the position <c>p</c> is occupied by an entity on the whole world.
    /// </summary>
    /// <param name="p">The position to check for entities on the whole world.</param>
    /// <returns><c>true</c> if <c>p</c> is occupied by an entity, <c>false</c> otherwise.</returns>
    public static bool IsOccupied(Pos p)
    {
        return GetEntityAt(p) != null;
    }

    /// <summary>
    /// Checks if the position <c>p</c> is walkable on the whole world.
    /// A position p is walkable when the tile at that position is walkable
    /// and not exists any entity of any village occupying that position.
    /// </summary>
    /// <param name="p">The position to check walkability.</param>
    /// <returns><c>true</c> is <c>p</c> is walkable, <c>false</c> otherwise.</returns>
    public static bool IsWalkable(Pos p)
    {
        if (Map.GetTile(p).IsWalkable())
            return !IsOccupied(p);
        return false;
    }
}
