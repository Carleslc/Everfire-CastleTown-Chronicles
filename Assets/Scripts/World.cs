using System;
using System.Collections.Generic;
using UnityEngine;

public static class World
{

    /// <summary>
    /// Tile map of this world.
    /// </summary>
    public static Map Map { get; private set; }

    public static Village Wilderness
    {
        get
        {
            return wilderness;
        }
    }

    private static Village wilderness;

    static Dictionary<string, Village> villages = new Dictionary<string, Village>();

    /// <summary>
    /// Initializes the world with a map and a Wilderness village
    /// </summary>
    /// <param name="map">The world map.</param>
    public static void Init(Map map)
    {
        Map = map;
        wilderness = new Village("Wilderness", false);
        AddVillage(wilderness);
        InitResources();
    }

    private static void InitResources() {
        for (int i = 0; i < Map.Height; i++)
        {
            for (int j = 0; j < Map.Width; j++)
            {
                Pos p = new Pos(i, j);
                if (Map.GetTile(p).ObjectAbove == Tile.Object.Stone)
                {
                    new Resource(ResourceType.stone, p, 20);
                }
                else if (Map.GetTile(p).ObjectAbove == Tile.Object.Tree) {
                    new Resource(ResourceType.tree, p, 20);
                }
            }
        }
    }

    /// <summary>
    /// Gets the entity occupying the position p.
    /// </summary>
    /// <param name="p">The position to check for entities on the whole world.</param>
    /// <returns>The entity occupying position <c>p</c>
    /// or <c>null</c> if there is not an entity at that position.</returns>
    public static Entity GetEntityAt(Pos p)
    {
        foreach (Village v in villages.Values)
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
    /// Adds a village to this world.
    /// </summary>
    /// <param name="village">The village to add.</param>
    public static void AddVillage(Village village) {
        string name = village.Name;

        if (villages.ContainsKey(name))
            throw new ArgumentException("There is already a village with name " + name + " in this world.");

        villages.Add(name, village);
    }

    /// <summary>
    /// Gets all villages of this world.
    /// </summary>
    /// <returns>All villages of this world.</returns>
    public static IEnumerator<Village> GetVillages()
    {
        foreach (Village v in villages.Values)
            yield return v;
    }

    /// <summary>
    /// Gets a village by its name.
    /// </summary>
    /// <param name="name">The name of the village to get.</param>
    /// <returns>The village with name <c>name</c>
    /// or <c>null</c> if there is not a village with that name.</returns>
    public static Village GetVillage(string name)
    {
        Village v;
        villages.TryGetValue(name, out v);
        return v;
    }

    /// <summary>
    /// Checks if the position <c>p</c> is walkable on the whole world.
    /// <para/>A position p is walkable when the tile at that position is walkable
    /// and not exists any entity of any village occupying that position.
    /// </summary>
    /// <param name="p">The position to check walkability.</param>
    /// <returns><c>true</c> if <c>p</c> is walkable, <c>false</c> otherwise.</returns>
    public static bool IsWalkable(Pos p)
    {
        return Map.IsWalkable(p) && !IsOccupied(p);
    }
}
