using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public class Building : Entity
{
    BuildingType buildingType;

    public Building(BuildingType buildingType, Pos location, Village village, int hitPoints) : base(location, village, hitPoints) {
        this.buildingType = buildingType;
    }

    public BuildingType BuildingType
    {
        get
        {
            return buildingType;
        }
    }
}

public enum BuildingType {
    warehouse,
    farm,
    forestersLodge,
    butchery,
    huntersHut
}

