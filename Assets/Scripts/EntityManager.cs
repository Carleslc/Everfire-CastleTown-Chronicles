using UnityEngine;
using System.Collections;
/// <summary>
/// Entity Manager manages a single entity and controls its representation in the map.
/// </summary>
public class EntityManager : MonoBehaviour {
    private bool isMoving = false;
    private bool isWaiting = false;
    /// <summary>
    /// Speet to be moved at.
    /// </summary>
    private float speed = 3;

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
    private Entity entity;

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    /// <summary>
    /// Necessary to call this in order to use all of the functionality of this class.
    /// </summary>
    /// <param name="entity"><c>Entity</c> to be moved.</param>
    public void Init(Entity entity) {
        this.entity = entity;
    }
    	
	void Update () {
        if (isMoving && !isWaiting)
        {
            LerpToDestination();
        }
        else if(!isWaiting){
            Move(entity.Move());
        }
	}

    private void LerpToDestination() {
        transform.position = (Vector2)transform.position + (destination * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, startingPos + destination) < speed * Time.deltaTime)
        {
            Debug.Log("Finished movement");
            isMoving = false;
            transform.position = startingPos + destination;
        }
    }

    public void Move(Movement movement) {
        if (isMoving || isWaiting)
            return;
        isMoving = true;
        startingPos = transform.position;
        switch (movement)
        {
            case Movement.WAIT:
                startingTime = Time.time;
                isWaiting = true;
                destination = Vector2.zero;
                StartCoroutine("Wait");
                break;
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
                break;              
        }
    }

    private IEnumerator Wait() {
        isMoving = false;
        while (isWaiting) {
            if (startingTime + waitTime < Time.time) {
                isWaiting = false;
            }
            yield return null;
        }
    }
}
