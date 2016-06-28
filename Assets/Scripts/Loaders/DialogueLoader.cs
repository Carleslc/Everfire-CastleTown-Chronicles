using UnityEngine;
using System.Collections.Generic;

public static class DialogueLoader
{

    private static Dictionary<Dialogue, DialogueTree> dialogues;

    public static Dictionary<Dialogue, DialogueTree> Dialogues
    {
        get
        {
            if (dialogues == null)
            {
                dialogues = new Dictionary<Dialogue, DialogueTree>();
                InitDialogues();
            }
            return dialogues;
        }

        set
        {
            dialogues = value;
        }
    }

    private static void InitDialogues()
    {
        DialogueTree tree = new DialogueTree(new DialogueNode("Today is a great day, don't yot think?"));
        DialogueNode positive = new DialogueNode("Hah! Great minds think alike!");
        DialogueNode negative = new DialogueNode("Come on dude don't be like that. Do you want to answer again?");
        new DialogueOption(
            "YES",
            positive,
            tree
            );
        new DialogueOption(
            "NO",
            negative,
            tree
            );
        tree.EditChild(1);
        new DialogueOption(
            "YES",
            tree.First,
            tree
            );
        new DialogueOption(
            "NO",
            negative,
            tree
            );
        tree.ResetConversation();
        dialogues.Add(Dialogue.test, tree);
    }

    public static DialogueTree LoadDialogueTree(Dialogue dialogue)
    {
        if (Dialogues.ContainsKey(dialogue))
            return Dialogues[dialogue];
        else
        {
            throw new System.Exception("DialogueTree " + dialogue.ToString() + " could not be loaded");
        };
    }


    public enum Dialogue
    {
        test
    }
}
