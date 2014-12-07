using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum EChannel
{
    All = 0,
    FoxNews = 1,
    AlJazeera = 2,
    BokoHaram = 3,
    AnarchyTV = 4
}

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
    public GameObject NewsTicker;
    public GameObject audimatBar;

    public EActionTag[] PositiveTags;
    public EActionTag[] NegativeTags;

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
                NewsTicker.GetComponent<NewsTicker>().ChangeAudimat(EAudimatState.Low);

            }
        }
        else if (CurrentAudimat > GameMode.AudimatMedium && CurrentAudimat < GameMode.AudimatHigh)
        {
            if (AudimatState != EAudimatState.Medium)
            {
                AudimatState = EAudimatState.Medium;
                NewsTicker.GetComponent<NewsTicker>().ChangeAudimat(EAudimatState.Medium);
            }
        }
        else if (CurrentAudimat > GameMode.AudimatHigh)
        {
            if (AudimatState != EAudimatState.High)
            {
                AudimatState = EAudimatState.High;
                NewsTicker.GetComponent<NewsTicker>().ChangeAudimat(EAudimatState.High);
            }
        }
    }

    public virtual void AddActionToChannel(InterestZone obj)
    {
        int positiveMatch = 0;
        int negativeMatch = 0;

        foreach (EActionTag actionTag in obj.Tags)
        {
            int i = 0;
            while (i < obj.Tags.Length)
            {
                if (PositiveTags[i] == actionTag)
                {
                    positiveMatch++;
                }
                if (NegativeTags[i] == actionTag)
                {
                    negativeMatch++;
                }
                i++;
            }
        }

        IncreaseAudimat(positiveMatch - negativeMatch);
    }

    private void IncreaseAudimat(int match)
    {
        CurrentAudimat += match;
    }
}