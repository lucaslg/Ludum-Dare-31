using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour
{
    public bool DebugEnabled = false;

    public GameObject FoxNewsParentGameObject;
    public GameObject AlJazeeraParentGameObject;
    public GameObject BokoHaramParentObject;
    public GameObject AnarchyTVParentObject;
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
    }
}
