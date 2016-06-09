using System;
using System.Reflection;

public enum Tile
{
    [TileAttr(true)] Grass,
    /// <summary>
    /// The base of a Tree.
    /// </summary>
    [TileAttr(false)] Tree,
    [TileAttr(false)] Stone,
    [TileAttr(false)] Water
}

class TileAttr : Attribute
{
    public bool Walkable { get; private set; }

    internal TileAttr(bool walkable)
    {
        Walkable = walkable;
    }
}

static class TileExtensions
{
    /// <summary>
    /// Checks if the tile is walkable or not.
    /// </summary>
    /// <returns><c>true</c> if this tile is walkable, <c>false</c> otherwise.</returns>
    public static bool isWalkable(this Tile tile)
    {
        return GetAttr(tile).Walkable;
    }

    private static TileAttr GetAttr(Tile p)
    {
        return (TileAttr)Attribute.GetCustomAttribute(ForValue(p), typeof(TileAttr));
    }

    private static MemberInfo ForValue(Tile p)
    {
        return typeof(Tile).GetField(Enum.GetName(typeof(Tile), p));
    }
}
