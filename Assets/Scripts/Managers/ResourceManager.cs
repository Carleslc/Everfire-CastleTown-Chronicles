using UnityEngine;
using System.Collections;

public class ResourceManager : EntityManager {

    Resource resource;
    public void Init(Resource resource)
    {
        this.resource = resource;
        DrawResource();
        base.Init(resource);
    }
    private void DrawResource()
    {
        GameObject resPrefab = Instantiate(PrefabLoader.GetResource(resource.Type), Vector2.zero,
    Quaternion.identity) as GameObject;
        resPrefab.transform.SetParent(transform, false);
    }


}
