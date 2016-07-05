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
}

//public enum BuildingSize {
//    [BuildingSizeAttr(10, 5)]
//    large10x5,
//    [BuildingSizeAttr(5, 10)]
//    large5x10,
//    [BuildingSizeAttr(5, 5)]
//    medium5x5,
//    [BuildingSizeAttr(3, 3)]
//    little3x3,
//    [BuildingSizeAttr(1, 2)]
//    tiny1x2
//}

//internal static class BuildingSizeExtensions
//{
//    internal static int Width(this BuildingSize bs)
//    {
//        return GetAttr(bs).Width;
//    }

//    internal static int Depth(this BuildingSize bs)
//    {
//        return GetAttr(bs).Depth;
//    }

//    private static BuildingSizeAttr GetAttr(BuildingSize bs)
//    {
//        return (BuildingSizeAttr)Attribute.GetCustomAttribute(ForValue(bs), typeof(BuildingSizeAttr));
//    }

//    private static MemberInfo ForValue(BuildingSize bs)
//    {
//        Type type = bs.GetType(); // get specific enum type
//        return type.GetField(Enum.GetName(type, bs));
//    }
//}

//[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
//internal class BuildingSizeAttr : Attribute
//{
//    public int Width { get; private set; }
//    public int Depth { get; private set; }

//    public BuildingSizeAttr(int width, int depth)
//    {
//        Width = width;
//        Depth = depth;
//   }
//}