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
            tree.Root,
            tree
            );
        new DialogueOption(
            "NO",
            negative,
            tree
            );
        tree.ResetConversation();
        dialogues.Add(Dialogue.test, tree);

        DialogueTree tree_work = new DialogueTree(new DialogueNode("What do you want me to do, sir?"));
        DialogueNode affirmative = new DialogueNode("Aye Aye sir!");
        new DialogueOption(
            "Hunter",
            affirmative,
            new DialogueCommand(Job.hunter.ToString(), DialogOrder.changeJob),
            tree_work
            );
        new DialogueOption(
            "Forester",            
            affirmative,
            new DialogueCommand(Job.forester.ToString(), DialogOrder.changeJob),
            tree_work
            );
        tree_work.ResetConversation();
        dialogues.Add(Dialogue.work, tree_work);
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
        test, work
    }
}
