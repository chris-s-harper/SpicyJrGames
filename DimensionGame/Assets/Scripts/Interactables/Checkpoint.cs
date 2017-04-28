using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private List<Checkpoint> otherCheckpoints = new List<Checkpoint>();
    [SerializeField]
    private Transform checkpointRespawnTransform;

    private bool isCheckpointActivated;
    private SpriteRenderer checkpointSprite;
    private AudioSource checkpointAudio;
    private Animator checkpointAnimator;
    private Player player;

    private const float checkPointYOffset = 1;

	// Use this for initialization
	void Start ()
    {
        isCheckpointActivated = false;
        checkpointAudio = GetComponent<AudioSource>();
        checkpointSprite = GetComponent<SpriteRenderer>();
        checkpointAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();

            if (!isCheckpointActivated)
            {
                isCheckpointActivated = true;
                checkpointAudio.Play();
                checkpointAnimator.SetBool("isActive", true);

                if (player !=null)
                {
                    ChangeRespawnToSet(player);

                    foreach (Checkpoint checkpoint in otherCheckpoints)
                    {
                        checkpoint.deactivateCheckpoint();
                    }
                }
            }
        }
    }


    void deactivateCheckpoint()
    {
        isCheckpointActivated = false;
        checkpointAnimator.SetBool("isActive", false);
    }

    //TODO: have the update respawn location change if a block moves
    public void ChangeRespawnToSet(Player p)
    {

        Vector2 respawnToSet = new Vector2(checkpointRespawnTransform.position.x,checkpointRespawnTransform.position.y);
        
        p.RespawnTransform = respawnToSet;
    }
}
