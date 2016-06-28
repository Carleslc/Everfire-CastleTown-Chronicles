using UnityEngine;
using System.Collections.Generic;

public class DialogueNode {
    private List<DialogueOption> options;
    string content;


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

    public string Content
    {
        get
        {
            return content;
        }
    }

    public DialogueNode(string content) {
        this.content = content;
        options = new List<DialogueOption>();
    }
}
