using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    DialogueTree dialogueTree;
    [SerializeField]
    DialogueUI dialogueUI;
    static bool inDialogue = false;
    private bool conversationOver = false;

    public static bool InDialogue
    {
        get
        {
            return inDialogue;
        }
    }

    public void TalkTo(ITalkable target) {
        inDialogue = true;
        dialogueTree = target.LoadTree();
        if (dialogueUI == null) {
            dialogueUI = FindObjectOfType<DialogueUI>();
        }
        dialogueUI.Init(this);
        dialogueUI.DrawNode(dialogueTree.First.Content);
    }

    public void ShowOptions()
    {
        if (conversationOver)
        {
            dialogueUI.ConversationEnded();
            inDialogue = false;
            return;
        }
        DialogueOption[] dialogueOptions = dialogueTree.CurrentNode.Options.ToArray();
        string[] ret = new string[dialogueOptions.Length];
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            ret[i] = dialogueOptions[i].Text;
        }
        dialogueUI.ShowOptions(ret);
    }

    public void OptionChosen(int optionNumber) {
        if (!dialogueTree.SelectOption(optionNumber))
        {
            conversationOver = true;
        }
        dialogueUI.DrawNode(dialogueTree.CurrentNode.Content);
    }


}
