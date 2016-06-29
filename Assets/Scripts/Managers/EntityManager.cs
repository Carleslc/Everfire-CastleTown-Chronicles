using UnityEngine;
using System.Collections;
using System;

public class EntityManager : MonoBehaviour, ITalkable  {
    Entity entity;
    public void Init(Entity entity)
    {
        this.entity = entity;
        DrawEntity();
    }

    void DrawEntity() {

    }

    public virtual void Destroy()
    {
        entity.Destroy();
    }

    public virtual void Kill()
    {
        entity.Kill();
    }

    public DialogueTree LoadTree() {
        return DialogueLoader.LoadDialogueTree(DialogueLoader.Dialogue.test);
    }

    public void ProcessCommands(DialogueCommand[] commands)
    {
        return;
    }
}
