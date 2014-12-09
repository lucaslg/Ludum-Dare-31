using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    public GameObject FergusonCamera;

    #region GAME CONSTS

    public const float AudimatIncreaseValue = 1.0f;

    public const float TimeBeforeChannelZapping = 50.0f;

    public const float ActionFreezeDuration = 10.0f;

    #endregion

    protected void Start()
    {
        SetupGameStateTimer(TimeBeforeChannelZapping);
    }
    
    protected void Update()
    {
        if (GameState.Zap &&
            GameState.ChannelSwitchTimer <= 0 && 
            GameState.CurrentChannel != EChannel.None &&
            !FergusonCamera.GetComponent<CameraController>().IsLocked)
        {
            GameState.Zap = false;
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
            GameState.ChannelSwitchTimer -= Time.deltaTime;
            yield return 0;
        }
        
        GameState.Zap = true;
        GameState.ChannelSwitchTimer = 0;
        yield return 0;
    }
}