using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    private Map map;

    private GameObject grassTile;
    private GameObject sandTile;
    private GameObject waterTile;
    //private GameObject[] borders;
    private GameObject treeProp;
    private GameObject stoneProp;
    [SerializeField]
    public static float tileSize = 1.0f;

    /// <summary>
    /// Initialises the map to be drawn. You need to call DrawMap tp actually draw it.
    /// </summary>
    /// <param name="map"> The <c>Map</c> to be drawn.</param>
    public void Init(Map map)
    {
        this.map = map;
        grassTile = PrefabLoader.GetTile(Tile.Ground.Grass);
        sandTile = PrefabLoader.GetTile(Tile.Ground.Sand);
        waterTile = PrefabLoader.GetTile(Tile.Ground.Water);

        treeProp = PrefabLoader.GetTile(Tile.Object.Tree);
        stoneProp = PrefabLoader.GetTile(Tile.Object.Stone);

    }

    public void Draw()
    {
        for (int i = 0; i < map.Height; i++)
            for (int j = 0; j < map.Width; j++)
            {
                Pos p = new Pos(i, j);
                DrawTile(map.GetTile(p), p);
            }
    }

    private void DrawTile(Tile t, Pos p)
    {
        GameObject tileToInstantiate = null;
        GameObject propToInstantiate = null;
        switch (t.GroundType)
        {
            case Tile.Ground.Grass:
                tileToInstantiate = grassTile;
                break;
            case Tile.Ground.Water:
                tileToInstantiate = waterTile;
                break;
            case Tile.Ground.Sand:
                tileToInstantiate = sandTile;
                break;
            default:
                tileToInstantiate = grassTile;
                break;
        }
        switch (t.ObjectAbove)
        {
            case Tile.Object.Empty:
                break;
            case Tile.Object.Tree:
                propToInstantiate = treeProp;
                break;
            case Tile.Object.Stone:
                propToInstantiate = stoneProp;
                break;
            default:
                break;
        }

        Vector2 tilePos = p.GetUnityCoordinates();
        string tileName = "(" + p.X + ", " + p.Y + ")";
        GameObject tile = Instantiate(tileToInstantiate, tilePos,
            Quaternion.identity) as GameObject;
        tile.name = "Tile: " + tileName;
        tile.transform.SetParent(transform, false);

        if (propToInstantiate != null)
        {
            GameObject prop = Instantiate(propToInstantiate, tilePos,
                Quaternion.identity) as GameObject;
            prop.name = "Prop: " + tileName;
            prop.transform.SetParent(transform, false);
        }

    }

    public Vector2 GetWorldPos(Pos p)
    {
        return new Vector2((p.Y * tileSize) - (map.Width / 2), (-p.X * tileSize) + (map.Height / 2));
    }
}
