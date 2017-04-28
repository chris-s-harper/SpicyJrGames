using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Key : MonoBehaviour
{
    [SerializeField]
    private Text keyText;

    private bool hasBeenPickedUp;
    AudioSource keyPickupSound;
    SpriteRenderer keySprite;

    // Use this for initialization
    void Start()
    {
        keyPickupSound = GetComponent<AudioSource>();
        keySprite = GetComponent<SpriteRenderer>();
        hasBeenPickedUp = false;
        //keyText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !hasBeenPickedUp)
        {
            /*segment of code creates illusion to player that Key has been picked up
            however gameObject is still active in order to reference it*/
            hasBeenPickedUp = true;
            keySprite.enabled = false;
            keyPickupSound.Play();
            //keyText.gameObject.SetActive(true);
        }
    }

    //used for the door to check if the player "has the key"
    public bool PlayerHasKey()
    {
        if (hasBeenPickedUp == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
