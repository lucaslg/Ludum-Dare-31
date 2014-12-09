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
    AnarchyTV = 4,
    None = 10
}

public enum EAudimatState
{
    Low = 0,
    Medium = 1,
    High = 2
}

public class Channel : MonoBehaviour
{
    public int CurrentAudimat = 0;
    public int AudimatAtStart;
    public GameObject NewsTicker;
    public GameObject AudimatBar;
    public GameObject Subtitles;

    [HideInInspector]
    public string WinningSentence;

    [HideInInspector]
    public string LosingSentence;

    [HideInInspector]
    public List<EActionTag> PositiveTags;

    [HideInInspector]
    public List<EActionTag> NegativeTags;


    [HideInInspector]
    public Dictionary<EActionTag, List<string>> SpeakerComments;

    [HideInInspector]
    public Dictionary<EActionTag, List<string>> Tweets;


    protected void Start()
    {
        CurrentAudimat = AudimatAtStart;

        SpeakerComments = new Dictionary<EActionTag, List<string>>();
        Tweets = new Dictionary<EActionTag, List<string>>();
    }

    protected void Update()
    {

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