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

    public static Channel CurrentChannel;

    void Start()
    {
        
    }

    public static void ChangeChannel(Channel chan)
    {
        CurrentChannel = chan;
    }
}
