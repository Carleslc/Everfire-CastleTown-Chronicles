using UnityEngine;
using System.Collections;

public class DrawMap : MonoBehaviour
{
    private Map map;

    string tilesPath = "Prefabs/Tiles/";
    string propsPath = "Prefabs/Props/";

    private GameObject grassTile;
    private GameObject sandTile;
    private GameObject waterTile;
    private GameObject[] borders;
    private GameObject treeProp;
    private GameObject stoneProp;
    private GameObject tilePrefab;
    [SerializeField]
    private float tileSize = 1.0f;

    public void Init(Map map)
    {
        this.map = map;
        grassTile = Resources.Load<GameObject>(tilesPath + "grass");
        sandTile =  Resources.Load<GameObject>(tilesPath + "sand");
        waterTile = Resources.Load<GameObject>(tilesPath + "water");

        treeProp =  Resources.Load<GameObject>(propsPath + "tree");
        stoneProp = Resources.Load<GameObject>(propsPath + "stone");

    }

    public void Draw()
    {
        for (int i = 0; i < map.Width; i++)
            for (int j = 0; j < map.Height; j++)
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

        if (p.X == 0 && p.Y == 0)
            tileToInstantiate = grassTile;
        Vector2 tilePos = new Vector2((p.X * tileSize) - (map.Width / 2), (-p.Y * tileSize) + (map.Height/2));
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
}
