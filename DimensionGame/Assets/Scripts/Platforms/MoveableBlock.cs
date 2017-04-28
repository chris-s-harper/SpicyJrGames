using UnityEngine;
using System.Collections;


public class MoveableBlock : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveToLocation;

    private Vector3 startLocation;
    private bool isAtStart;

	// Use this for initialization
	void Start ()
    {
        isAtStart = true;
        startLocation = gameObject.transform.position;
	}

    public void moveTheBlock()
    {
        if (isAtStart)
        {
            gameObject.transform.position = moveToLocation;
            isAtStart = false;
        }
        else
        {
            gameObject.transform.position = startLocation;
            isAtStart = true;
        }
    }
}
