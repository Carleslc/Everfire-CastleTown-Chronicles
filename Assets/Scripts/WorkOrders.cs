using UnityEngine;
using System.Collections.Generic;

public static class WorkOrders {
        
    /// <summary>
    /// To be called when there is not enough raw material stored in the workplace.
    /// </summary>
    /// <param name="ammount"> The amount of raw material to be gathered.</param>
    public static void GatherRawMaterial(Worker worker, Workplace workplace, int ammount) {

    }

    /// <summary>
    /// To be called when there is enough raw material stored in the workplace and more processed goods are needed.
    /// </summary>
    /// <param name="ammount"></param>
    public static void ProcessRawMaterial(Worker worker, Workplace workplace, int ammount) {

    }

    /// <summary>
    /// To be called when there are enough processed goods in the workplace to be carried to the warehouse.
    /// </summary>
    public static void StoreGoods(Worker worker, Building warehouse) {
        
    }
}