using UnityEngine;
using System.Collections.Generic;

public class DialogueTree
{
    private List<DialogueNode> dialogNodes;
    private List<DialogueOrder> orders;

    DialogueNode currentNode;

    public DialogueNode CurrentNode
    {
        get
        {
            return currentNode;
        }
    }

    public List<DialogueOrder> Orders
    {
        get
        {
            return orders;
        }
    }

    public DialogueNode AddNode(DialogueNode newNode)
    {
        dialogNodes.Add(newNode);
        currentNode = newNode;
        return newNode;
    }

    public void AddOption(string text, DialogueNode node, DialogueNode dest)
    {
        //Add if it's not already there
        if (!dialogNodes.Contains(dest))
            AddNode(dest);

        if (!dialogNodes.Contains(node))
            AddNode(node);

        DialogueOption opt;
        //opt = new DialogueOption(text, dest);
        //node.Options.Add(opt);
    }

    public void OptionSelected(int option)
    {
        DialogueOption selected = currentNode.Options[option];
        currentNode = selected.Dest;

        if (selected.Order != DialogueOrder.none) {
        }
    }

    public DialogueOption[] GetOptions()
    {
        return currentNode.Options.ToArray();
    }

    public DialogueTree()
    {
        dialogNodes = new List<DialogueNode>();
        currentNode = null;
    }

    public enum DialogueOrder {
        none
    }

}
