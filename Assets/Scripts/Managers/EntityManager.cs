using UnityEngine;
using System.Collections;

public class EntityManager : MonoBehaviour {
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
}
