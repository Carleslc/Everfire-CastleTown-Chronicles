using UnityEngine;
using System.Collections.Generic;

public class UIVillagesPanel : UIDebugPanel {

    private void OnEnable() {
        //EventManager.StartListening(EventManager.EventType.OnNewEntity, UpdateContent);
        //EventManager.StartListening(EventManager.EventType.OnNewVillage, UpdateContent);
        UpdatePanel();
    }

    /*private void OnDisable() {
        //EventManager.StopListening(EventManager.EventType.OnNewEntity, UpdateContent);
        EventManager.StopListening(EventManager.EventType.OnNewVillage, UpdateContent);
    }*/

    protected override void UpdateContent()
    {
        base.UpdateContent();
        IEnumerator<Village> villages = World.GetVillages();
        villages.MoveNext();
        bool isFinished = false;
        for (int i = 0; !isFinished; i++)
        {
            isFinished = true;
        }
        
    }
}
