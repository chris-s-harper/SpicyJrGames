using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//so the audio plays whenever a blip is selected
[RequireComponent(typeof(AudioSource))]
public class MainMenuManager : MonoBehaviour
{
    //audio blip used whenever a button is pressed
    private AudioSource select;
    private Transform mainMenuPanel;
    private Transform creditsPanel;
    private Transform howToPlayPanel;

	// Use this for initialization
	void Start ()
    {
        select = GetComponent<AudioSource>();

        mainMenuPanel = GameObject.Find("TitlePanel").GetComponent<Transform>();
        creditsPanel = GameObject.Find("CreditsPanel").GetComponent<Transform>();
        howToPlayPanel = GameObject.Find("HowToPlayPanel").GetComponent<Transform>();

        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void openCredits()
    {
        select.Play();
        mainMenuPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }

    public void openHowToPlay()
    {
        select.Play();
        mainMenuPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
    }

    public void returnToTitle()
    {
        select.Play();
        howToPlayPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }

    public void loadScene(string sceneToLoad)
    {
        select.Play();
        SceneManager.LoadScene(sceneToLoad);
    }

}
