using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    int maxOptionCols;
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    GameObject optionsPanel;
    [SerializeField]
    GameObject optionPrefab;

    private GameObject[] optionsObjects;

    DialogueManager dialogueManager;
    /// <summary>
    /// USE THE PROPERTY
    /// </summary>
    private int _opSel = 0;
    private int nOfOptions = -1;

    private bool optionsShown = false;

    private int optionSelected
    {
        get
        {
            return _opSel;
        }

        set
        {
            _opSel = value >= nOfOptions || value < 0 ? 0 : value;
            Debug.Log("Selected option: " + _opSel);
            
        }
    }

    public void Init(DialogueManager dialogueManager)
    {
        this.dialogueManager = dialogueManager;
    }

    public void ConversationEnded() {
        optionSelected = 0;
        nOfOptions = -1;
        gameObject.SetActive(false);
    }

    public void DrawNode(string text)
    {
        dialogueText.gameObject.SetActive(true);
        optionsPanel.SetActive(false);
        dialogueText.text = text;
        optionsShown = false;
    }

    public void ShowOptions(string[] options)
    {
        Debug.Log("ShowOptions");
        dialogueText.gameObject.SetActive(false);
        optionsPanel.SetActive(true);
        if (optionsObjects != null) {
            foreach (GameObject go in optionsObjects) {
                Destroy(go);
            }
        }
        optionsObjects = new GameObject[options.Length];
        int offsetX = 0;
        int offsetY = 0;
        for (int i = 0; i < options.Length; i++)
        {
            GameObject option = Instantiate(optionPrefab, new Vector2(offsetX * 80, -offsetY * 40), Quaternion.identity)
                as GameObject;
            option.transform.SetParent(optionsPanel.transform, false);
            option.GetComponent<Text>().text = options[i];
            optionsObjects[i] = option;
            //this makes sense, believe me
            if (++offsetX > maxOptionCols) {
                offsetX = -1;
                ++offsetY;
            }
            ++offsetX;
        } 
        optionsShown = true;
        nOfOptions = options.Length;
    }

    void Update()
    {
        //TODO make it like the player's with a class 4 him, maybe call it PlayerInput? so kewl

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!optionsShown)
                dialogueManager.ShowOptions();
        }
        if (!optionsShown)
            return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            optionSelected += 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            optionSelected -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.OptionChosen(optionSelected);
        }        
    }


}
