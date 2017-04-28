using UnityEngine;
using System.Collections;

public class Deathborder : MonoBehaviour
{
    private AudioSource deathAudio;
    // Use this for initialization
    void Start ()
    {
        deathAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                deathAudio.Play();
                player.Respawn();
            }
        }
    }
}
