using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    public static EChannel CurrentChannel { get; private set; }

    public static FoxNews FoxNewsInstance { get; private set; }
    public static AlJazeera AlJazeeraInstance { get; private set; }
    public static AnarchyTV AnarchyTVInstance { get; private set; }
    public static BokoHaram BokoHaramInstance { get; private set; }

    private static GameObject _foxNewsParentGameObject;
    private static GameObject _alJazeeraParentGameObject;
    private static GameObject _anarchyTvParentGameObject;
    private static GameObject _bokoHaramParentGameObject;
    private static GameObject _fergusonParentGameObject;
    private static GameObject _audimatBarParentGameObject;

	public InterestZone police;
	public InterestZone robbery;

    public static GameObject FergusonCameraInstance { get; private set; }

    private static GameObject _endScreenParentGameObject;
    public static EndScreen EndScreenInstance { get; private set; }

    [HideInInspector]
    public static List<InterestZone> InterestZoneList = new List<InterestZone>();


    public static bool Zap = false;
	private static bool policeIsPlayed = false;
	private static bool robberyIsPlayed = false;
	private static bool resetAnim = false;

    /// <summary>
    /// Time before GameMode Channel Switch need to be activated
    /// </summary>
    public static float ChannelSwitchTimer;

    public static bool TimerInitialized;

    protected void Start()
    {
        _foxNewsParentGameObject = GameObject.Find("Fox News");
        _alJazeeraParentGameObject = GameObject.Find("Al Jazeera");
        _anarchyTvParentGameObject = GameObject.Find("Anarchy TV");
        _bokoHaramParentGameObject = GameObject.Find("Boko Haram");
        _fergusonParentGameObject = GameObject.Find("Ferguson");
        _audimatBarParentGameObject = GameObject.Find("Audimat Bar Canvas");

        FoxNewsInstance = _foxNewsParentGameObject.transform.FindChild("Fox News Logic").GetComponent<FoxNews>();
        AlJazeeraInstance = _alJazeeraParentGameObject.transform.FindChild("Al Jazeera Logic").GetComponent<AlJazeera>();
        AnarchyTVInstance = _anarchyTvParentGameObject.transform.FindChild("Anarchy TV Logic").GetComponent<AnarchyTV>();
        BokoHaramInstance = _bokoHaramParentGameObject.transform.FindChild("Boko Haram Logic").GetComponent<BokoHaram>();

        FergusonCameraInstance = GameObject.Find("Ferguson/Camera");

        _endScreenParentGameObject = GameObject.Find("EndScreen");
        EndScreenInstance = GameObject.Find("EndScreen/EndScreen Logic").GetComponent<EndScreen>();

        _endScreenParentGameObject.gameObject.SetActive(false);
        _foxNewsParentGameObject.gameObject.SetActive(false);
        _alJazeeraParentGameObject.gameObject.SetActive(false);
        _anarchyTvParentGameObject.gameObject.SetActive(false);
        _bokoHaramParentGameObject.gameObject.SetActive(false);

        _fergusonParentGameObject.SetActive(true);

        CurrentChannel = EChannel.FoxNews;
        _foxNewsParentGameObject.gameObject.SetActive(true);

		policeIsPlayed = false;
		robberyIsPlayed = false;

		//police = GameObject.Find ("PoliceForce");
		//robbery = GameObject.Find ("Roberry");
    }

    protected void Update()
	{
		//Debug.Log (ChannelSwitchTimer);

		if (ChannelSwitchTimer < 45 && ChannelSwitchTimer > 44 && (!policeIsPlayed))
		{
			Debug.Log ("Déclenchement police");
			policeIsPlayed = true;
			police.GetComponent<BoxCollider>().enabled = true;
			police.GetComponent<Animator>().SetBool("active",true);
		}

		if (ChannelSwitchTimer < 30 && ChannelSwitchTimer > 29 && (!robberyIsPlayed))
		{
			Debug.Log ("Déclenchement roberry");
			robberyIsPlayed = true;
			robbery.GetComponent<BoxCollider>().enabled = true;
			robbery.GetComponent<Animator>().SetBool("active",true);
		}

		// End of the scene
		if (ChannelSwitchTimer < 0.5 && !resetAnim)
		{
			resetAnim = true;

			policeIsPlayed = false;
			police.GetComponent<BoxCollider>().enabled = false;
			police.GetComponent<Animator>().SetBool("active",false);
			
			robberyIsPlayed = false;
			robbery.GetComponent<BoxCollider>().enabled = false;
			robbery.GetComponent<Animator>().SetBool("active",false);
			
			robbery.HasBeenSeen = false;
			police.HasBeenSeen = false;
		}
    }

    /// <summary>
    /// Returns the current Channel instance
    /// </summary>
    /// <returns>Current Channel Instance, null if channel is not set</returns>
    public static Channel GetCurrentChannelInstance()
    {
        switch (CurrentChannel)
        {
            case EChannel.AlJazeera:
                return GameState.AlJazeeraInstance;

            case EChannel.AnarchyTV:
                return GameState.AnarchyTVInstance;

            case EChannel.BokoHaram:
                return GameState.BokoHaramInstance;

            case EChannel.FoxNews:
                return GameState.FoxNewsInstance;

            default:
                return null;
        }
    }

    /// <summary>
    /// Zap to the next channel :
    ///     1) Fox News
    ///     2) Al Jazeera
    ///     3) Anarchy TV
    ///     4) Boko Haram
    /// </summary>
    public static void ZapToNextChannel()
    {
		// Réinitialisation de la scene
		resetAnim = true;

        if (!TimerInitialized)
        {
            return;
        }
        switch (CurrentChannel)
        {
            case EChannel.FoxNews:
                CurrentChannel = EChannel.AlJazeera;
                _foxNewsParentGameObject.SetActive(false);
                _alJazeeraParentGameObject.SetActive(true);
                break;

            case EChannel.AlJazeera:
                CurrentChannel = EChannel.AnarchyTV;
                _alJazeeraParentGameObject.SetActive(false);
                _anarchyTvParentGameObject.SetActive(true);
                break;

            case EChannel.AnarchyTV:
                CurrentChannel = EChannel.BokoHaram;
                _anarchyTvParentGameObject.SetActive(false);
                _bokoHaramParentGameObject.SetActive(true);
                break;

            case EChannel.BokoHaram:
                CurrentChannel = EChannel.None;
                _bokoHaramParentGameObject.SetActive(false);

                _audimatBarParentGameObject.SetActive(false);
                _fergusonParentGameObject.SetActive(false);
                _endScreenParentGameObject.SetActive(true);
                EndScreenInstance.Win();
                break;
        }
        ResetInterestZones();
        FergusonCameraInstance.GetComponent<CameraController>().ResetZoom();
    }



    /// <summary>
    /// Reset every InterestZone.HasBeenSeen to false
    /// </summary>
    public static void ResetInterestZones()
    {		
        foreach (InterestZone interestZone in GameState.InterestZoneList)
        {
            interestZone.HasBeenSeen = false;
        }
    }
}
