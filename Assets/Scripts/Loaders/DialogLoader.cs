using UnityEngine;
using System.Collections.Generic;

public static class DialogLoader {

    private static Dictionary<Dialogue, DialogueTree> dialogues;

    public static Dictionary<Dialogue, DialogueTree> Dialogues
    {
        get
        {
            if (dialogues == null) {
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

    private static void InitDialogues() {
        DialogueTree tree = new DialogueTree();
        //DialogueNode greetings = tree.AddNode(new DialogueNode("Today is a great day, don't yot think?"));
        //DialogueNode tree.AddNode()
        //tree.AddOption(
            
        //    )
        //dialogues.Add(Dialogue.test, )
    }

    public static DialogueTree LoadDialogueTree(Dialogue dialogue) {
        if (dialogues.ContainsKey(dialogue))
            return dialogues[dialogue];
        else
        {
            throw new System.Exception("DialogueTree " + dialogue.ToString() + " could not be loaded");
        };
    }


    public enum Dialogue{
        test
    }
}
