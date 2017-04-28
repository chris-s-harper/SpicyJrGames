using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//CUTSCENE MANAGER GOES ON CUTSCENE PANEL
[RequireComponent(typeof(AudioSource))]
public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private string cutsceneString;
    [SerializeField]
    private Text cutsceneText;
    [SerializeField]
    private float textDefaultWaitTime;
    [SerializeField]
    private float spedUpTextWaitTime;
    [SerializeField]
    private float cutsceneFadeOutTime;
    [SerializeField]
    private char spaceChar;
    [SerializeField]
    private Player player;

    private char[] cutsceneTextChars;
    private WaitForSeconds textAnimationInterval;
    private WaitForSeconds cutsceneFadeOutInterval;
    private Image canvasImage;
    private AudioSource charBlipAudio;
    private bool canContinue = false;

    void Awake()
    {
        textAnimationInterval = new WaitForSeconds(textDefaultWaitTime);
        cutsceneFadeOutInterval = new WaitForSeconds(cutsceneFadeOutTime);
    }

    // Use this for initialization
    void Start ()
    {
        charBlipAudio = GetComponent<AudioSource>();
        canvasImage = GetComponent<Image>();
        player.ChangeCanControl(false);
        DisplayText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpeedThroughAndDisableCutscene();
	}

    private void DisplayText()
    {
        cutsceneTextChars = cutsceneString.ToCharArray();
        StartCoroutine(AnimateText(cutsceneTextChars));
    }

    private IEnumerator AnimateText(char[] charArray)
    {
        foreach (char letter in charArray)
        {
            cutsceneText.text += letter;
            if (letter != spaceChar)
            {
                charBlipAudio.pitch = Random.Range(1, 3);
                charBlipAudio.Play();
            }
            yield return textAnimationInterval;
        }
        canContinue = true;
    }

    private IEnumerator DisableCutscene()
    {
        canvasImage.CrossFadeAlpha(0, cutsceneFadeOutTime, false);
        cutsceneText.CrossFadeAlpha(0, cutsceneFadeOutTime, false);
        yield return cutsceneFadeOutInterval;
        gameObject.SetActive(false);
        player.ChangeCanControl(true);
    }

    private void SpeedThroughAndDisableCutscene()
    {
        Debug.Log("Testing");
        if (Input.GetButtonDown("Jump") && canContinue)
        {
            StartCoroutine(DisableCutscene());
        }

        if (Input.GetButtonDown("Jump") && !canContinue)
        {
            textAnimationInterval = new WaitForSeconds(spedUpTextWaitTime);
        }
    }
}
