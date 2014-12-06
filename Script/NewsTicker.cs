using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EAudimatState
{
    Low = 0,
    Medium = 1,
    High = 2
}

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(ContentSizeFitter))]
public class NewsTicker : MonoBehaviour
{
    private Text _textComponent;
    private Vector2 _textComponentSize;
    private Vector3 _startingPosition;
    private string[] _currentAudimatMessages;
    private EAudimatState _audimatState;

    private bool initialized = false;

    public float ScrollingSpeed;
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
        if (!initialized)
        {
            GUIStyle guiStyle = new GUIStyle();
            guiStyle.font = _textComponent.font;
            guiStyle.fontSize = _textComponent.fontSize;
            _textComponentSize = guiStyle.CalcSize(new GUIContent(_textComponent.text));
            Debug.Log(_textComponentSize);

            if (_textComponentSize.x < Screen.width)
            {
                _startingPosition = new Vector3(Screen.width, 0, 0);
            }
            else
            {
                _startingPosition = new Vector3(_textComponentSize.x, 0, 0);
            }
            _textComponent.rectTransform.localPosition = _startingPosition;
            
            initialized = true;
        }

        if (_textComponent.rectTransform.localPosition.x > - _startingPosition.x)
        {
            _textComponent.rectTransform.localPosition = new Vector3(_textComponent.rectTransform.localPosition.x - ScrollingSpeed / 100 / Time.deltaTime, 0);
        }
        else
        {
            _textComponent.rectTransform.localPosition = _startingPosition;
        }
	}

    public void ChangeAudimat(EAudimatState state)
    {
        _audimatState = state;
        switch (_audimatState)
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
        initialized = false;
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
            _textComponent.text = string.Format("{0} - TOX NEWS - {1}", _textComponent.text, messages[i].ToUpper());
            i++;
        }
    }
}
