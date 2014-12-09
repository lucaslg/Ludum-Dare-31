using UnityEngine;
using System.Collections;

// NO TIME LEFT ! LET'S CODE DIRTY !!!! QUICK !
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public bool IsDebugEnabled = true;

    public GameObject Image;
    public GameObject WinningChannelText;
    public GameObject LosingChannelText;

    public Sprite FoxNews_EndScreen;
    public Sprite AlJazeera_EndScreen;
    public Sprite AnarchyTV_EndScreen;
    public Sprite BokoHaram_EndScreen;

    protected void Start()
    {
        
    }

    public void Win()
    {
        Debug.Log(GameState.FoxNewsInstance.LosingSentence);
        Debug.Log(GameState.AlJazeeraInstance.LosingSentence);
        Debug.Log(GameState.BokoHaramInstance.LosingSentence);
        Debug.Log(GameState.AnarchyTVInstance.LosingSentence);

        EChannel winningChannel = ComputeWinningChannel();
        EChannel losingChannel = ComputeLosingChannel();
        Image.GetComponent<Image>().sprite = GetImageToDisplay(winningChannel);

        WinningChannelText.GetComponent<Text>().text = GetWinningText(winningChannel);
        LosingChannelText.GetComponent<Text>().text = GetLosingText(losingChannel);
    }

    public EChannel ComputeWinningChannel()
    {
        int higherAudimat = GameState.AlJazeeraInstance.CurrentAudimat;
        EChannel higherAudimatChannel = EChannel.AlJazeera;

        if (GameState.AnarchyTVInstance.CurrentAudimat > higherAudimat)
        {
            higherAudimat = GameState.AnarchyTVInstance.CurrentAudimat;
            higherAudimatChannel = EChannel.AnarchyTV;
        }
        else if (GameState.AnarchyTVInstance.CurrentAudimat == higherAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0 :
                    higherAudimat = GameState.AnarchyTVInstance.CurrentAudimat;
                    higherAudimatChannel = EChannel.AnarchyTV;
                    break;
                case 1:
                    break;
            }
        }

        if (GameState.BokoHaramInstance.CurrentAudimat > higherAudimat)
        {
            higherAudimat = GameState.BokoHaramInstance.CurrentAudimat;
            higherAudimatChannel = EChannel.BokoHaram;
        }
        else if (GameState.BokoHaramInstance.CurrentAudimat == higherAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    higherAudimat = GameState.BokoHaramInstance.CurrentAudimat;
                    higherAudimatChannel = EChannel.BokoHaram;
                    break;
                case 1:
                    break;
            }
        }

        if (GameState.FoxNewsInstance.CurrentAudimat > higherAudimat)
        {
            higherAudimat = GameState.FoxNewsInstance.CurrentAudimat;
            higherAudimatChannel = EChannel.FoxNews;
        }
        else if (GameState.FoxNewsInstance.CurrentAudimat == higherAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    higherAudimat = GameState.FoxNewsInstance.CurrentAudimat;
                    higherAudimatChannel = EChannel.FoxNews;
                    break;
                case 1:
                    break;
            }
        }

        if (IsDebugEnabled)
            Debug.Log("Winning channel = " + higherAudimatChannel.ToString());

        return higherAudimatChannel;
    }


    public EChannel ComputeLosingChannel()
    {
        int lowerAudimat = GameState.AlJazeeraInstance.CurrentAudimat;
        EChannel lowerAudimatChannel = EChannel.AlJazeera;

        if (GameState.AnarchyTVInstance.CurrentAudimat < lowerAudimat)
        {
            lowerAudimat = GameState.AnarchyTVInstance.CurrentAudimat;
            lowerAudimatChannel = EChannel.AnarchyTV;
        }
        else if (GameState.AnarchyTVInstance.CurrentAudimat == lowerAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    lowerAudimat = GameState.AnarchyTVInstance.CurrentAudimat;
                    lowerAudimatChannel = EChannel.AnarchyTV;
                    break;
                case 1:
                    break;
            }
        }

        if (GameState.BokoHaramInstance.CurrentAudimat < lowerAudimat)
        {
            lowerAudimat = GameState.BokoHaramInstance.CurrentAudimat;
            lowerAudimatChannel = EChannel.BokoHaram;
        }
        else if (GameState.BokoHaramInstance.CurrentAudimat == lowerAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    lowerAudimat = GameState.BokoHaramInstance.CurrentAudimat;
                    lowerAudimatChannel = EChannel.BokoHaram;
                    break;
                case 1:
                    break;
            }
        }

        if (GameState.FoxNewsInstance.CurrentAudimat < lowerAudimat)
        {
            lowerAudimat = GameState.FoxNewsInstance.CurrentAudimat;
            lowerAudimatChannel = EChannel.FoxNews;
        }
        else if (GameState.FoxNewsInstance.CurrentAudimat == lowerAudimat)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    lowerAudimat = GameState.FoxNewsInstance.CurrentAudimat;
                    lowerAudimatChannel = EChannel.FoxNews;
                    break;
                case 1:
                    break;
            }
        }

        if (IsDebugEnabled)
            Debug.Log("Losing channel = " + lowerAudimatChannel.ToString());

        return lowerAudimatChannel;
    }


    public Sprite GetImageToDisplay(EChannel channel)
    {
        switch (channel)
        {
            case EChannel.AlJazeera:
                return AlJazeera_EndScreen;

            case EChannel.AnarchyTV:
                return AnarchyTV_EndScreen;

            case EChannel.BokoHaram:
                return BokoHaram_EndScreen;

            case EChannel.FoxNews:
                return FoxNews_EndScreen;
        }
        return null;
    }

    public string GetWinningText(EChannel channel)
    {
        string winningText = null;
        switch (channel)
        {
            case EChannel.AlJazeera:
                winningText = GameState.AlJazeeraInstance.WinningSentence;
                break;

            case EChannel.AnarchyTV:
                winningText = GameState.AnarchyTVInstance.WinningSentence;
                break;

            case EChannel.BokoHaram:
                winningText = GameState.BokoHaramInstance.WinningSentence;
                break;

            case EChannel.FoxNews:
                winningText = GameState.FoxNewsInstance.WinningSentence;
                break;
        }

        if (IsDebugEnabled)
            Debug.Log("Winning Text = " + winningText);

        return winningText;
    }

    public string GetLosingText(EChannel channel)
    {
        string losingText = null;
        switch (channel)
        {
            case EChannel.AlJazeera:
                losingText = GameState.AlJazeeraInstance.LosingSentence;
                break;

            case EChannel.AnarchyTV:
                losingText = GameState.AnarchyTVInstance.LosingSentence;
                break;

            case EChannel.BokoHaram:
                losingText = GameState.BokoHaramInstance.LosingSentence;
                break;

            case EChannel.FoxNews:
                losingText = GameState.FoxNewsInstance.LosingSentence;
                break;
        }

        if (IsDebugEnabled)
            Debug.Log("Losing Text = " + losingText);

        return losingText;
    }

    public void BackToMenu()
    {
        Application.LoadLevel(0);
    }
}
