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
        Store(ResourceType.deer, 20);
        EventManager.TriggerEvent(EventManager.EventType.OnWarehouseUpdated);
    }

    public override int Retrieve(ResourceType resource, int ammount)
    {
        EventManager.TriggerEvent(EventManager.EventType.OnWarehouseUpdated);
        return base.Retrieve(resource, ammount);
    }
    public override int Store(ResourceType resource, int ammount)
    {
        EventManager.TriggerEvent(EventManager.EventType.OnWarehouseUpdated);
        return base.Store(resource, ammount);
    }
}
