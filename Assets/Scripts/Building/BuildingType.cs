using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public enum BuildingType
{
    //Job, Capacity, HitPoints, Width, Depth
    [BuildingTypeAttr(200, 200, 10, 5)]
    warehouse,
    [BuildingTypeAttr(Job.forester, 20, 100, 5, 5)]
    foresterslodge,     //cabaña del leñador
    [BuildingTypeAttr(Job.stonecutter, 20, 100, 5, 5)]
    stonequarry,        //cantera de piedra
    [BuildingTypeAttr(Job.hunter, 20, 100, 5, 5)]
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

    internal static int HitPoints(this BuildingType bt)
    {
        return GetAttr(bt).HitPoints;
    }
    internal static int Capacity(this BuildingType bt)
    {
        return GetAttr(bt).Capacity;
    }
    internal static Job Job(this BuildingType bt)
    {
        return GetAttr(bt).Job;
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
    public int HitPoints { get; private set; }
    //We leave it -1 if it's not an storage building
    public int Capacity { get; private set; }
    //We leave it to Job.none if it's not a workplace
    public Job Job { get; private set; }

    public BuildingTypeAttr(Job job, int capacity, int hitPoints, int width, int depth)
    {
        Job = job;
        Capacity = capacity;
        HitPoints = hitPoints;
        Width = width;
        Depth = depth;
    }

    public BuildingTypeAttr(int capacity, int hitPoints, int width, int depth)
    {
        Job = Job.none;
        Capacity = capacity;
        HitPoints = hitPoints;
        Width = width;
        Depth = depth;
    }

    public BuildingTypeAttr(int hitPoints, int width, int depth) {
        Job = Job.none;
        Capacity = -1;
        HitPoints = hitPoints;
        Width = width;
        Depth = depth;
    }
}