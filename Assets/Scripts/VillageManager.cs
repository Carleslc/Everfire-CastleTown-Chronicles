using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    private List<EntityManager> entityManagers = new List<EntityManager>();
    private Village village;
    private Entity[] entities;
    private Environment environment;

    public void Init(Environment environment, Village village) {
        this.village = village;
        entities = this.village.GetEntities();
        //Debug.Log(village.Name + "Has the following entities: " + village.GetEntityAt(new Pos(0, 0)));
        this.environment = environment;
        InitGraphics();
    }

    private void InitGraphics() {
        foreach (Entity e in entities) {
            if (e is Human)
            {               
                Human h = (Human)e;                
                GameObject humanPrefab = Instantiate(PrefabLoader.GetHumanBlankPrefab(), environment.GetWorldPos(e.CurrentPosition),
                    Quaternion.identity) as GameObject;

                GameObject humanPrefabVisuals = Instantiate(PrefabLoader.GetHumanVisualPrefab(h.Gender, h.Job), Vector2.zero,
                    Quaternion.identity) as GameObject;

                humanPrefabVisuals.transform.SetParent(humanPrefab.transform, false);
                humanPrefab.transform.SetParent(transform, false);
                EntityManager entityManager = humanPrefab.GetComponent<EntityManager>();
                entityManager.Init(h);
                entityManagers.Add(entityManager);
            }
        }
    }

}
