using System.Collections.Generic;
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

    protected new void Start()
    {
        base.Start();

        /* ******************************************************************************************
         *                           Speaker's comments initialization                              *
         * **************************************************************************************** */
        #region Speaker's comments

        // Chaos comments
        SpeakerComments.Add(EActionTag.Chaos, new List<string>()
        {
            "We've been lead to this state of near civil war by the pure and simple incompetence of our socialist government. Vote with your american hearts next time.",
            "The country, OUR country, the one where our fathers and the fathers of our fathers were born, is burning ! Grab your constitutional full automatic legal guns and restore order in our home !"
        });

        // Crime comments
        SpeakerComments.Add(EActionTag.Crime, new List<string>()
        {
            "This is who the real rioters in Ferguson are ! Relentlessy violent, idiotic creatures who do not care for the safety or properties of those who live with and among them !",
            "Who can honestly blame Darren Wilson for what he did ? He prevented one of these criminals to injure our society once more. The jury and the law decided he was right, as did God."
        });

        // Misery comments
        SpeakerComments.Add(EActionTag.Misery, new List<string>()
        {
            "Here you can observe a bunch of ineducated people in their daily lives.",
            "The apple never falls far from the tree !"
        });

        // Order comments
        SpeakerComments.Add(EActionTag.Order, new List<string>()
        {
            "One can only admire what all these brave men are achieving for the love of their country.",
            "The determination of our policemen will allow thousands of true americans to reclaim their rightful land."
        });

        // Peace comments
        SpeakerComments.Add(EActionTag.Peace, new List<string>()
        {
            "If they want us to believe they are NOT an organized group of disorganization, they could as well hide their accents.",
            "You CANNOT listen to gangsta rap music and pretend you do not want any form of violence in your life. Police officers are only giving them what they ask for !"
        });

        // Police Violence comments
        SpeakerComments.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "Desperate times call for desperate mesures.",
            "Look what those poor police officers are forced to do to insure our fellow contrymen and women's safety."
        });
        #endregion Speaker's comments

        /* ******************************************************************************************
         *                                  Tweets initialization                                   *
         * **************************************************************************************** */
        #region Tweets

        // Chaos Tweets
        Tweets.Add(EActionTag.Chaos, new List<string>()
        {
            "lets give to the N-words an H-bomb #AmericaBeforeMe",
            "just let the city burn we don't need them in the USA",
            "where are all the #Ferguson firefighters ? KFC???"
        });

        // Crime Tweets
        Tweets.Add(EActionTag.Crime, new List<string>()
        {
            "Get Shot Or Die Trying lmao",
            "black people riot at night cause they got a natural camo suit LOL don't freak out i have a black friend (my gardener)",
            "put them in jail and then burn the jail and then bury the ashes in Africa"
        });

        // Misery Tweets
        Tweets.Add(EActionTag.Misery, new List<string>()
        {
            "what happens when you spend all your money on fried chicken ? #niggerniggernigger",
            "@FoxNews if only they didn't quit school at 8yo LOL #losers",
            "black ppl are poor ppl because their not whit ppl"
        });

        // Order Tweets
        Tweets.Add(EActionTag.Order, new List<string>()
        {
            "the police force should drop tear gas from B52 planes",
            "why not use lethal force anyway ?? Their either criminals or rappers or future criminals #PureAmerica",
            "im in a plane to #Ferguson right now!! Ready to kick niggas ass for #MyBelovedUSA"
        });

        // Peace Tweets
        Tweets.Add(EActionTag.Peace, new List<string>()
        {
            "#SuperiorWhiteRace #MonkeyRiot #ItsRainingChicken #NoRacist",
            "wtf fox news!! Stop spreading your good nigger propaganda #supportDarenWilson",
            "We were at peace when they were picking up cotton #RestoreSlavery"
        });

        // Police Violence Tweets
        Tweets.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "they only do their jobs #supportDarrenWilson",
            "who lives by the sword shall die by the sword",
            "Darren Wilson did what every good citizen should do !!! #SarahPalin"
        });
        #endregion Tweets
        /* ******************************************************************************************
         *                      End of Dirty Hard coded dialogs initialization                      *
         * **************************************************************************************** */
    }

    public override void AddActionToChannel(InterestZone obj)
    {
        if (obj.ChannelTarget == EChannel.All || obj.ChannelTarget == EChannel.FoxNews)
        {
            base.AddActionToChannel(obj);
        }
    }
}
