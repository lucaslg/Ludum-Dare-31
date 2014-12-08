using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(ContentSizeFitter))]
public class NewsTicker : MonoBehaviour
{
    public bool IsDebugEnabled = false;

    private Text _textComponent;
    private Vector2 _textComponentSize;
    private Vector3 _startingPosition;

    private bool _initialized = false;
    private bool _isSpecificNewsDisplayed = false;

    public string Separator;
    public static float ScrollingSpeed = 1;
    public string[] DefaultNews;

    // Awake (First on instantiation flow)
    protected void Awake()
    {
        _textComponent = gameObject.GetComponent<Text>();
        ClearTicker();
        PopulateTickerWithDefaultNews();
    }

    // Start (Second on instantiation flow)
	protected void Start ()
	{

	}
	
	// Update is called once per frame
    protected void Update()
    {
        // bypass update if specific news is displayed (scrolling directly handeled in coroutine)
        // bypass update if there is no text
        if (!string.IsNullOrEmpty(_textComponent.text) && !_isSpecificNewsDisplayed) 
        {
            // Have to use a dirty initialized boolean because of content size fitter
            if (!_initialized)
            {
                GUIStyle guiStyle = new GUIStyle
                {
                    font = _textComponent.font,
                    fontSize = _textComponent.fontSize
                };
                _textComponentSize = guiStyle.CalcSize(new GUIContent(_textComponent.text));

                _startingPosition = new Vector3(Screen.width / 2.0f + _textComponentSize.x / 2.0f, 0.0f, 0.0f);

                ResetTickerPosition();

                _initialized = true;
                if (IsDebugEnabled)
                {
                    Debug.Log("Initialization done. Starting position = " + _startingPosition);
                }
            }

            if (_textComponent.rectTransform.localPosition.x > -_startingPosition.x)
            {
                _textComponent.rectTransform.localPosition = new Vector3(_textComponent.rectTransform.localPosition.x - ScrollingSpeed / 10.0f / Time.deltaTime, 0);
            }
            else
            {
                ResetTickerPosition();
            }
        }
	}

    public void DisplaySpecificNews(string news)
    {
        _isSpecificNewsDisplayed = true;
        StartCoroutine(SpecificNewsDisplayManager(news));
    }

    public void PopulateTickerWithDefaultNews()
    {
        List<int> newsShuffle = new List<int>();
        int i = 0;
        while (i < DefaultNews.Length)
        {
            newsShuffle.Add(i);
            i++;
        }
        newsShuffle.Shuffle();

        int j = 0;
        while (j < newsShuffle.Count)
        {
            _textComponent.text = string.Format("{0} {1} {2}", _textComponent.text, Separator, DefaultNews[newsShuffle[j]].ToUpper());
            j++;
        }
    }

    private IEnumerator SpecificNewsDisplayManager(string news)
    {
        bool isScrollFinished = false;

        ClearTicker();
        _textComponent.text = string.Format("{0} {1} {2}", "- VIEWERS TWEET - ", news.ToUpper(), " - REACT ON #FERGUSON -");
        yield return 0;
        ComputeStartingPosition();
        ResetTickerPosition();

        while (!isScrollFinished)
        {
            if (_textComponent.rectTransform.localPosition.x > -_startingPosition.x)
            {
                _textComponent.rectTransform.localPosition = new Vector3(_textComponent.rectTransform.localPosition.x - ScrollingSpeed / 100.0f / Time.deltaTime, 0);
            }
            else
            {
                ClearTicker();
                PopulateTickerWithDefaultNews();
                isScrollFinished = true;
            }
            yield return 0;
        }

        ComputeStartingPosition();
        ResetTickerPosition();
        _initialized = true;
        _isSpecificNewsDisplayed = false;
    }

    private void ClearTicker()
    {
        _textComponent.text = string.Empty;
    }

    private void ResetTickerPosition()
    {
        _textComponent.rectTransform.localPosition = _startingPosition;
    }

    private void ComputeStartingPosition()
    {
        GUIStyle guiStyle = new GUIStyle
        {
            font = _textComponent.font,
            fontSize = _textComponent.fontSize
        };
        _textComponentSize = guiStyle.CalcSize(new GUIContent(_textComponent.text));

        _startingPosition = new Vector3(Screen.width / 2.0f + _textComponentSize.x / 2.0f, 0.0f, 0.0f);
    }
}

#region UTILITIES

public static class Utilities
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

#endregion UTILITIES
