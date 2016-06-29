using UnityEngine;
using System.Collections.Generic;


//DialogueTree is composed of nodes and options. A node can have various options, and they redirect to other nodes.
public class DialogueTree
{
    //private List<DialogueNode> dialogNodes;
    private List<DialogueCommand> commands;
    DialogueNode root;
    //private List<DialogueNode> currentNodes;
    //int currentNodeIndex = 0;

    DialogueNode currentNode;
    DialogueOption lastOptionSelected;

    public DialogueNode CurrentNode
    {
        get
        {
            if (currentNode == null) {
                currentNode = root;
            }
            return currentNode;
        }
    }

    public List<DialogueCommand> Commands
    {
        get
        {
            return commands;
        }
    }

    public DialogueNode First
    {
        get
        {
            return root;
        }
    }

    public void AddOption(DialogueOption dialogueOption)
    {
        currentNode.Options.Add(dialogueOption);        
    }

        /// <summary>
        /// Use this function only when building the tree.
        /// </summary>
        /// <param name="child">The nth child of the node, starting from the first added</param>
    public void EditChild(int child) {
        lastOptionSelected = currentNode.Options[child];
        currentNode = currentNode.Options[child].Dest;
    }

    /// <summary>
    /// Use this function only when building the tree.
    /// </summary>
    public void EditParent() {
        currentNode = lastOptionSelected.Origin;
    }

    public bool SelectOption(int option)
    {
        //We go to the nth node                     
        DialogueOption selected = currentNode.Options[option];
        Debug.Log("Selected option: " + selected.Text);
        currentNode = selected.Dest;

        if (selected.Command.order != DialogOrder.none) {
            commands.Add(selected.Command);
        }
        //We check if the current node has any options
        //note that the current node's been updated, so
        //we're really checking the nth son of the previous currentNode
        if (currentNode.Options.Count <= 0)
            return false;
        return true;
    }

    public DialogueOption[] GetOptions()
    {
        return currentNode.Options.ToArray();
    }

    public DialogueTree(DialogueNode root)
    {
        commands = new List<DialogueCommand>();
        this.root = root;
        currentNode = null;
    }

    public void ResetConversation()
    {
        currentNode = root;
    }
}

public struct DialogueCommand
{
    public string parameters;
    public DialogOrder order;
    public DialogueCommand(string parameters, DialogOrder order)
    {
        this.parameters = parameters;
        this.order = order;
    }
}

public enum DialogOrder
{
    none, changeJob
}