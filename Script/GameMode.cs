using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    #region Singleton Implementation
    private static GameMode _instance = null;

    public static GameMode Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameMode();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    #endregion

    #region GAME CONSTS
    
    public const float AudimatLow       = 0;
    public const float AudimatMedium    = 100;
    public const float AudimatHigh      = 300;

    public const float AudimatIncreaseValue = 1.0f;
    public const float AudimatIncreaseDelay = 0.5f;

    #endregion

    protected void Start()
    {
        
    }

    protected void Update()
    {
        if (GameState.ChannelSwitchTimer <= 0 && 
            GameState.CurrentChannel != EChannel.None)
        {
            GameState.ZapToNextChannel();
        }
    }

    /// <summary>
    /// Set the gamestate timer to a given duration and start a coroutine to decrement it
    /// </summary>
    /// <param name="duration">duration of Gamestate timer</param>
    public void SetupGameStateTimer(float duration)
    {
        GameState.ChannelSwitchTimer = duration;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (GameState.ChannelSwitchTimer > 0)
        {
            GameState.ChannelSwitchTimer -= Time.deltaTime;
            if (GameState.ChannelSwitchTimer < 0)
            {
                GameState.ChannelSwitchTimer = 0;
            }
        }
        yield return 0;
    }
}
