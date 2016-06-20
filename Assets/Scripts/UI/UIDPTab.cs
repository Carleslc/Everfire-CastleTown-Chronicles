using UnityEngine;
using System.Collections;

public class UIDPTab : MonoBehaviour {

    private UIDebugPanel uiDebugPanel;

    public void ButtonClicked() {
        if(uiDebugPanel == null)
            uiDebugPanel = transform.GetComponentInParent<UIDebugPanel>();
        uiDebugPanel.ChangePanel(gameObject.name);
    }
    	
	// Update is called once per frame
	void Update () {
	
	}
}
