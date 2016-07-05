using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class Building : Entity
{
    int width;
    int depth;
    BuildingType buildingType;

    public Building(BuildingType buildingType, int width, int depth, Pos location, Village village, int hitPoints) : base(location, village, hitPoints)
    {
        this.buildingType = buildingType;
        this.width = width;
        this.depth = depth;
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