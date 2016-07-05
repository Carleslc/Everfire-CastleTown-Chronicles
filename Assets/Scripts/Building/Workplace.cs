using UnityEngine;
using System.Collections;

public class Workplace : StorageBuilding
{
    ResourceType rawMaterial;
    ResourceType processedGood;
    int productionGoal;
    int productionProcess = 0;

    public Workplace(Job job, int capacity, BuildingType buildingType, int width, int depth, Pos location, Village village, int hitPoints) :
        base(capacity, buildingType, width, depth, location, village, hitPoints)
    {
        rawMaterial = job.RawMaterial();
        processedGood = job.ProcessedGood();
        InitAllowedGoods();
        productionGoal = processedGood.ProductionTime();
    }

    private void InitAllowedGoods()
    {
        allowedGoods.AddRange(new ResourceType[]
        {
            //We allow to have food and stuff in here, because every Workplace is a home
            ResourceType.bread,
            ResourceType.sausage,
            rawMaterial,
            processedGood
        });
    }

    /// <summary>
    /// Returns the ammount of productionTime remaining unitill the resource is processed.
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    public int Process(int speed)
    {
        int timeLeft = productionProcess - productionGoal;
        if (GetStoredAmount(rawMaterial) > 0)
        {
            //If the raw material IS NOT a map resource
            if (rawMaterial.ProductionTime() > 0)
            {
                int ammount;
                //We have to chech if the rawMaterial is stored in the building
                if (!storedGoods.TryGetValue(rawMaterial, out ammount) || ammount <= 0)
                    throw new System.Exception(rawMaterial.ToString() + " not stored in " + BuildingType.ToString());
                //We do this because with map resources we can't have them stored in our building, we have to collect them.
            }
            productionProcess += speed;
            timeLeft = productionProcess - productionGoal;
            if (timeLeft <= 0)
            {
                if (Retrieve(rawMaterial, 1) == 1)
                    throw new System.Exception("Production cannot be finished in Workplace " + BuildingType.ToString() +
                        " because there is not enough raw material stored.");
                Store(processedGood, 1);
            }
        }
        return timeLeft > 0 ? timeLeft : 0;
    }
}
