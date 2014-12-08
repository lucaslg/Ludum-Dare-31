using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BokoHaram : Channel 
{
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
            "It happens that Allah's slave do something in which he sees no harm, but that will make him fall in hell for seventies autumns.",
            "They're is Allah and only Allah. They bring the seed of chaos which is the mark of the Sheitan. They call for their anhilation!"
        });

        // Crime comments
        SpeakerComments.Add(EActionTag.Crime, new List<string>()
        {
            "The misbelievers respects no law. Death is the only response.",
            "These mens should fear God, but white capitalism eated the soul of these brothers!",
            "Their state can't even protect their people against the thieves! Their hands should be cutted right now!"
        });

        // Misery comments
        SpeakerComments.Add(EActionTag.Misery, new List<string>()
        {
            "Allah gave us oil wells and the infidels live in misery ! They shall hear the true meaning of God !",
            "Do not pity those who do not cherish God and his glory !"
        });

        // Order comments
        SpeakerComments.Add(EActionTag.Order, new List<string>()
        {
            "It's only dogs fighting dogs, brothers.",
            "Look, look at the sionist american order my borthers!"
        });

        // Peace comments
        SpeakerComments.Add(EActionTag.Peace, new List<string>()
        {
            "These christians are filled with lies! They hide their true nature behind many faces, but they are snakes!",
            "Don't believe what you see, they are aaaaall sodomites. Hamdulila."
        });

        // Police Violence comments
        SpeakerComments.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "The infidels grips to their dying world, corrupted by sex and music, but their world is dying brothers!",
            "These dogs would have been beheaded!"
        });
        #endregion Speaker's comments

        /* ******************************************************************************************
         *                                  Tweets initialization                                   *
         * **************************************************************************************** */
        #region Tweets
        // Chaos Tweets
        Tweets.Add(EActionTag.Chaos, new List<string>()
        {

            "hide your wifes, your daughters and the girls who are both #muslimLife",
            "Allah's wrath has falled on the sheitan land! #GODUNDCHAINED #Lamborghini"
        });

        // Crime Tweets
        Tweets.Add(EActionTag.Crime, new List<string>()
        {
            "ouais mon pote, hang all dirty jews and christians, tahu les frères! #THUGLIFESYRIA #M60PICKUP",
            "Black americans muslim are sodomites barbarians and deserve nothing but slavery. Salem. #Arabianempire"
        });

        // Misery Tweets
        Tweets.Add(EActionTag.Misery, new List<string>()
        {
            "La ilaha illa Allah, ha la ili, hay yo",
            "Hili b'Allah, hey, hili bay yo they're not getting arab money",
            "if only they had oil wells… but we'll invade them either way #HOWIRONIC"
        });

        // Order Tweets
        Tweets.Add(EActionTag.Order, new List<string>()
        {
            "Stoning to death prove to be the most efficient way to impress people  #silexfrvr",
            "Salamaleikoum, im a good looking virgin in search for a true soldier of god. Call me. #ConvertedWhiteSlut"
        });

        // Peace Tweets
        Tweets.Add(EActionTag.Peace, new List<string>()
        {
            "American mens had their dick and ball cut off by all thes feminist gay shit! #AmericanAreGay",
            "i think i see a killer CIA drone up my house #PROBABLYDEADSOON #FUCKNSA"
        });

        // Police Violence Tweets
        Tweets.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "it's not hallal, tout ça #92i",
            "'terrorize Allah's ennemy and yours' QURAN , 8:60 #AbuBakr"
        });
        #endregion Tweets

        /* ******************************************************************************************
         *                                   Tags initialization                                    *
         * **************************************************************************************** */
        #region Tags

        // Positive tags
        PositiveTags.Add(EActionTag.Crime);
        PositiveTags.Add(EActionTag.Chaos);
        PositiveTags.Add(EActionTag.Misery);
        PositiveTags.Add(EActionTag.PoliceViolence);
        PositiveTags.Add(EActionTag.Order);
        PositiveTags.Add(EActionTag.Peace);

        #endregion

        /* ******************************************************************************************
         *                      End of Dirty Hard coded dialogs initialization                      *
         * **************************************************************************************** */
    }
}
