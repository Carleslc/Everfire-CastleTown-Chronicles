using UnityEngine;
using System.Collections;

public static class PrefabLoader
{

    private static string tilesPath = "Prefabs/Tiles/";
    private static string propsPath = "Prefabs/Props/";
    private static string humansPath = "Prefabs/Humans/human";

    public static GameObject GetTilePrefab(Tile.Ground type)
    {
        string finalPath = tilesPath;
        switch (type)
        {
            case Tile.Ground.Grass:
                finalPath += "grass";
                break;
            case Tile.Ground.Sand:
                finalPath += "sand";
                break;
            case Tile.Ground.Water:
                finalPath += "water";
                break;
            default:
                throw new System.Exception("Ground type does not hasve a matching prefab in the database");
        }
        return Resources.Load<GameObject>(finalPath);
    }

    public static GameObject GetTilePrefab(Tile.Object type)
    {
        string finalPath = propsPath;
        switch (type)
        {
            case Tile.Object.Empty:
                finalPath += "empty";
                break;
            case Tile.Object.Tree:
                finalPath += "tree";
                break;
            case Tile.Object.Stone:
                finalPath += "stone";
                break;
            default:
                throw new System.Exception("Object type does not hasve a matching prefab in the database");
        }
        return Resources.Load<GameObject>(finalPath);
    }

    public static GameObject GetHumanBlankPrefab()
    {
        return Resources.Load<GameObject>(humansPath);
    }

    public static GameObject GetHumanVisualPrefab(Gender gender)
    {
        string finalPath = humansPath;
        switch (gender)
        {
            case Gender.male:
                finalPath += "_male";
                break;
            case Gender.female:
                finalPath += "_female";
                break;
            case Gender.other:
                finalPath += "_other";
                break;
            default:
                throw new System.Exception("Gender type does not hasve a matching prefab in the database");
        }
        return Resources.Load<GameObject>(finalPath);
    }


}
