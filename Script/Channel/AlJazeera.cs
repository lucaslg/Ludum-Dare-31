using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AlJazeera : Channel
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
            "At least a dozen buildings were set on fire around the city, many in the vicinity of Ferguson Market and Liquor, the store Michael Brown was in before he was killed by Officer Wilson. ",
            "Several stores were looted and vandalized during the protests."
        });

        // Crime comments
        SpeakerComments.Add(EActionTag.Crime, new List<string>()
        {
            "As the night goes on, the situation grows more intense. Buildings are set on fire, and looting are reported in several businesses.",
            "Protesters surge forward, throwing objects at officers in riot gear. The sound of gunfire can be heard. A vehicle was just set on fire."
        });

        // Misery comments
        SpeakerComments.Add(EActionTag.Misery, new List<string>()
        {
            "African-Americans, most particulary African-Americans men, are still likely more to be stopped and searched by police, charged with crimes and sentenced by longer prison terms.",
            "African-Americans are afflicted by discrimination."
        });

        // Order comments
        SpeakerComments.Add(EActionTag.Order, new List<string>()
        {
            "Police officers use tear gas and smoke to disperse people who are hurling rocks and breaking the windows of parked police cruisers.",
            "Confrontations between protesters and law enforcement officers continue even after Gov. Jay Nixon deployed the Missouri National Guard to help quell the unrest."
        });

        // Peace comments
        SpeakerComments.Add(EActionTag.Peace, new List<string>()
        {
            "The first amendement to the United States protects the Freedom of Speech and the right to demonstrate.",
            "The announcement of the Great Jury set off another wave of protests."
        });

        // Police Violence comments
        SpeakerComments.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "In some places, African-Americans are not treated with the proper respect.",
            "Michael Brown, an unarmed black teenager, was shot and killed on Aug. 9."
        });
        #endregion Speaker's comments

        /* ******************************************************************************************
         *                                  Tweets initialization                                   *
         * **************************************************************************************** */
        #region Tweets

        // Chaos Tweets
        Tweets.Add(EActionTag.Chaos, new List<string>()
        {
            "Take food stamps away from all these losers. They burn our flag, shut this #chimpout immediately #shutitdown",
            "They just have set fire to my KFC !!!!!!! #colonelsanders"
        });

        // Crime Tweets
        Tweets.Add(EActionTag.Crime, new List<string>()
        {
            "If #BlackLivesMatter why do Blacks keep killing each other? #chimpout",
            "Glued to livestreams for hours watching the #chimpout in #Ferguson tonight. Running out of popcorn",
            "Just put a new brush gaurd on my truck...no time like the present to test it out !"
        });

        // Misery Tweets
        Tweets.Add(EActionTag.Misery, new List<string>()
        {
            "Police should Protect people, not Kill them #injustice",
            "Actually sad to see how African-Americans are treated !!"
        });

        // Order Tweets
        Tweets.Add(EActionTag.Order, new List<string>()
        {
            "We support the St. Louis City and County Police. #trueamericans",
            "Order has to be maintained in Ferguson. It cannot go on like that… #order"
        });

        // Peace Tweets
        Tweets.Add(EActionTag.Peace, new List<string>()
        {
            "Here are the real activists !",
            "It’s a good thing to see people fight for their rights !",
            "Real Live Liberal America !"
        });

        // Police Violence Tweets
        Tweets.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "The US Constitution is supposed to protect this people !",
            "Americans=no history/no education /no decency"
        });
        #endregion Tweets

        /* ******************************************************************************************
         *                                   Tags initialization                                    *
         * **************************************************************************************** */

        #region Tags

        // Positive tags
        PositiveTags.Add(EActionTag.Misery);
        PositiveTags.Add(EActionTag.Peace);
        PositiveTags.Add(EActionTag.PoliceViolence);

        // Negative tags
        NegativeTags.Add(EActionTag.Crime);
        NegativeTags.Add(EActionTag.Chaos);
        NegativeTags.Add(EActionTag.Order);
        
        #endregion

        WinningSentence = "Following the slow destruction of Fergusion in the name of freedom, qatari princes acquired all of the real estate market in the city. " +
                          "Streets are now paved with gold and the Football Club of Ferguson has become the top one MLS franchise in America.";

        LosingSentence = "The United States of America declared Qatar had acquired nuclear armament and therefore invaded the country. In other news, the oil price in America dropped to 5 dollars per barrel.";

        /* ******************************************************************************************
         *                      End of Dirty Hard coded dialogs initialization                      *
         * **************************************************************************************** */

        
    }
}
