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
    
    #endregion
}
