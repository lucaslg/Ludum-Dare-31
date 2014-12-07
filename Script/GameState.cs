using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    #region Singleton implementation
    private static GameState _instance = null;

    public static GameState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameState();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    #endregion

    public static EChannel CurrentChannel { get; private set; }

    /// <summary>
    /// Time before GameMode Channel Switch need to be activated
    /// </summary>
    public static float ChannelSwitchTimer;

    protected void Start()
    {
        CurrentChannel = EChannel.FoxNews;
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
        switch (CurrentChannel)
        {
            case EChannel.FoxNews:
                CurrentChannel = EChannel.AlJazeera;
                break;

            case EChannel.AlJazeera:
                CurrentChannel = EChannel.AnarchyTV;
                break;

            case EChannel.AnarchyTV:
                CurrentChannel = EChannel.BokoHaram;
                break;

            case EChannel.BokoHaram:
                CurrentChannel = EChannel.None;
                break;
        }
    }
}
