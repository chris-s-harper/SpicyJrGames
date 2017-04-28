using UnityEngine;
using System.Collections;

public class PinkBlockPhasable : MonoBehaviour
{
    BoxCollider2D boxCollider2d;
    void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
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
                gameObject.layer = 0;
                boxCollider2d.isTrigger = true;
                //player.ChangeToGreen();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null && player.CheckColor() == "pink")
            {
                gameObject.layer = 8;
                boxCollider2d.isTrigger = false;
            }
        }
    }
}
