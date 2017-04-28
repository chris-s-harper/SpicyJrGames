using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PinkBlock : MonoBehaviour
{
    private AudioSource deathAudio;
    // Use this for initialization
    void Start()
    {
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null && player.CheckColor() == "green")
            {
                deathAudio.Play();
                player.Respawn();
                player.ChangeToPink();
            }
        }
    }
}
