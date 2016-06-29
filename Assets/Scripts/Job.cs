using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public enum Job
{
    [JobAttr(ResourceType.wheat, ResourceType.grain)]
    farmer,
    [JobAttr(ResourceType.tree, ResourceType.wood)]
    forester,
    [JobAttr(ResourceType.meat, ResourceType.sausage)]
    butcher,
    [JobAttr(ResourceType.deer, ResourceType.meat)]
    hunter,
    //this enum type is used in WorkerManagerEditor.cs
    dirty
}


internal static class JobExtensions
{
    internal static ResourceType RawMaterial(this Job j)
    {
        return GetAttr(j).RawMaterial;
    }

    internal static ResourceType ProcessedGood(this Job j)
    {
        return GetAttr(j).ProcessedGood;
    }
    private static JobAttr GetAttr(Job j)
    {
        return (JobAttr)Attribute.GetCustomAttribute(ForValue(j), typeof(JobAttr));
    }

    private static MemberInfo ForValue(Job j)
    {
        Type type = j.GetType(); // get specific enum type
        return type.GetField(Enum.GetName(type, j));
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
internal class JobAttr : Attribute
{
    public ResourceType RawMaterial { get; private set; }
    public ResourceType ProcessedGood { get; private set; }


    public JobAttr(ResourceType rawMaterial, ResourceType processedGood)
    {
        RawMaterial = rawMaterial;
        ProcessedGood = processedGood;
    }
}

