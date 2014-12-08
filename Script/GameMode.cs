using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    public GameObject FergusonCamera;

    #region GAME CONSTS
    
    public const float AudimatLow       = 0;
    public const float AudimatMedium    = 100;
    public const float AudimatHigh      = 300;

    public const float AudimatIncreaseValue = 1.0f;

    public const float TimeBeforeChannelZapping = 6000.0f;

    public const float SubtitlesDuration = 10.0f;

    #endregion

    protected void Start()
    {
        SetupGameStateTimer(TimeBeforeChannelZapping);
    }
    
    protected void Update()
    {
        if (GameState.ChannelSwitchTimer <= 0 && 
            GameState.CurrentChannel != EChannel.None &&
            !FergusonCamera.GetComponent<CameraController>().IsLocked)
        {
            GameState.ZapToNextChannel();
            SetupGameStateTimer(TimeBeforeChannelZapping);
        }
    }

    /// <summary>
    /// Set the gamestate timer to a given duration and start a coroutine to decrement it
    /// </summary>
    /// <param name="duration">duration of Gamestate timer</param>
    public void SetupGameStateTimer(float duration)
    {
        GameState.ChannelSwitchTimer = duration;
        GameState.TimerInitialized = true;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (GameState.ChannelSwitchTimer > 0)
        {
            //Debug.Log(GameState.ChannelSwitchTimer);
            GameState.ChannelSwitchTimer -= Time.deltaTime;
            if (GameState.ChannelSwitchTimer < 0)
            {
                GameState.ChannelSwitchTimer = 0;
            }
            yield return 0;
        }
        yield return 0;
    }
}
