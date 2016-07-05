using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarehouseTextUI : MonoBehaviour {

    [SerializeField]
    private Text warehouseText;
    private Warehouse warehouse;
    // Use this for initialization
    void Start() {
        warehouse = World.PlayerVillage.Warehouse;
        UpdateResources();
    }

    public void OnEnable() {
        EventManager.StartListening(EventManager.EventType.OnWarehouseUpdated, UpdateResources);
    }

    public void OnDisable() {
        EventManager.StopListening(EventManager.EventType.OnWarehouseUpdated, UpdateResources);
    }

    private void UpdateResources() {
        warehouseText.text = "";
        string newLine;
        foreach(ResourceType rt in warehouse.AllowedGoods)
        {
            newLine = rt.ToString() + ": " + warehouse.GetStoredAmount(rt) + "\n";
            warehouseText.text += newLine;
        }
    }
}
