using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Lever : MonoBehaviour
{
    [SerializeField]
    private MoveableBlock[] moveableBlocks;

    private AudioSource leverSound;
    private Animator leverAnimator;

    private bool leverIsPulled;
	// Use this for initialization
	void Start ()
    {
        leverSound = gameObject.GetComponent<AudioSource>();
        leverAnimator = gameObject.GetComponent<Animator>();
        leverIsPulled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void moveTheBlocks()
    {
        foreach (MoveableBlock block in moveableBlocks)
        {
            block.moveTheBlock();
        }
        leverIsPulled = !leverIsPulled;
        leverAnimator.SetBool("leverIsPulled", leverIsPulled);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player != null && player.gameObject.tag == "Player")
        {
            leverSound.Play();
            moveTheBlocks();
        }
    }
}
