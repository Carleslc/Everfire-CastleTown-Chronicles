using UnityEngine;
using System.Collections.Generic;


//DialogueTree is composed of nodes and options. A node can have various options, and they redirect to other nodes.
public class DialogueTree
{
    private List<DialogueCommand> commands;
    DialogueNode root;

    DialogueNode currentNode;
    DialogueOption lastOptionSelected;

    /// <summary>
    /// Current node we're treating. By default it's the Root. If SelectOption, EditChild or EditParent are called,
    /// the current node will change.
    /// </summary>
    public DialogueNode CurrentNode
    {
        get
        {
            if (currentNode == null)
            {
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

    public DialogueNode Root
    {
        get
        {
            return root;
        }
    }

    /// <summary>
    /// We add an option and a child to the CurrentNode we're treating. Each dialogueOption is a link with a child.
    /// </summary>
    /// <param name="dialogueOption"></param>
    public void AddOption(DialogueOption dialogueOption)
    {
        currentNode.Options.Add(dialogueOption);
    }

    /// <summary>
    /// Use this function only when building the tree. The CurrentNode will now be the nth child of the CurrentNode.
    /// </summary>
    /// <param name="n">The nth child of the node, starting from the first added</param>
    public void EditChild(int n)
    {
        lastOptionSelected = currentNode.Options[n];
        currentNode = currentNode.Options[n].Dest;
    }

    /// <summary>
    /// Use this function only when building the tree. The CurrentNode will now be the originNode of the last option selected.
    /// </summary>
    public void EditParent()
    {
        currentNode = lastOptionSelected.Origin;
    }

    /// <summary>
    /// Navigate to the nth option of the currentNode. All orders associated with the option selected will be added to Orders.
    /// </summary>
    /// <param name="n">The nth option to select.</param>
    /// <returns></returns>
    //NOTE: We separate EditChild from this one because although they do similar things, this one goes on collecting
    //the orders from every option it'll travel through.
    public bool SelectOption(int n)
    {
        //We go to the nth node                     
        DialogueOption selected = currentNode.Options[n];
        currentNode = selected.Dest;
        if (selected.Command.order != DialogOrder.none)
        {
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

    /// <summary>
    /// Call it after a conversation to reset the tree to its original state.
    /// </summary>
    public void ResetConversation()
    {
        //erase the commands
        commands = new List<DialogueCommand>();
        currentNode = root;
    }
}

//This struct if for storing orders with parameters.
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