using UnityEngine;
using System.Collections;

public class DialogueOption {
    string text;
    DialogueNode dest;
    private DialogueTree.DialogueOrder order; 

    public DialogueOption(string text, DialogueNode dest, DialogueTree.DialogueOrder order) {
        this.text = text;
        this.dest = dest;
        this.order = order;
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

    public DialogueTree.DialogueOrder Order
    {
        get
        {
            return order;
        }
    }
}
