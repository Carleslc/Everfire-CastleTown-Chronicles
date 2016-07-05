using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class Building : Entity
{
    int width;
    int depth;
    BuildingType buildingType;

    public Building(BuildingType buildingType, Pos location, Village village) : base(location, village, buildingType.HitPoints())
    {
        this.buildingType = buildingType;
        width = buildingType.Width();
        depth = buildingType.Depth();
    }


    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Depth
    {
        get
        {
            return depth;
        }
    }

    public BuildingType BuildingType
    {
        get
        {
            return buildingType;
        } 
    }
}