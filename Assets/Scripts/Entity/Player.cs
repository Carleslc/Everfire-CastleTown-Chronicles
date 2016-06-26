using UnityEngine;
using System.Collections;

public class Player : Human {
    private int clothesType;

    private Movement playerInput = Movement.WAIT;

    public Player(int clothesType, string name, Gender gender, int bodyType, int hairType, Pos location, Village village, int hitPoints) :
        base(name, gender, bodyType, hairType, location, village, hitPoints) {
        this.clothesType = clothesType;
        InitialisationCompleted();
    }

    public int ClothesType
    {
        get
        {
            return clothesType;
        }
    }

    public Movement PlayerInput
    {
        set
        {
            playerInput = value;
        }
    }
    //Im overriding Move because we dont need Player to move constantly
    public override bool Move() {
        return Move(playerInput);
    }

    public override Movement NextMovement() {
        return playerInput;
    }
}
