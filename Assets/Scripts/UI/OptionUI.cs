using UnityEngine;
using System.Collections;

public class OptionUI : MonoBehaviour {
    [SerializeField]
    private GameObject selector;
    private bool selected = false;

    public bool Selected
    {        
        set
        {
            selected = value;
            selector.SetActive(selected);
        }
    }    
}
