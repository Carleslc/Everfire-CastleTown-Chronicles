using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour {

    //private List<EntityManager> entityManagers = new List<EntityManager>();
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

        GameObject humanPrefab = null;

        GameObject body = Instantiate(PrefabLoader.GetHumanBody(h.Gender, h.BodyType), Vector2.zero,
            Quaternion.identity) as GameObject;

        GameObject clothes = null;
        if (h is Villager)
        {
            humanPrefab = Instantiate(PrefabLoader.GetHumanBlank(), environment.GetWorldPos(h.CurrentPosition),
                    Quaternion.identity) as GameObject;
            Villager v = (Villager)h;
            clothes = Instantiate(PrefabLoader.GetHumanWorkClothes(v.Job), Vector2.zero,
                Quaternion.identity) as GameObject;
        }
        else if(h is Player){
            humanPrefab = Instantiate(PrefabLoader.GetPlayerBlank(), environment.GetWorldPos(h.CurrentPosition),
                    Quaternion.identity) as GameObject;
            Player p = (Player)h;
            clothes = Instantiate(PrefabLoader.GetHumanPlayerClothes(p.ClothesType), Vector2.zero,
                Quaternion.identity) as GameObject;
        }

        if (clothes == null) {
            throw new System.Exception("Clothes could not be loaded for the Human " + h.Name);
        }

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
        //entityManagers.Add(entityManager);
    }
}
