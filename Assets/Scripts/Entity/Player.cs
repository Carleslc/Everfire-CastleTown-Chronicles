using UnityEngine;
using System.Collections;

public class Player : Human {
    private int clothesType;

    private Movement playerInput = Movement.WAIT;

    public Player(string name, Pos location, Village village, Gender gender, int bodyType, int hairType, int clothesType) :
        base(name, location, village, gender, bodyType, hairType) {
        this.clothesType = clothesType;
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
