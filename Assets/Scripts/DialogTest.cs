using UnityEngine;
using System.Collections;
using System;

public class DialogTest : MonoBehaviour, ITalkable {
    DialogueTree tree;
    [SerializeField]
    DialogueManager manager;
    private float delay = .1f;
    private float sTime = 0;
    private bool kek = false;

    void Start() {
        sTime = Time.time;

    }

    public DialogueTree LoadTree()
    {
        return DialogueLoader.LoadDialogueTree(DialogueLoader.Dialogue.test);
    }

    public void ProcessCommands(DialogueCommand[] commands)
    {
        throw new NotImplementedException();
    }

    void Update() {
        if (kek)
            return;
        if (sTime + delay > Time.time) {
            kek = true;
            manager = FindObjectOfType<DialogueManager>();
            manager.TalkTo(this);
        }
    }
    // Use this for initialization
    //void Start () {
    //    tree = DialogueLoader.LoadDialogueTree(DialogueLoader.Dialogue.test);
    //    UpdateDialogue();

    //}

    //private void UpdateDialogue() {
    //    Debug.Log(tree.CurrentNode.Content);
    //    DialogueOption[] options = tree.CurrentNode.Options.ToArray();
    //    for (int i = 0; i < options.Length; i++)
    //    {
    //        Debug.Log(options[i].Text);
    //    }
    //}

    //private void SelectOption(int index)
    //{
    //    if (tree.SelectOption(index))
    //    {
    //        UpdateDialogue();
    //    }
    //    else {
    //        Debug.Log(tree.CurrentNode.Content);
    //        Debug.Log("Conversation ended");
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        SelectOption(1);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        SelectOption(0);
    //    }
    //}
}
