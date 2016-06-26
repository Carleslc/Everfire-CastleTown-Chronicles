using UnityEngine;
using System.Collections.Generic;

public class StorageBuilding : Building {

    int capacity;
    Dictionary<Resource, int> storedGoods;
    protected List<Resource> allowedGoods;

    public StorageBuilding(int capacity, BuildingType buildingType, Pos location, Village village, int hitPoints) : 
        base(buildingType, location, village, hitPoints)
    {
        this.capacity = capacity;
        storedGoods = new Dictionary<Resource, int>();
        allowedGoods = new List<Resource>();
    }

    public void Store(Resource resource, int ammount)
    {

    }
}

//public enum StorageBuildingType
//{
//    [BuildingTypeAttr(2000)]
//    warehouse,
//    [BuildingTypeAttr(20)]
//    farm,
//    [BuildingTypeAttr(30)]
//    forestersLodge,
//    [BuildingTypeAttr(10)]
//    butchery,
//    [BuildingTypeAttr(10)]
//    huntersHut
//}


//internal static class BuildingTypeExtensions
//{
//    internal static int Capacity(this StorageBuildingType j)
//    {
//        return GetAttr(j).Capacity;
//    }
//    private static BuildingTypeAttr GetAttr(StorageBuildingType j)
//    {
//        return (BuildingTypeAttr)Attribute.GetCustomAttribute(ForValue(j), typeof(BuildingTypeAttr));
//    }

//    private static MemberInfo ForValue(StorageBuildingType j)
//    {
//        Type type = j.GetType(); // get specific enum type
//        return type.GetField(Enum.GetName(type, j));
//    }
//}

//[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
//internal class BuildingTypeAttr : Attribute
//{
//    public int Capacity { get; private set; }


//    public BuildingTypeAttr(int capacity)
//    {
//        Capacity = capacity;
//    }
//}
