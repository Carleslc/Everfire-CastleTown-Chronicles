using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    private static float lerpRate = 15;
    private static string humanPrefabsPath = "Prefabs/Humans/human";

    List<EntityManager> entityManagers = new List<EntityManager>();
    Village village;
    Dictionary<Pos, Entity> entities;
    Environment environment;

    public void Init(Environment environment, Village village) {
        this.village = village;
        entities = village.GetEntities();
        //Debug.Log(village.Name + "Has the following entities: " + village.GetEntityAt(new Pos(0, 0)));
        this.environment = environment;
        InitGraphics();
    }

    private void InitGraphics() {
        foreach (KeyValuePair<Pos, Entity> pair in entities) {
            if (pair.Value is Human)
            {               
                Human h = (Human)pair.Value;
                string gender = h.Gender.ToString();
                GameObject humanPrefab = Instantiate(Resources.Load(humanPrefabsPath), environment.GetWorldPos(pair.Key),
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
