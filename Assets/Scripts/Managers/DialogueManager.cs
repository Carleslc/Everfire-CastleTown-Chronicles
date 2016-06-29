using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerManager))]
public class DialogueManager : MonoBehaviour {
    private DialogueTree dialogueTree;

    private Player entity;
    private ITalkable currentTalker;

    private PlayerManager playerManager;

    private DialogueUI dialogueUI;
    //This keeps the player from moving
    static bool inDialogue = false;
    //This helps us put an end to the conversation.
    private bool conversationOver = false;

    public static bool InDialogue
    {
        get
        {
            return inDialogue;
        }
    }

    public void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        entity = (Player)playerManager.Entity;
    }

    void Update() {
        if (inDialogue)
            return;
        if (Input.GetKeyDown(KeyCode.E)) {
            Pos p = new Pos(entity.CurrentPosition, playerManager.LastDirectionFaced);
            //We get the entity that it's closest to us and check if it implements ITalkable.        
            currentTalker = World.GetEntityAt(p) as ITalkable;
            if (currentTalker != null) {
                StartConversation();
            }
        }
    }

    public void StartConversation() {
        currentTalker.StartTalking();
        inDialogue = true;
        dialogueTree = currentTalker.LoadTree();
        if (dialogueUI == null) {
            //There's only one DilaogueUI, so no worries.
            dialogueUI = FindObjectOfType<DialogueUI>();
            dialogueUI.Init(this);
        }
        dialogueUI.ConversationStarted();
        dialogueUI.DrawNode(dialogueTree.Root.Content);
    }

    private void EndConversation() {
        dialogueUI.ConversationEnded();
        currentTalker.StopTalking();
        currentTalker.ProcessCommands(dialogueTree.Commands.ToArray());
        dialogueTree.ResetConversation();
        inDialogue = false;
        conversationOver = false;
    }

    public void ShowOptions()
    {
        //That means there are no options to show.
        if (conversationOver)
        {
            EndConversation();
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
        //Because this returns false if there are no options, we are safe to assume that it means the conversation ended.
        if (!dialogueTree.SelectOption(optionNumber))
        {
            conversationOver = true;
        }
        dialogueUI.DrawNode(dialogueTree.CurrentNode.Content);
    }


}
