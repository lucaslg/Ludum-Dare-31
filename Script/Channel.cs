using System;
using System.Collections.Generic;
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
    public GameObject NewsTicker;
    public GameObject audimatBar;

    public ActionTag[] PositiveTags;
    public ActionTag[] NegativeTags;

    private List<int> _positiveAudimatObjects;
    private List<int> _negativeAudimatObjects;

    [HideInInspector]
    public EAudimatState AudimatState;

    protected void Start()
    {
        CurrentAudimat = AudimatAtStart;
        AudimatState = EAudimatState.Low;
        _positiveAudimatObjects = new List<int>();
        _negativeAudimatObjects = new List<int>();
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

    public void AddActionToChannel(InterestZone obj)
    {
        int positiveMatch = 0;
        int negativeMatch = 0;

        foreach (ActionTag actionTag in obj.tags)
        {
            int i = 0;
            while (i < obj.tags.Length)
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