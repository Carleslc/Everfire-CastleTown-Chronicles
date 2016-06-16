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
                GameObject humanPrefab = Instantiate(PrefabLoader.GetHumanBlank(), environment.GetWorldPos(e.CurrentPosition),
                    Quaternion.identity) as GameObject;

                GameObject body = Instantiate(PrefabLoader.GetHumanBody(h.Gender, 1), Vector2.zero,
                    Quaternion.identity) as GameObject;

                GameObject clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(h.Job), Vector2.zero,
                    Quaternion.identity) as GameObject;

                GameObject hair = Instantiate(PrefabLoader.GetHumanHair(h.Gender, 1), Vector2.zero,
                    Quaternion.identity) as GameObject;

                body.transform.SetParent(humanPrefab.transform, false);
                clothes.transform.SetParent(humanPrefab.transform, false);
                hair.transform.SetParent(humanPrefab.transform, false);

                humanPrefab.transform.SetParent(transform, false);
                EntityManager entityManager = humanPrefab.GetComponent<EntityManager>();
                entityManager.Init(h);
                entityManagers.Add(entityManager);
            }
        }
    }

}
