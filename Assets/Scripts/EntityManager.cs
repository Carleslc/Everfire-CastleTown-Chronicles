using UnityEngine;
using System.Collections;

public class EntityManager : MonoBehaviour {
    private bool isMoving = false;
    private float speed = 3;
    private float waitTime = 1;
    private float startingTime;
    private Vector2 destination;
    private Vector2 startingPos;
    private Movement movementDir;

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

    void Start () {
	}
	
	void Update () {
        if (isMoving && movementDir != Movement.WAIT)
        {
            LerpToDestination();
        }
	}

    private void LerpToDestination() {
        transform.position = (Vector2)transform.position + (destination * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) < speed * Time.deltaTime)
        {
            isMoving = false;
            transform.position = destination;
        }
    }

    public void Move(Movement movement) {
        if (isMoving)
            return;
        movementDir = movement;
        isMoving = true;
        startingPos = transform.position;
        switch (movement)
        {
            case Movement.WAIT:
                startingTime = Time.time;
                StartCoroutine("Wait");
                destination = Vector2.zero;
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
        while (isMoving) {
            if (startingTime + waitTime < Time.time)
                isMoving = false;
            yield return null;
        }
    }
}
