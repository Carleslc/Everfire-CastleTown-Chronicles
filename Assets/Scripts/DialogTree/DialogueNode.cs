using UnityEngine;
using System.Collections.Generic;

public class DialogueNode {
    private List<DialogueOption> options;

    public List<DialogueOption> Options
    {
        get
        {
            return options;
        }

        set
        {
            options = value;
        }
    }

    string content;

    public DialogueNode(string content) {
        this.content = content;
        options = new List<DialogueOption>();
    }
}
