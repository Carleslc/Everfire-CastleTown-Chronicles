using UnityEngine;
using System.Collections;

public class HumanManager : MovingEntityManager
{
    Human human;
    public void Init(Human human)
    {
        this.human = human;
        DrawHuman();
        base.Init(human);
    }
    private void DrawHuman()
    {
        GameObject body = Instantiate(PrefabLoader.GetHumanBody(human.Gender, human.BodyType), Vector2.zero,
    Quaternion.identity) as GameObject;
        GameObject hair = Instantiate(PrefabLoader.GetHumanHair(human.Gender, human.HairType), Vector2.zero,
            Quaternion.identity) as GameObject;
        body.transform.SetParent(transform, false);
        hair.transform.SetParent(transform, false);
    }
}
