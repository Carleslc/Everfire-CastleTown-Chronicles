using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerManager))]
public class DialogueManager : MonoBehaviour {
    DialogueTree dialogueTree;
    Player entity;
    PlayerManager playerManager;
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

    void Update() {
        if (inDialogue)
            return;
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("lMao");
            Pos p;
            Debug.Log(p = new Pos(entity.CurrentPosition, playerManager.LastDirectionFaced));
            ITalkable e = World.GetEntityAt(p) as ITalkable;
            if (e != null) {
                e.StartTalking();
                TalkTo(e);
            }
        }
    }

    public void Start() {
        playerManager = GetComponent<PlayerManager>();
        entity = (Player)playerManager.Entity;
        Debug.Log(inDialogue);
    }

    public void TalkTo(ITalkable target) {
        inDialogue = true;
        dialogueTree = target.LoadTree();
        if (dialogueUI == null) {
            dialogueUI = FindObjectOfType<DialogueUI>();
            dialogueUI.Init(this);
        }
        dialogueUI.ConversationStarted();
        dialogueUI.DrawNode(dialogueTree.First.Content);
    }

    public void ShowOptions()
    {
        if (conversationOver)
        {
            dialogueUI.ConversationEnded();
            dialogueTree.ResetConversation();
            inDialogue = false;
            conversationOver = false;
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
