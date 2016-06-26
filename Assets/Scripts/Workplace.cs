using UnityEngine;
using System.Collections;

public class Workplace : StorageBuilding
{
    Resource rawMaterial;
    Resource processedGood;

    public Workplace(Job job, int capacity, BuildingType buildingType, Pos location, Village village, int hitPoints) :
        base(capacity, buildingType, location, village, hitPoints)
    {
        rawMaterial = job.RawMaterial();
        processedGood = job.ProcessedGood();
        //We allow to have food and stuff in here, because every Workplace is a home
        allowedGoods.Add(Resource.bread);
        allowedGoods.Add(Resource.sausage);
    }

    public void Process()
    {

    }
}
