using System;
using System.Reflection;

public enum Tile
{
    [TileAttr(true)] Grass,
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
