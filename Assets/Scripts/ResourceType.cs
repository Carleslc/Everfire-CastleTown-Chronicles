using System;
using System.Reflection;

//I don't think it's wise to define superior tiers of Resources as childs of Entity given their "located" nature, iykwim
public enum ResourceType
{
    //If the ProductionTime is 0, the resource can be refined on its location, no need to go to a Workplace.
    [ResourceTypeAttr(0)]
    stone,
    //This stone block is the stone once it's collected, and not further refined!
    [ResourceTypeAttr(200)]
    stoneBlock,
    [ResourceTypeAttr(0)]
    deer,
    [ResourceTypeAttr(50)]
    meat,
    sausage,
    wheat,
    grain,
    flour,
    bread,
    tree,
    wood,
    plank
}

internal static class ResourceTypeExtensions
{
    internal static int ProductionTime(this ResourceType rt)
    {
        return GetAttr(rt).ProductionTime;
    }

    private static ResourceTypeAttr GetAttr(ResourceType rt)
    {
        return (ResourceTypeAttr)Attribute.GetCustomAttribute(ForValue(rt), typeof(ResourceTypeAttr));
    }

    private static MemberInfo ForValue(ResourceType rt)
    {
        Type type = rt.GetType(); // get specific enum type
        return type.GetField(Enum.GetName(type, rt));
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
internal class ResourceTypeAttr : Attribute
{
    public int ProductionTime { get; private set; }    

    public ResourceTypeAttr(int productionTime)
    {
        ProductionTime = productionTime;
    }    
}

