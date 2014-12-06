using UnityEngine;
using System.Collections;

public class FoxNews : Channel 
{
    #region Singleton Implementation
    private static FoxNews _instance = null;

    public static FoxNews Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FoxNews();
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
