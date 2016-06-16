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
        this.environment = environment;
        InitGraphics();
    }

    private void InitGraphics() {
        foreach (Entity e in entities) {
            if (e is Human)
            {               
                InstantiateHuman((Human)e);
            }
        }
    }

    private void InstantiateHuman(Human h) {

        GameObject humanPrefab = Instantiate(PrefabLoader.GetHumanBlank(), environment.GetWorldPos(h.CurrentPosition),
                    Quaternion.identity) as GameObject;

        GameObject body = Instantiate(PrefabLoader.GetHumanBody(h.Gender, h.BodyType), Vector2.zero,
            Quaternion.identity) as GameObject;

        GameObject clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(h.Job), Vector2.zero,
            Quaternion.identity) as GameObject;

        GameObject hair = Instantiate(PrefabLoader.GetHumanHair(h.Gender, h.HairType), Vector2.zero,
            Quaternion.identity) as GameObject;

        //We set all the human's body parts as its children
        body.transform.SetParent(humanPrefab.transform, false);
        clothes.transform.SetParent(humanPrefab.transform, false);
        hair.transform.SetParent(humanPrefab.transform, false);

        //The human is the child of the village
        humanPrefab.transform.SetParent(transform, false);
        EntityManager entityManager = humanPrefab.GetComponent<EntityManager>();
        //We activate the entity manager of our newly instantiated human.
        entityManager.Init(h);
        entityManagers.Add(entityManager);
    }
}
