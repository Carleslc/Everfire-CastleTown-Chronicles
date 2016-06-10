using UnityEngine;
using System.Collections;

public class DrawMap : MonoBehaviour
{
    private Map map;
    [SerializeField]
    private GameObject grassTile;
    [SerializeField]
    private GameObject waterTile;
    [SerializeField]
    private GameObject[] borders;
    [SerializeField]
    private GameObject treeProp;
    [SerializeField]
    private GameObject stoneProp;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private float tileSize = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Map map)
    {
        this.map = map;
    }

    public void Draw()
    {
        transform.position = new Vector2(Mathf.Ceil(-map.Width / 2), Mathf.Ceil(-map.Height / 2));

        for (int i = 0; i < map.Width; i++)
            for (int j = 0; j < map.Height; j++)
            {
                Pos p = new Pos(i, j);
                DrawTile(map.GetTileType(p), p);
            }
    }

    private void DrawTile(Tile t, Pos p)
    {
        GameObject tileToInstantiate = null;
        GameObject propToInstantiate = null;
        switch (t)
        {
            case Tile.Grass:
                tileToInstantiate = grassTile;
                break;
            case Tile.Tree:
                propToInstantiate = treeProp;
                break;
            case Tile.Stone:
                propToInstantiate = stoneProp;
                break;
            case Tile.Water:
                tileToInstantiate = waterTile;
                break;
            default:
                break;
        }
        if (tileToInstantiate == null)
            tileToInstantiate = grassTile;
        if (propToInstantiate != null) {
            GameObject prop = Instantiate(propToInstantiate, new Vector2(p.X * tileSize, p.Y * tileSize),
                Quaternion.identity) as GameObject;
            prop.name = "(" + p.X + ", " + p.Y + ")";
            prop.transform.SetParent(transform, false);
        }
        GameObject tile = Instantiate(tileToInstantiate, new Vector2(p.X * tileSize, p.Y * tileSize),
            Quaternion.identity) as GameObject;
        tile.name = "(" + p.X + ", " + p.Y + ")";
        tile.transform.SetParent(transform, false);
    }
}
