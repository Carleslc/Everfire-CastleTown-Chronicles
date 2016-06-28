using UnityEngine;
using System.Collections;

public class DialogueOption {
    string text;
    DialogueNode dest;
    DialogueNode origin;
    private DialogueCommand command; 

    public DialogueOption(string text, DialogueNode dest, DialogueCommand command, DialogueTree dialogueTree) {
        this.text = text;
        this.dest = dest;
        this.command = command;
        origin = dialogueTree.CurrentNode;
        dialogueTree.AddOption(this);
    }

    public DialogueOption(string text, DialogueNode dest, DialogueTree dialogueTree) {
        this.text = text;
        this.dest = dest;
        command = new DialogueCommand(null, DialogOrder.none);
        origin = dialogueTree.CurrentNode;
        dialogueTree.AddOption(this);
    }

    public string Text
    {
        get
        {
            return text;
        }
    }

    public DialogueNode Dest
    {
        get
        {
            return dest;
        }
    }

    public DialogueCommand Command
    {
        get
        {
            return command;
        }
    }

    public DialogueNode Origin
    {
        get
        {
            return origin;
        }
    }
}
