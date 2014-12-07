using UnityEngine;
using System.Collections;

public class AlJazeera : Channel
{
    #region Singleton Implementation
    private static AlJazeera _instance = null;

    public static AlJazeera Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AlJazeera();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    #endregion
}
