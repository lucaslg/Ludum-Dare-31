using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour
{
    public bool DebugEnabled = false;

    public GameObject FoxNewsParentGameObject;
    public GameObject AlJazeeraParentGameObject;
    public GameObject BokoHaramParentObject;
    public GameObject AnarchyTVParentObject;

    public GameObject FoxNewsAudio;
    public GameObject AlJazeeraAudio;
    public GameObject BokoHaramAudio;
    public GameObject AnarchyTVAudio;

    public GameObject ScrambleParentGameObject;

    public GameObject MainPanel;
    public GameObject CreditsPanel;

    private float _waitTime;
    private const float MinWait = 4.0f;
    private const float MaxWait = 5.0f;

    private const float ScrambleWaitTime = 0.2f;

    public static bool IsSwitchingChannel = true;
    private int _currentChannel = 0;

	protected void Start ()
	{
        // Auto switch channel setup
        ScrambleParentGameObject.SetActive(false);
        AlJazeeraParentGameObject.SetActive(false);
        FoxNewsParentGameObject.SetActive(true);
	    CreditsPanel.SetActive(false);
	    MainPanel.SetActive(true);

        FoxNewsAudio.GetComponent<AudioSource>().Play();
        AlJazeeraAudio.GetComponent<AudioSource>().Play();
        BokoHaramAudio.GetComponent<AudioSource>().Play();
        AnarchyTVAudio.GetComponent<AudioSource>().Play();

        if(Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            GameObject.Find("UI Canvas/Main/Exit Button").SetActive(false);
        }

	    StartCoroutine(SwitchChannel());
	}

    /// <summary>
    /// Automatically switch between channels while IsSwitchingChannel is true
    /// </summary>
    /// <returns></returns>
    private IEnumerator SwitchChannel()
    {
        while (IsSwitchingChannel)
        {
            _waitTime = Random.Range(MinWait, MaxWait);

            _currentChannel++;
            if (_currentChannel > 3)
            {
                _currentChannel = 0;
            }
            switch (_currentChannel)
            {
                case 0:
                    if (DebugEnabled)
                    {
                        Debug.Log("[MENU] Switch to Fox News");
                    }
                    ScrambleParentGameObject.SetActive(true);
                    DeactivateAllChannels();

                    yield return new WaitForSeconds(ScrambleWaitTime);

                    ScrambleParentGameObject.SetActive(false);
                    FoxNewsParentGameObject.SetActive(true);
                    FoxNewsAudio.GetComponent<AudioSource>().mute = false;
                    break;
                case 1:
                    if (DebugEnabled)
                    {
                        Debug.Log("[MENU] Switch to Al Jazeera");
                    }
                    ScrambleParentGameObject.SetActive(true);
                    DeactivateAllChannels();

                    yield return new WaitForSeconds(ScrambleWaitTime);

                    ScrambleParentGameObject.SetActive(false);
                    AlJazeeraParentGameObject.SetActive(true);
                    AlJazeeraAudio.GetComponent<AudioSource>().mute = false;
                    break;
                case 2:
                    if (DebugEnabled)
                    {
                        Debug.Log("[MENU] Switch to Boko Haram");
                    }
                    ScrambleParentGameObject.SetActive(true);
                    DeactivateAllChannels();

                    yield return new WaitForSeconds(ScrambleWaitTime);

                    ScrambleParentGameObject.SetActive(false);
                    BokoHaramParentObject.SetActive(true);
                    BokoHaramAudio.GetComponent<AudioSource>().mute = false;
                    break;
                case 3:
                    if (DebugEnabled)
                    {
                        Debug.Log("[MENU] Switch to Anarchy TV");
                    }
                    ScrambleParentGameObject.SetActive(true);
                    DeactivateAllChannels();

                    yield return new WaitForSeconds(ScrambleWaitTime);

                    ScrambleParentGameObject.SetActive(false);
                    AnarchyTVParentObject.SetActive(true);
                    AnarchyTVAudio.GetComponent<AudioSource>().mute = false;
                    break;
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }

    public void StartGame()
    {
        StopAllCoroutines();
        ScrambleParentGameObject.SetActive(true);
        FoxNewsParentGameObject.SetActive(false);
        AlJazeeraParentGameObject.SetActive(false);
        BokoHaramParentObject.SetActive(false);
        AnarchyTVParentObject.SetActive(false);

        Application.LoadLevel(1);
    }

    public void DisplayCredits()
    {
        MainPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void DisplayMenu()
    {
        CreditsPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void DeactivateAllChannels()
    {
        FoxNewsParentGameObject.SetActive(false);
        AlJazeeraParentGameObject.SetActive(false);
        BokoHaramParentObject.SetActive(false);
        AnarchyTVParentObject.SetActive(false);
        MuteAllChannels();
    }

    private void MuteAllChannels()
    {
        FoxNewsAudio.GetComponent<AudioSource>().mute = true;
        AlJazeeraAudio.GetComponent<AudioSource>().mute = true;
        BokoHaramAudio.GetComponent<AudioSource>().mute = true;
        AnarchyTVAudio.GetComponent<AudioSource>().mute = true;
    }
}
