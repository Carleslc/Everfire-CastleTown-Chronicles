using UnityEngine;
using System.Collections;

public class AnimalManager : MovingEntityManager {

    Animal animal;
    public void Init(Animal animal)
    {
        this.animal = animal;
        DrawAnimal();
        base.Init(animal);
    }
    private void DrawAnimal()
    {
        GameObject body = Instantiate(PrefabLoader.GetAnimalBody(animal.Type), Vector2.zero,
    Quaternion.identity) as GameObject;        
        body.transform.SetParent(transform, false);
    }
}
