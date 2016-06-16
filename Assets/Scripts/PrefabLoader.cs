using UnityEngine;
using System.Collections;

public static class PrefabLoader
{

    private static string tilesPath = "Prefabs/Tiles/";
    private static string propsPath = "Prefabs/Props/";
    private static string humansPath = "Prefabs/Humans/";

    public static GameObject GetTile(Tile.Ground type)
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

    public static GameObject GetTile(Tile.Object type)
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

    public static GameObject GetHumanBlank()
    {
        return Resources.Load<GameObject>(humansPath + "human");
    }

    public static GameObject GetHumanHair(Gender gender, int index)
    {
        string finalPath = humansPath;
        finalPath += gender.ToString() + "_hair" + "_" + index;        
        return Resources.Load<GameObject>(finalPath);
    }
    public static GameObject GetHumanWorkClothes(Job job) {
        string finalPath = humansPath;
        finalPath += "clothes_" + job.ToString();
        return Resources.Load<GameObject>(finalPath);

    }
    public static GameObject GetHumanBody(Gender gender, int index)
    {
        string finalPath = humansPath;
        finalPath += gender.ToString() + "_body" + "_" + index;
        return Resources.Load<GameObject>(finalPath);
    }



}
