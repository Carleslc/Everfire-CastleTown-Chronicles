using System;
using System.Text;

/// <summary>
/// Class for load/save a map of/to a CSV-serialized file.
/// </summary>
public static class MapLoader {

    /// <summary>
    /// Character that separates the layers (ground, object) of a tile.
    /// </summary>
    public static readonly char LayerSeparator = '~';

    /// <summary>
    /// Loads a map from a serialized map on a CSV file.
    /// </summary>
    /// <param name="pathMapCSV">System path of the CSV file that has stored the map to load.</param>
    /// <returns>The map loaded from <c>pathMapCSV</c></returns>
    public static Map loadMap(string pathMapCSV)
    {
        try {
            string[] rows = System.IO.File.ReadAllLines(pathMapCSV);
            int height = rows.Length;
            if (height > 0)
            {
                string[] firstRowColumns = rows[0].Split(';');
                Tile[,] tiles = new Tile[height, firstRowColumns.Length];
                // first row (we've it already splitted)
                assignTiles(ref tiles, 0, firstRowColumns);
                // remaining rows
                for (int i = 1; i < height; ++i)
                    assignTiles(ref tiles, i, rows[i].Split(';'));
                return new Map(tiles);
            }
        } catch (Exception e) {
            throw new ArgumentException(e.Message);
        }
        return new Map(0, 0);
    }

    private static void assignTiles(ref Tile[,] tiles, int i, string[] cols)
    {
        for (int j = 0; j < cols.Length; ++j)
        {
            string[] layers = cols[j].Split(LayerSeparator);
            tiles[i, j] = new Tile((Tile.Ground)int.Parse(layers[0]), (Tile.Object)int.Parse(layers[1]));
        }
    }

    /// <summary>
    /// Saves a map to a serialized map on a CSV file.
    /// </summary>
    /// <param name="map">Map to serialize and save.</param>
    /// <param name="pathMapCSV">System path of the CSV file where to save the <c>map</c>.</param>
    public static void saveMap(Map map, string pathMapCSV)
    {
        try {
            int height = map.Height;
            int width = map.Width;

            string[] rows = new string[height];

            for (int i = 0; i < height; ++i)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < width; ++j)
                {
                    Tile at = map.GetTile(new Pos(i, j));
                    sb.Append((int)at.GroundType).Append(LayerSeparator).Append((int)at.ObjectAbove);
                    if (j < width - 1)
                        sb.Append(';');
                }
                sb.AppendLine();
                rows[i] = sb.ToString();
            }

            System.IO.File.WriteAllLines(pathMapCSV, rows);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}
