using UnityEngine;
using System.Collections;

public static class PrefabLoader
{

    private static string tilesPath = "Prefabs/Tiles/";
    private static string propsPath = "Prefabs/Props/";
    private static string humansPath = "Prefabs/Humans/";
    private static string villagePath = "Prefabs/village";
    private static string uiComponentsPath = "Prefabs/UI/";

    /// <summary>
    /// Returns the village prefab, which contains a <c>VillageManager</c>.
    /// </summary>
    /// <returns></returns>
    public static GameObject GetVillage() {
        return LoadGameObject(villagePath);
    }

    /// <summary>
    /// Returns a tile prefab with a sprite attached to it. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <param name="type">The <c>Tile.Ground</c> type to get the tile of.</param>
    /// <returns></returns>
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
        return LoadGameObject(finalPath);
    }

    /// <summary>
    /// Returns a tile prefab with a sprite attached to it. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <param name="type">The <c>Tile.Object</c> type to get the tile of.</param>
    /// <returns></returns>
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
        return LoadGameObject(finalPath);
    }

    /// <summary>
    /// Returns the "Blank" human prefab, which contains the <c>EntityManager</c> script. If you want to actually see it,
    /// you'll have to retrieve a Hair, Body and Clothes for him or her. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <returns></returns>
    public static GameObject GetHumanBlank()
    {
        return LoadGameObject(humansPath + "human");
    }

    public static GameObject GetPlayerBlank() {
        return LoadGameObject(humansPath + "player");
    }

    /// <summary>
    /// Returns a Hair, which has an animation and has to be the child of an object with the component <c>EntityManager</c>
    /// to actully work. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <param name="gender">The <c>Gender</c> the hair will have.</param>
    /// <param name="index">The index the hair will have.</param>
    /// <returns></returns>
    public static GameObject GetHumanHair(Gender gender, int index)
    {
        string finalPath = humansPath;
        finalPath += gender.ToString() + "_hair" + "_" + index;
        return LoadGameObject(finalPath);
    }

    /// <summary>
    /// Returns a Clothes, which has an animation and has to be the child of an object with the component <c>EntityManager</c>
    /// to actully work. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <param name="job">The <c>Job</c> the clothes are related with.</param>
    /// <returns></returns>
    public static GameObject GetHumanWorkClothes(Job job) {
        string finalPath = humansPath;
        finalPath += "clothes_" + job.ToString();
        return LoadGameObject(finalPath);

    }

    public static GameObject GetHumanPlayerClothes(int index) {
        string finalPath = humansPath;
        finalPath += "clothes_player_" + index;
        return LoadGameObject(finalPath);
    }

    /// <summary>
    /// Returns a Body, which has an animation and has to be the child of an object with the component <c>EntityManager</c>
    /// to actully work. It will raise an exception if the prefab does not exist.
    /// </summary>
    /// <param name="gender">The <c>Gender</c> the body will have.</param>
    /// <param name="index">The index the body will have.</param>
    /// <returns></returns>
    public static GameObject GetHumanBody(Gender gender, int index)
    {
        string finalPath = humansPath;
        finalPath += gender.ToString() + "_body" + "_" + index;
        return LoadGameObject(finalPath);
    }

    public static GameObject GetUIComponent(int index) {
        string finalPath = uiComponentsPath;
        switch (index)
        {
            case 1:
                finalPath += "UIDPTab";
                break;
            default:
                break;
        }
        return LoadGameObject(finalPath);
    }

    //Class to control that the requested prefab actually exists or not.
    private static GameObject LoadGameObject(string path) {
        GameObject go = Resources.Load<GameObject>(path);
        if (go == null)
            throw new System.Exception("The requested prefab does not exist: " + path);
        return go;
    }

}
