public enum Tile
{
    grass, tree
}
public static class TileExtensions
{
    public static bool isWalkable(this Tile tile)
    {
        return true;
    }
}