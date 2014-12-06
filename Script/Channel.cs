using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public enum EAudimatState
{
    Low = 0,
    Medium = 1,
    High = 2
}

public class Channel : MonoBehaviour
{
    public float CurrentAudimat = 0;
    public float AudimatAtStart = 50;
    public NewsTicker NewsTicker;

    [HideInInspector]
    public EAudimatState AudimatState;

    protected void Start()
    {
        CurrentAudimat = AudimatAtStart;
        AudimatState = EAudimatState.Low;
    }

    protected void Update()
    {
        if (CurrentAudimat > GameMode.AudimatLow && CurrentAudimat < GameMode.AudimatMedium)
        {
            if (AudimatState != EAudimatState.Low)
            {
                AudimatState = EAudimatState.Low;
                NewsTicker.ChangeAudimat(EAudimatState.Low);
            }
        }
        else if (CurrentAudimat > GameMode.AudimatMedium && CurrentAudimat < GameMode.AudimatHigh)
        {
            if (AudimatState != EAudimatState.Medium)
            {
                AudimatState = EAudimatState.Medium;
                NewsTicker.ChangeAudimat(EAudimatState.Medium);
            }
        }
        else if (CurrentAudimat > GameMode.AudimatHigh)
        {
            if (AudimatState != EAudimatState.High)
            {
                AudimatState = EAudimatState.High;
                NewsTicker.ChangeAudimat(EAudimatState.High);
            }
        }

    }
}
