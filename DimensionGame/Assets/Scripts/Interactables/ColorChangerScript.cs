using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ColorChangerScript : MonoBehaviour
{
    AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            string playerColor;

            if (player != null)
            {
                playerColor = player.CheckColor();

                if (playerColor == "green")
                {
                    audioSource.Play();
                    player.ChangeToPink();
                    Debug.Log("Color changed to" + player.CheckColor().ToString());
                }

                if (playerColor == "pink")
                {
                    audioSource.Play();
                    player.ChangeToGreen();
                    Debug.Log("Color changed to" + player.CheckColor().ToString());
                }
            }
        }
    }
}
