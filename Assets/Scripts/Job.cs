using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public enum Job
{
    [JobAttr(Resource.wheat, Resource.grain)]
    farmer,
    [JobAttr(Resource.tree, Resource.wood)]
    forester,
    [JobAttr(Resource.meat, Resource.sausage)]
    butcher,
    [JobAttr(Resource.deer, Resource.meat)]
    hunter
}


internal static class JobExtensions
{
    internal static Resource RawMaterial(this Job j)
    {
        return GetAttr(j).RawMaterial;
    }

    internal static Resource ProcessedGood(this Job j)
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
    public Resource RawMaterial { get; private set; }
    public Resource ProcessedGood { get; private set; }


    public JobAttr(Resource rawMaterial, Resource processedGood)
    {
        RawMaterial = rawMaterial;
        ProcessedGood = processedGood;
    }
}

