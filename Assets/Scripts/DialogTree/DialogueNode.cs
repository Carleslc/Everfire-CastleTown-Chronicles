using UnityEngine;
using System.Collections.Generic;

public class DialogueNode {
    /// <summary>
    /// This is the possible options the player has to continue through the dialogTree. If there are any,
    /// we assume it's an end-of-conversation node
    /// </summary>
    private List<DialogueOption> options;
    /// <summary>
    /// This is displayed as the text of the dialogueNode.
    /// </summary>
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
