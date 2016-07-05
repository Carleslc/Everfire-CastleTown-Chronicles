using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public enum BuildingType
{
    [BuildingTypeAttr(200, 10, 5)]
    warehouse,
    [BuildingTypeAttr(Job.forester, 20, 5, 5)]
    foresterslodge,     //cabaña del leñador
    [BuildingTypeAttr(Job.stonecutter, 20, 5, 5)]
    stonequarry,        //cantera de piedra
    [BuildingTypeAttr(Job.hunter, 20, 5, 5)]
    huntershut,         //cabaña del cazador
}

internal static class BuildingTypeExtensions
{
    internal static int Width(this BuildingType bt)
    {
        return GetAttr(bt).Width;
    }

    internal static int Depth(this BuildingType bt)
    {
        return GetAttr(bt).Depth;
    }

    private static BuildingTypeAttr GetAttr(BuildingType bt)
    {
        return (BuildingTypeAttr)Attribute.GetCustomAttribute(ForValue(bt), typeof(BuildingTypeAttr));
    }

    private static MemberInfo ForValue(BuildingType bt)
    {
        Type type = bt.GetType(); // get specific enum type
        return type.GetField(Enum.GetName(type, bt));
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
internal class BuildingTypeAttr : Attribute
{
    public int Width { get; private set; }
    public int Depth { get; private set; }
    //We leave it -1 if it's not an storage building
    public int Capacity { get; private set; }
    //We leave it to Job.none if it's not a workplace
    public Job Job { get; private set; }

    public BuildingTypeAttr(Job job, int capacity, int width, int depth)
    {
        Job = job;
        Capacity = capacity;
        Width = width;
        Depth = depth;
    }

    public BuildingTypeAttr(int capacity, int width, int depth)
    {
        Job = Job.none;
        Capacity = capacity;
        Width = width;
        Depth = depth;
    }

    public BuildingTypeAttr(int width, int depth) {
        Job = Job.none;
        Capacity = -1;
        Width = width;
        Depth = depth;
    }
}