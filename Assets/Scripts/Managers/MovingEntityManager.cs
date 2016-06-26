using UnityEngine;
using System.Collections;
/// <summary>
/// Entity Manager manages a single entity and controls its representation in the map.
/// </summary>
public class MovingEntityManager : EntityManager {
    private bool isMoving = false;
    private bool isWaiting = false;

    private bool updateAnimations = false;
    /// <summary>
    /// Speet to be moved at.
    /// </summary>
    private float speed = 1;

    private Vector2 startingPos;
    /// <summary>
    /// Time to wait when the Movement WAIT is executed.
    /// </summary>
    private float waitTime = 1;
    /// <summary>
    /// Used to calculate te wait time.
    /// </summary>
    private float startingTime;
    /// <summary>
    /// Position to arrive at. Once achieved, it remains the same.
    /// </summary>
    private Vector2 destination;
    //private Movement movementDir;
    private MovingEntity entity;

    private bool isForcedMovement = false;

    int newMoveX = 0;
    int newMoveY = 0;

    private AutoAnimator[] autoAnims;

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }

    public bool IsForcedMovement
    {
        set
        {
            isForcedMovement = value;
        }
    }

    public MovingEntity Entity
    {
        get
        {
            return entity;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    /// <summary>
    /// Necessary to call this in order to use all of the functionality of this class.
    /// </summary>
    /// <param name="movingEntity"><c>Entity</c> to be moved.</param>
    public void Init(MovingEntity movingEntity) {
        this.entity = movingEntity;
        autoAnims = GetComponentsInChildren<AutoAnimator>();
    }

    //Basically, the EntityManager keeps retrieving the moves of the entity it is managing. When it's moving, it doesn't get more
    //movement commands, as it is currently finishing the movement and it would lead to inconsistencies with the logical map.
    void Update () {
        if (updateAnimations) {
            autoAnims = GetComponentsInChildren<AutoAnimator>();
            updateAnimations = false;
        }
        TryToMove();
    }

    protected void TryToMove() {
        if (!isWaiting || isForcedMovement)
        {
            if (isMoving)
                LerpToDestination();
            else
            {
                Movement next = entity.NextMovement();
                Move(entity.Move() ? next : Movement.WAIT);
                setAnimatorState(next);
            }
        }
    }

    private void LerpToDestination() {
        transform.position = (Vector2)transform.position + (destination * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, startingPos + destination) < speed * Time.deltaTime)
        {
            isMoving = false;
            transform.position = startingPos + destination;
        }
    }

    private void Move(Movement movement) {
        if (isMoving || (isWaiting && !isForcedMovement))
            return;
        isMoving = true;
        startingPos = transform.position;
        switch (movement)
        {
            case Movement.UP:
                destination = Vector2.up;
                break;
            case Movement.DOWN:
                destination = Vector2.down;
                break;
            case Movement.RIGHT:
                destination = Vector2.right;
                break;
            case Movement.LEFT:
                destination = Vector2.left;
                break;
            default:
                isMoving = false;
                if (!isWaiting) {
                    isWaiting = true;
                    startingTime = Time.time;
                    destination = Vector2.zero;
                    StartCoroutine("Wait");
                }
                break;
        }
    }

    private IEnumerator Wait() {
        while (isWaiting) {
            if (startingTime + waitTime < Time.time) {
                isWaiting = false;
            }
            yield return null;
        }
    }

    private void setAnimatorState(Movement m) {
        foreach (AutoAnimator a in autoAnims) {
            a.Movement = m;
            a.IsMoving = IsMoving;
        }
    }

    protected void UpdateAnimators() {
        updateAnimations = true;
    }}
