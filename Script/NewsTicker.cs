using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(ContentSizeFitter))]
public class NewsTicker : MonoBehaviour
{
    private Text _textComponent;
    private Vector2 _textComponentSize;
    private Vector3 _startingPosition;
    private string[] _currentAudimatMessages;

    private bool _initialized = false;

    public string Separator;
    public static float ScrollingSpeed = 2;
    public string[] LowAudimatMessages;
    public string[] MediumAudimatMessages;
    public string[] HighAudimatMessages;

    // Awake (First on instantiation flow)
    protected void Awake()
    {
        _textComponent = gameObject.GetComponent<Text>();
        ClearTicker();

        // Populate the currentAudimatMessage array with Low Audimat by default
        _currentAudimatMessages = LowAudimatMessages;

        PopulateTextComponent(_currentAudimatMessages);
    }

    // Start (Second on instantiation flow)
	protected void Start ()
	{

	}
	
	// Update is called once per frame
    protected void Update()
    {
        if (!string.IsNullOrEmpty(_textComponent.text))
        {
            if (!_initialized)
            {
                GUIStyle guiStyle = new GUIStyle
                {
                    font = _textComponent.font,
                    fontSize = _textComponent.fontSize
                };
                _textComponentSize = guiStyle.CalcSize(new GUIContent(_textComponent.text));


                _startingPosition = new Vector3(Screen.width / 2.0f + _textComponentSize.x / 2.0f, 0.0f, 0.0f);

                Debug.Log(_startingPosition);
                _textComponent.rectTransform.localPosition = _startingPosition;

                _initialized = true;
            }
            else
            {
                if (_textComponent.rectTransform.localPosition.x > -_startingPosition.x)
                {
                    _textComponent.rectTransform.localPosition = new Vector3(_textComponent.rectTransform.localPosition.x - ScrollingSpeed / 100.0f / Time.deltaTime, 0);
                }
                else
                {
                    _textComponent.rectTransform.localPosition = _startingPosition;
                }
            }
        }
	}

    public void ChangeAudimat(EAudimatState state)
    {
        switch (state)
        {
            case EAudimatState.Low:
                _currentAudimatMessages = LowAudimatMessages;
                break;
            case EAudimatState.Medium:
                _currentAudimatMessages = MediumAudimatMessages;
                break;
            case EAudimatState.High:
                _currentAudimatMessages = HighAudimatMessages;
                break;
        }
        ClearTicker();
        _initialized = false;
        PopulateTextComponent(_currentAudimatMessages);
    }

    private void ClearTicker()
    {
        _textComponent.text = string.Empty;
    }

    /// <summary>
    /// Populate the textComponent with the messages array
    /// </summary>
    /// <param name="messages"></param>
    private void PopulateTextComponent(string[] messages)
    {         
        int i = 0;
        while (i < messages.Length)
        {
            _textComponent.text = string.Format("{0} {1} {2}", _textComponent.text, Separator, messages[i].ToUpper());
            i++;
        }
    }
}
