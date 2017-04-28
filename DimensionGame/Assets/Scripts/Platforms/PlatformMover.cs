using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    [SerializeField]
    private float xMovementPoint;
    [SerializeField]
    private float yMovementPoint;
    [SerializeField]
    private float movementSpeed;

    private Vector3 startLocation;
    private Vector3 endLocation;

    private bool reachedDestination;
    private bool reachedStart;

    private const float xMovementExtra = 0.1f;
    private const float yMovementExtra = 0.1f;

    // Use this for initialization
    void Start ()
    {
        startLocation = new Vector3((gameObject.transform.position.x + xMovementExtra), (gameObject.transform.position.y + yMovementExtra));
        endLocation = new Vector3((xMovementPoint - xMovementExtra), (yMovementPoint - yMovementExtra));
        reachedStart = true;
        reachedDestination = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateDestinationAndStart();
        move();
	}

    private void move()
    {
        float tValue;
        tValue = Time.deltaTime * movementSpeed;

        if (reachedStart)
        {
            transform.position = Vector3.Lerp(transform.position, endLocation, (Time.deltaTime * movementSpeed));
        }
        if (reachedDestination)
        {
            transform.position = Vector3.Lerp(transform.position, startLocation, (Time.deltaTime * movementSpeed));
        }
    }

    private void updateDestinationAndStart()
    {
        Debug.Log("reachedStart is " + reachedStart.ToString());
        if (transform.position.x > xMovementPoint)
        {
            reachedStart = true;
            reachedDestination = false;
        }
        else
        {
            reachedStart = false;
            reachedDestination = true;
        }
    }
}
