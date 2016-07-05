using UnityEngine;
using System.Collections.Generic;

public abstract class StorageBuilding : Building
{
#pragma warning disable 0414
    int maxCapacity;
    int usedCapacity;
    bool isFull = false;
#pragma warning disable 0414
    protected Dictionary<ResourceType, int> storedGoods;
    protected List<ResourceType> allowedGoods;

    public int Capacity
    {
        get
        {
            return maxCapacity;
        }
    }

    public bool IsFull
    {
        get
        {
            return isFull;
        }
    }

    public int UsedCapacity
    {
        get
        {
            return usedCapacity;
        }

        set
        {
            usedCapacity = value;
            if (usedCapacity >= maxCapacity)
            {
                isFull = true;
                usedCapacity = maxCapacity;
            }
        }
    }

    public StorageBuilding(int maxCapacity, BuildingType buildingType, int width, int depth, Pos location, Village village, int hitPoints) :
        base(buildingType, width, depth, location, village, hitPoints)
    {
        if (buildingType == BuildingType.warehouse)
            village.Warehouse = this;
        this.maxCapacity = maxCapacity;
        UsedCapacity = 0;
        storedGoods = new Dictionary<ResourceType, int>();
        allowedGoods = new List<ResourceType>();
    }

    /// <summary>
    /// Deletes an <c>ammount</c> ammount of the resource <c>resource</c>. Returns the ammount that remains of the given
    /// resourceType.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="ammount"></param>
    /// <returns></returns>
    public int Retrieve(ResourceType resource, int ammount)
    {
        if (storedGoods.ContainsKey(resource))
        {
            storedGoods[resource] -= ammount;
            if (storedGoods[resource] < 0)
            {
                int ret = storedGoods[resource];
                storedGoods[resource] = 0;
                return ret;
            }
        }

        //If the storedGood does not exist, we return the ammount to be stored, which means that nothing was retrieved.
        return ammount;
    }

    public int GetStoredAmount(ResourceType resource)
    {
        int ret = 0;
        storedGoods.TryGetValue(resource, out ret);
        return ret;
    }

    /// <summary>
    /// Stores an ammount of a given resource in this building. Returns 0 if it is successful. If it returns more that that it
    /// means that the returned ammount of resources could not be stored in there.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="ammount"></param>
    /// <returns></returns>
    public int Store(ResourceType resource, int ammount)
    {
        if (!allowedGoods.Contains(resource))
        {
            throw new System.Exception("Building " + BuildingType.ToString() + " does not allow this kind of resource: " +
                resource.ToString());
        }
        if (storedGoods.ContainsKey(resource))
        {
            storedGoods[resource] += ammount;
        }
        else
        {
            storedGoods.Add(resource, ammount);
        }

        int notStoredAmmount = 0;
        if (usedCapacity + ammount > maxCapacity)
        {
            notStoredAmmount = maxCapacity - usedCapacity + ammount;
        }
        //We dont care if it overflows, the property takes care of it.
        UsedCapacity += ammount;
        return notStoredAmmount;
    }
}