using UnityEngine;
using System.Collections;
using System;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private string pauseButton;
    [SerializeField]
    private GameObject pauseMenu;

    Player player;
    private bool isPaused = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        pauseMenu.SetActive(isPaused);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown(pauseButton))
        {
            OpenAndClosePauseMenu();
        }
    }

    public void OpenAndClosePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
