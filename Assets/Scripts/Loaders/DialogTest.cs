using UnityEngine;
using System.Collections;

public class DialogTest : MonoBehaviour {
    DialogueTree tree;
    // Use this for initialization
    void Start () {
        tree = DialogueLoader.LoadDialogueTree(DialogueLoader.Dialogue.test);
        UpdateDialogue();
        
    }

    private void UpdateDialogue() {
        Debug.Log(tree.CurrentNode.Content);
        DialogueOption[] options = tree.CurrentNode.Options.ToArray();
        for (int i = 0; i < options.Length; i++)
        {
            Debug.Log(options[i].Text);
        }
    }

    private void SelectOption(int index)
    {
        if (tree.SelectOption(index))
        {
            UpdateDialogue();
        }
        else {
            Debug.Log(tree.CurrentNode.Content);
            Debug.Log("Conversation ended");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectOption(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectOption(0);
        }
    }
}
