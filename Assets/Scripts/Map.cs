using UnityEngine;
using System.Text;

/// <summary>
/// Class that holds the information of various <c>Tiles</c>.
/// </summary>
public class Map {
    Tile[,] map;
    int height, width;

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="height">Height of the map, in tiles.</param>
    /// <param name="width">Width of the map, in tiles.</param>
    public Map(int height, int width) : this(new Tile[height, width]) {}

    /// <summary>
    /// Creates a map with the initialized tiles.
    /// </summary>
    /// <param name="tiles">The tiles rows and columns to be set on this map.</param>
    public Map(Tile[,] tiles)
    {
        this.height = tiles.GetLength(0);
        this.width = tiles.GetLength(1);
        map = tiles;
    }

    /// <summary>
    /// Creates a random map with the ground made of the <c>Tile</c> tile.
    /// </summary>
    /// <param name="height">Height of the map, in tiles.</param>
    /// <param name="width">Width of the map, in tiles.</param>
    /// <param name="tile"><c>Tile</c> the map is going to be filled with.</param>
    public Map(int height, int width, Tile.Ground tile)
    {
        this.height = height;
        this.width = width;
        map = new Tile[this.height, this.width];
        for (int i = 0; i < this.height; i++)
            for (int j = 0; j < this.width; j++) {
                map[i, j] = new Tile(tile);
                if (Random.value * 10 > 9)
                    map[i, j].ObjectAbove = Tile.Object.Stone;
                else if (Random.value * 10 > 7)
                    map[i, j].ObjectAbove = Tile.Object.Tree;                
            }
    }

    /// <summary>
    /// Returns the type of the tile in the <c>Pos</c> p.
    /// </summary>
    /// <param name="p">Position occupied by the tile to be retrieved.</param>
    /// <returns>Returns the <c>Tile</c> in p.</returns>
    /// <exception cref="ArgumentException">If position is out of map bounds.</exception>
    public Tile GetTile(Pos p) {
        if (IsOutOfBounds(p))
            throw new System.ArgumentOutOfRangeException(p + " is out of bounds.");

        return map[p.X, p.Y];
    }

    /// <summary>
    /// Returns whether if p is out of bounds in the <c>Map</c> or not.
    /// </summary>
    /// <param name="p">The position to be investigated.</param>
    /// <returns>true if p is out of bounds in the <c>Map</c>, false if it is not.</returns>
    public bool IsOutOfBounds(Pos p) {
        return p.X >= height || p.X < 0 || p.Y >= width || p.Y < 0;
    }

    /// <summary>
    /// Sets the <c>Tile</c> in p to the <c>Tile</c> t.
    /// </summary>
    /// <param name="p">The position occupied by the tile to be modified.</param>
    /// <param name="t">The new <c>Tile</c>Tile to occupy p with.</param>
    /// <exception cref="ArgumentException">If position is out of map bounds.</exception>
    public void SetTile(Pos p, Tile t) {
        if (IsOutOfBounds(p))
            throw new System.ArgumentOutOfRangeException(p + " is out of bounds.");
        map[p.X, p.Y] = t;
    }

    /// <summary>
    /// Checks if the tile at position <c>p</c> is walkable.
    /// </summary>
    /// <param name="p">The position to check walkability.</param>
    /// <returns><c>true</c> if tile at position <c>p</c> exists and is walkable,
    /// <c>false</c> otherwise.</returns>
    public bool IsWalkable(Pos p)
    {
        try { return GetTile(p).IsWalkable(); }
        catch (System.ArgumentOutOfRangeException) { } // if position is out of map then return false
        return false;
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j)
                sb.Append(map[i, j]).Append(", ");
            sb.AppendLine();
        }
        return sb.ToString();
    }
    
}

/// <summary>
/// Class that stores a position inside the <b>Map</b>. The X value represents the <b>horizontal</b> axis of the map,
/// and the Y value the <b>vertical</b> axis.
/// </summary>
[System.Serializable]
public class Pos {
    [SerializeField]
    int x;
    [SerializeField]
    public int y;

    /// <summary>
    /// The position's <b>horizontal</b> axis' value.
    /// </summary>
    public int X
    {
        get
        {
            return x;
        }
    }

    /// <summary>
    /// The position's <b>vertical</b> axis' value.
    /// </summary>
    public int Y
    {
        get
        {
            return y;
        }
    }
    /// <summary>
    /// Default Constructor.
    /// </summary>
    /// <param name="x">The position's <b>horizontal</b> axis' value.</param> 
    /// <param name="y">The position's <b>vertical</b> axis' value.</param>
    public Pos(int x, int y) {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly to the <b>left</b> of the instance this function is being called on.</returns>
    public Pos Left() {
        return new Pos(x, y - 1);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly to the <b>right</b> of the instance this function is being called on.</returns>
    public Pos Right()
    {
        return new Pos(x, y + 1);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly <b>above</b> of the instance this function is being called on.</returns>
    public Pos Up()
    {
        return new Pos(x - 1, y);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly <b>below</b> of the instance this function is being called on.</returns>
    public Pos Down() {
        return new Pos(x + 1, y);
    }

    /// <summary>
    /// Gets the distance in tiles from this position to another.
    /// </summary>
    /// <param name="to">The (destination) other position.</param>
    /// <returns>The distance in tiles from this position to another.</returns>
    public int Distance(Pos to)
    {
        int x = to.X - X;
        int y = to.Y - Y;
        return (x >= 0 ? x : -x) + (y >= 0 ? y : -y);
    }

    /// <summary>
    /// Gets this position as unity coordiantes (Vector2 position).
    /// </summary>
    /// <returns>The unity coordinates of this position.</returns>
    public Vector2 GetWorldPos()
    {
        return new Vector2((Y * MapManager.tileSize) - World.Map.Width / 2, (-X * MapManager.tileSize) + World.Map.Height / 2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Pos o = (Pos)obj;
        return x == o.x && y == o.y;
    }

    public override int GetHashCode()
    {
        int prime = 31;
        int result = 1;
        result = prime * result + x.GetHashCode();
        result = prime * result + y.GetHashCode();
        return result;
    }

    public override string ToString()
    {
        return "[" + x + "," + y + "]";
    }
}
