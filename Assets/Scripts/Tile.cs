using System;
using System.Reflection;
using TileExtensions;

public class Tile
{
    Ground ground;
    Object above;

    /// <summary>
    /// The base ground (lowest level) of this tile.
    /// </summary>
    public Ground GroundType
    {
        get { return ground; }
        set { ground = value; }
    }

    /// <summary>
    /// The object above the ground (highest level) of this tile.
    /// </summary>
    public Object ObjectAbove
    {
        get { return above; }
        set { above = value; }
    }

    /// <summary>
    /// Constructs new tile with a base ground and without any object stacked above.
    /// </summary>
    /// <param name="groundType">Base ground type of the tile</param>
    public Tile(Ground groundType) : this(groundType, Object.Empty) { }

    /// <summary>
    /// Constructs new tile with a base ground and an object stacked above.
    /// </summary>
    /// <param name="groundType">Base ground type of the tile</param>
    /// <param name="objectAbove">Object type to stack above the ground</param>
    public Tile(Ground groundType, Object objectAbove)
    {
        GroundType = groundType;
        ObjectAbove = objectAbove;
    }

    /// <summary>
    /// Checks if there is an object above the ground of this tile.
    /// </summary>
    /// <returns><c>true</c> if this tile has a non-empty object, <c>false</c> otherwise.</returns>
    public bool HasObjectAbove()
    {
        return above != Object.Empty;
    }

    /// <summary>
    /// Checks whether this tile is walkable or not.
    /// <para/>A tile is walkable when both ground and object on top are walkable.
    /// </summary>
    /// <returns><c>true</c> if this tile is walkable, <c>false</c> otherwise.</returns>
    public bool IsWalkable()
    {
        return ground.IsWalkable() && above.IsWalkable();
    }

    /// <summary>
    /// Tile type that represents the base ground of a tile
    /// </summary>
    public enum Ground
    {
        [TileAttr(true)]
        Grass,
        [TileAttr(true)]
        Sand,
        [TileAttr(false)]
        Water
    }

    /// <summary>
    /// Tile type that can be stacked above the ground
    /// </summary>
    public enum Object
    {
        [TileAttr(true)]
        Empty,
        /// <summary>
        /// The base of a Tree.
        /// </summary>
        [TileAttr(false)]
        Tree,
        [TileAttr(false)]
        Stone
    }

    public override string ToString() {
        return GroundType + "~" + ObjectAbove;
    }
}

/// <summary>
/// This namespace only must be used in this class for Tile enums additional information.
/// This namespace has been created for restrict access to attributes outside Tile class.
/// </summary>
namespace TileExtensions
{
    internal static class TileExtensions {

        /// <summary>
        /// Checks whether this tile type is walkable or not.
        /// </summary>
        /// <returns><c>true</c> if this tile is walkable, <c>false</c> otherwise.</returns>
        internal static bool IsWalkable(this Enum e)
        {
            return GetAttr(e).Walkable;
        }

        private static TileAttr GetAttr(Enum e)
        {
            return (TileAttr)Attribute.GetCustomAttribute(ForValue(e), typeof(TileAttr));
        }

        private static MemberInfo ForValue(Enum e)
        {
            return typeof(Enum).GetField(Enum.GetName(typeof(Enum), e));
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal class TileAttr : Attribute
    {
        public bool Walkable { get; private set; }

        public TileAttr(bool walkable)
        {
            Walkable = walkable;
        }
    }
}
