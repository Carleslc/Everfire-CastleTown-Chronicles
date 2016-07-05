using UnityEngine;
using System.Collections.Generic;
using System;

public class Warehouse : StorageBuilding {

	public Warehouse (BuildingType buildingType, Pos location, Village village) :
        base(buildingType, location, village)
    {
        village.Warehouse = this;
        allowedGoods = new List<ResourceType>((ResourceType[])Enum.GetValues(typeof(ResourceType)));
        Debug.Log("Warehouse " + allowedGoods.Count);
        storedGoods.Add(ResourceType.deer, 20);
    }

    public override int Retrieve(ResourceType resource, int ammount)
    {
        int ret = base.Retrieve(resource, ammount);
        EventManager.TriggerEvent(EventManager.EventType.OnWarehouseUpdated);
        return ret;
    }
    public override int Store(ResourceType resource, int ammount)
    {
        int ret =  base.Store(resource, ammount);
        EventManager.TriggerEvent(EventManager.EventType.OnWarehouseUpdated);
        return ret;
    }
}
