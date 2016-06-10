using UnityEngine;

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
    public Map(int height, int width) {
        this.height = height;
        this.width = width;
        map = new Tile[this.height, this.width];
    }

    /// <summary>
    /// Creates a random map with the groud made of the <c>Tile</c> tile.
    /// </summary>
    /// <param name="height">Height of the map, in tiles.</param>
    /// <param name="width">Width of the map, in tiles.</param>
    /// <param name="tile"><c>Tile</c> the map is going to be filled with.</param>
    public Map(int height, int width, Tile tile)
    {
        this.height = height;
        this.width = width;
        map = new Tile[this.height, this.width];
        for (int i = 0; i < this.height; i++)
            for (int j = 0; j < this.width; j++) {
                if (Random.value * 10 > 9)
                    map[i, j] = Tile.Stone;
                else if (Random.value * 10 > 7)
                    map[i, j] = Tile.Tree;
                else {
                    map[i, j] = tile;
                }
            }
    }

    /// <summary>
    /// Returns the type of the tile in the <c>Pos</c> p. Prints an error and returns an empty <c>Tile</c>if
    /// p is an invalid <c>Pos</c>.
    /// </summary>
    /// <param name="p">Position occupied by the tile to be retrieved.</param>
    /// <returns>Returns the <c>Tile</c> in p. Returns an empty <c>Tile</c>if
    /// p is an invalid <c>Pos</c>.
    /// </returns>
    public Tile GetTileType(Pos p) {
        Pos p1 = new Pos(1, 2);
        p1 = p1.Left();
        if (isOutOfBounds(p)) {
            Debug.LogError("Tile is out of bounds.");
            return new Tile();
        }
        return map[p.X, p.Y];
    }

    /// <summary>
    /// Returns if the <c>Tile</c> in p is walkable. Prints an error and returns false if
    /// p is an invalid <c>Pos</c>.
    /// </summary>
    /// <param name="p">Position occupied by the tile to be retrieved.</param>
    /// <returns>Returns if the <c>Tile</c> in p is walkable. Returns false if
    /// p is an invalid <c>Pos</c>.
    /// </returns>
    public bool IsWalkable(Pos p) {
        if (isOutOfBounds(p)) {
            Debug.LogError("Tile is out of bounds.");
            return false;
        }
        return map[p.X, p.Y].isWalkable();
    }

    /// <summary>
    /// Returns whether if p is out of bounds in the <c>Map</c> or not.
    /// </summary>
    /// <param name="p">The position to be investigated.</param>
    /// <returns>true if p is out of bounds in the <c>Map</c>, false if it is not.</returns>
    public bool isOutOfBounds(Pos p) {
        return p.X >= width || p.X < 0 || p.Y >= height || p.Y < 0;
    }

    /// <summary>
    /// Sets the <c>Tile</c> in p to the <c>Tile</c> t. Prints an error if
    /// p is an invalid <c>Pos</c>.
    /// </summary>
    /// <param name="p">The position occupied by the tile to be modified.</param>
    /// <param name="t">The new <c>Tile</c>Tile to occupy p with.</param>
    public void SetTile(Pos p, Tile t) {
        if (isOutOfBounds(p))
        {
            Debug.LogError("Tile is out of bounds.");
            return;
        }
        map[p.X, p.Y] = t;
    }
}

/// <summary>
/// Class that stores a position inside the <b>Map</b>. The X value represents the <b>horizontal</b> axis of the map,
/// and the Y value the <b>vertical</b> axis.
/// </summary>
public class Pos{
    int x;
    int y;

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
        return new Pos(x - 1, y);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly to the <b>right</b> of the instance this function is being called on.</returns>
    public Pos Right()
    {
        return new Pos(x + 1, y);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly <b>above</b> of the instance this function is being called on.</returns>
    public Pos Up()
    {
        
        return new Pos(x, y + 1);
    }

    /// <summary>
    /// Returns a new Pos.
    /// </summary>
    /// <returns>The position directly <b>below</b> of the instance this function is being called on.</returns>
    public Pos Down() {
        return new Pos(x, y - 1);
    }
}