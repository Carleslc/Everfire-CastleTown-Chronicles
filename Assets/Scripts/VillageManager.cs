using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    private static float lerpRate = 15;
    private static string humanPrefabsPath = "Prefabs/Humans/human";

    List<EntityManager> entityManagers = new List<EntityManager>();
    Village village;
    Entity[] entities;
    Environment environment;

    public void Init(Environment environment, Village village) {
        this.village = village;
        entities = village.GetEntities();
        //Debug.Log(village.Name + "Has the following entities: " + village.GetEntityAt(new Pos(0, 0)));
        this.environment = environment;
        InitGraphics();
    }

    private void InitGraphics() {
        foreach (Entity e in entities) {
            if (e is Human)
            {               
                Human h = (Human)e;
                string gender = h.Gender.ToString();
                GameObject humanPrefab = Instantiate(Resources.Load(humanPrefabsPath), environment.GetWorldPos(e.CurrentPosition),
                    Quaternion.identity) as GameObject;

                GameObject humanPrefabVisuals = Instantiate(Resources.Load(humanPrefabsPath + "_" + gender), Vector2.zero,
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
