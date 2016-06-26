using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoAnimator : MonoBehaviour
{
    [SerializeField]
    private string prefabName;
    private string spritePath;

    private SpriteRenderer spriteRenderer;

    public Sprite[] animArray;

    private Movement previousMovement = Movement.WAIT;
    private Movement currentMovement;

    private bool isMoving = true;

    float delay = .15f;
    private int startingFrame;
    private int currentFrame;
    private bool isInExtraFrame = false;
    private float startingTime;

    public Movement Movement
    {
        set
        {
            previousMovement = currentMovement;
            currentMovement = value;
            ResetAnimationStart();
        }
    }

    public int CurrentFrame
    {
        get
        {
            return currentFrame;
        }

        set
        {
            currentFrame = value;
            if (currentFrame > startingFrame + 2) {
                if (!isInExtraFrame)
                {
                    currentFrame = startingFrame + 1;
                    isInExtraFrame = true;
                }                
            }
            else if (isInExtraFrame) {
                currentFrame = startingFrame;
                isInExtraFrame = false;
            }
        }
    }

    public bool IsMoving
    {
        set
        {
            isMoving = value;
        }
    }

    void Start()
    {
        spritePath = "AnimSprites/" + prefabName;
        Debug.Log("Loading " + spritePath);
        animArray = Resources.LoadAll<Sprite>(spritePath);
        Movement = Movement.RIGHT;
        startingTime = Time.time;
        StartCoroutine("Animate");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void ResetAnimationStart() {
        if (previousMovement != currentMovement)
        {
            startingTime = -1;
            switch (currentMovement)
            {
                case Movement.UP:
                    startingFrame = (int)AnimStart.UP;
                    break;
                case Movement.DOWN:
                    startingFrame = (int)AnimStart.DOWN;
                    break;
                case Movement.RIGHT:
                    startingFrame = (int)AnimStart.RIGHT;
                    break;
                case Movement.LEFT:
                    startingFrame = (int)AnimStart.LEFT;
                    break;
                default:
                    break;
            }
            currentFrame = startingFrame;
        }
    }

    IEnumerator Animate()
    {
        while (true)
        {
            if (isMoving)
            {
                if (startingTime + delay < Time.time)
                {
                    startingTime = Time.time;
                    CurrentFrame = currentFrame + 1;
                    spriteRenderer.sprite = animArray[CurrentFrame];
                }

            }
            else
            {
                spriteRenderer.sprite = animArray[startingFrame + 1];
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private enum AnimStart {
        DOWN = 0,
        LEFT = 3,
        RIGHT = 6,
        UP = 9,        
    }
}