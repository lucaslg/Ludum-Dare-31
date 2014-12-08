using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AnarchyTV : Channel 
{
    #region Singleton Implementation
    private static AnarchyTV _instance = null;

    public static AnarchyTV Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AnarchyTV();
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
            "See what comes when you fuck too much with poor people !",
            "Atmosphere is SOOOO hot right now! Hope you can join the party..."
        });


        // Crime comments
        SpeakerComments.Add(EActionTag.Crime, new List<string>()
        {
            "It's only fair reparations.",
            "Ever heard of reappropriation? EVERYTHING is ours and i mean EVERYTHING!"

        });

        // Misery comments
        SpeakerComments.Add(EActionTag.Misery, new List<string>()
        {
            "That shit had gone too long...",
            "Capitalist exploitation really IS killin' us all!"

        });

        // Order comments
        SpeakerComments.Add(EActionTag.Order, new List<string>()
        {
            "Imperialist dogs of globalized capitalism! You deserve everything you're gonna get tonight!",
            "Psycho-fascist shit goin' on here!"

        });

        // Peace comments
        SpeakerComments.Add(EActionTag.Peace, new List<string>()
        {
            "Lame asses hippies ALWAYYYS crash the party!",
            "Tsss... They ain't gonna shake the system with that..."

        });

        // Police Violence comments
        SpeakerComments.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "Scumbags cops always loooove hittin' unarmed people.",
            "Fuck that, you monsters! Die fuckin Pig! die!"

        });
        #endregion Speaker's comments

        /* ******************************************************************************************
         *                                  Tweets initialization                                   *
         * **************************************************************************************** */
        #region Tweets
        // Chaos Tweets
        Tweets.Add(EActionTag.Chaos, new List<string>()
        {
            "know where I can score some weed in Santa-Monica tonight? #SHAKINPRETTYBAD #OBSIDIAN",
            "oooooooh shit! Burn some pigs for us yall Ferguson ppl! #DiePIGDie"
        });

        // Crime Tweets
        Tweets.Add(EActionTag.Crime, new List<string>()
        {
            "Hey! Leave some XBOX One for us guys, I was sick today, #PackiNForTonighT",
            "That's so cool my friends and I want to do a documentary on you guys and were wondering if... #STUPIDOCCUPYSTUDENT"
        });

        // Misery Tweets
        Tweets.Add(EActionTag.Misery, new List<string>()
        {
            "Capitalism is not a system but a social link between antagonist classes based on their production...#DialecticMaterialismISBORING",
            "street is the street. always have always will be. Fo life. Sure thing, mos definitly #UKNOWMAN #SAMESHITEVERYWHERE"
        });

        // Order Tweets
        Tweets.Add(EActionTag.Order, new List<string>()
        {
            "die u anarchists punkasses make me sick, we're gonna find you and kill you, traitor to the white race #KKKforlife",
            "we should really get some c4 and fast #ARMEDSTRUGGLE #DIRECTACTION #UNABOMBER"
        });

        // Peace Tweets
        Tweets.Add(EActionTag.Peace, new List<string>()
        {
            "peace to every1, blak ppl R the only tru gods #CLARENCE13X",
            "Beat the fuckin' rasta u dirty thugs #HippiesRDirtY"
        });

        // Police Violence Tweets
        Tweets.Add(EActionTag.PoliceViolence, new List<string>()
        {
            "Time to pick up you gun... #COPONAFORK",
            "Who watches this shit anyway? We're 12 viewers right now! #Stoplivinginafantasyworld"
        });
        #endregion Tweets
        /* ******************************************************************************************
         *                      End of Dirty Hard coded dialogs initialization                      *
         * **************************************************************************************** */
    }
}
