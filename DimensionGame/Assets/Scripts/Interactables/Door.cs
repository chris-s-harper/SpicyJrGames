using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    private AudioSource doorAudio;
    private Animator doorAnimator;
    private Key key;

	// Use this for initialization
	void Start ()
    {
        doorAudio = GetComponent<AudioSource>();
        doorAnimator = GetComponent<Animator>();
        key = GameObject.Find("Key").GetComponent<Key>();
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

            //if the player is not null
            //also checks the "PlayerHasKey()" bool, located in key to have less coupling
            if (player != null && key.PlayerHasKey())
            {
                player.ChangeIsLevelComplete(true);
                doorAnimator.SetTrigger("HasKey");
                player.ChangeCanControl(false);
                doorAudio.Play();
                loadSceneFunction();
                //Debug.Log("You win!");
            }
        }
    }

    //base code used to load the next scene/level
    void loadSceneCode()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    //function used to put a delay on the loading of the level
    void loadSceneFunction()
    {
        Invoke("loadSceneCode", 2);
    }
}
