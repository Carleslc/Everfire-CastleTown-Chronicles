using UnityEngine;
using System.Collections.Generic;

public abstract class StorageBuilding : Building {
#pragma warning disable 0414
    int capacity;
#pragma warning disable 0414
    Dictionary<ResourceType, int> storedGoods;
    protected List<ResourceType> allowedGoods;

    public StorageBuilding(int capacity, BuildingType buildingType, int width, int depth, Pos location, Village village, int hitPoints) : 
        base(buildingType, width, depth, location, village, hitPoints)
    {
        if (buildingType == BuildingType.warehouse)
            village.Warehouse = this;
        this.capacity = capacity;
        storedGoods = new Dictionary<ResourceType, int>();
        allowedGoods = new List<ResourceType>();
    }

    public void Store(ResourceType resource, int ammount)
    {
        //capacity will be used here
    }
}