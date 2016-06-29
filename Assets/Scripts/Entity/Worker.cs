using UnityEngine;
using System.Collections;
using System;

public class Worker : Human, ITalkable{
    private Job job;
    private bool isTalking = false;

    public Worker(Job job, string name, Gender gender, int bodyType, int hairType, Pos location, Village village, int hitPoints) :
        base(name, gender, bodyType, hairType, location, village, hitPoints)
    {
        this.job = job;
        InitialisationCompleted();
    }

    public Job Job
    {
        get
        {
            return job;
        }

        set
        {
            job = value;
        }
    }

    public override bool Move()
    {
        return false;
        //if (isTalking)
        //    return false;

        //return base.Move();
    }

    public DialogueTree LoadTree()
    {
        return DialogueLoader.LoadDialogueTree(DialogueLoader.Dialogue.test);
    }

    public void ProcessCommands(DialogueCommand[] commands)
    {
        throw new NotImplementedException();
    }

    public void StartTalking()
    {
        isTalking = true;
    }

    public void StopTalking()
    {
        isTalking = false;
    }
}