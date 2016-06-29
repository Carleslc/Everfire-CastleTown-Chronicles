using UnityEngine;
using System.Collections;

public class PlayerManager : HumanManager
{
    //Here we'll store all of the directions currently being pressed. Used to determine the last direction pressed.
    private bool[] pressedDirs;
    private Movement lastPressed;
    private Camera mainCam;
    private Player player;

    void Awake() {
        pressedDirs = new bool[5] {false, false, false, false, false};        
    }

    public void Init (Player player) {
        this.player = player;
        DrawPlayer();
        base.Init(player);
        mainCam = Camera.main;
        mainCam.transform.position = new Vector3(0, 0, -1);
        mainCam.transform.SetParent(transform, false);
        IsForcedMovement = true;
        Speed = 2;

    }

    private void DrawPlayer() {
        GameObject clothes = null;
        clothes = Instantiate(PrefabLoader.GetHumanPlayerClothes(player.ClothesType), Vector2.zero,
                Quaternion.identity) as GameObject;
        clothes.transform.SetParent(transform, false);
    }

    void Update () {
        GetPlayerInput();
        if (!IsMoving)
        {
            player.PlayerInput = lastPressed;
        }
        else {
        }
        TryToMove();
    }

    private void GetPlayerInput() {
        if (DialogueManager.InDialogue)
            return;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = 10;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 2;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            SetPressed(Movement.RIGHT);
            SetUnpressed(Movement.LEFT);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            SetPressed(Movement.LEFT);
            SetUnpressed(Movement.RIGHT);            
        }
        else
        {
            SetUnpressed(Movement.RIGHT);
            SetUnpressed(Movement.LEFT);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            SetPressed(Movement.UP);
            SetUnpressed(Movement.DOWN);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            SetPressed(Movement.DOWN);
            SetUnpressed(Movement.UP);
        }
        else
        {
            SetUnpressed(Movement.UP);
            SetUnpressed(Movement.DOWN);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            lastPressed = Movement.WAIT;
    }

    private void SetPressed(Movement m) {
        if (!pressedDirs[(int)m])
        {
            pressedDirs[(int)m] = true;
            lastPressed = m;
        }
    }
    private void SetUnpressed(Movement m) {
        if (pressedDirs[(int)m])
        {
            pressedDirs[(int)m] = false;
            if (lastPressed == m)
                GetNewLastPressed();
        }
    }

    private void GetNewLastPressed() {
        for (int i = 0; i < pressedDirs.Length; i++)
        {
            if (pressedDirs[i])
                lastPressed = (Movement)i;
        }
    }
}
