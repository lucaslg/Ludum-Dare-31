using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

// Required component
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(BoxCollider2D))]
public class CameraController : MonoBehaviour
{
    public bool IsDebugEnabled;

    #region Attributes

    // Speed
    public float HorizontalSpeed = 3f;
    public float VerticalSpeed = 3f;

    // Zoom
    public float MinimumZoom = 0f;
    public float MaximumZoom = 10f;
    public float SpeedZoom = 3f;
    private float _currentZoom;
    private float _friendlyCurrentZoom;

    /// <summary>
    /// True if the camera is locked (can't move)
    /// </summary>
    public bool IsLocked;

    public static InterestZone FocusedZone;

    // Keymap
    public KeyCode ZoomKey = KeyCode.PageUp;
    public KeyCode UnzoomKey = KeyCode.PageDown;

    private Vector2 _direction;

    // Limitation
    private bool _canMoveUp;
    private bool _canMoveRight;
    private bool _canMoveDown;
    private bool _canMoveLeft;

    #endregion

    // Use this for initialization
    protected void Start()
    {
        _friendlyCurrentZoom = 0;
        _currentZoom = transform.position.z;

        // Zoom Management 
        MinimumZoom += _currentZoom;
        MaximumZoom += _currentZoom;

        // Locking system
        IsLocked = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!IsLocked)
        {
            float axisX = Input.GetAxis("Horizontal");
            float axisY = Input.GetAxis("Vertical");

            ZoomManager();

            // Update the position
            if (!_canMoveUp && axisY < 0)
            {
                axisY = 0;
            }
            if (!_canMoveDown && axisY > 0)
            {
                axisY = 0;
            }
            if (!_canMoveLeft && axisX < 0)
            {
                axisX = 0;
            }
            if (!_canMoveRight && axisX > 0)
            {
                axisX = 0;
            }

            transform.position = new Vector3(transform.position.x + axisX * HorizontalSpeed * Time.deltaTime, transform.position.y + axisY * VerticalSpeed * Time.deltaTime, _currentZoom);

            CollideManager();
        }

        ManageInterestZone();
    }

    #region Camera Functions

    private void ManageInterestZone()
    {
        if (!IsLocked)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.forward);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.tag == "InterestZone" &&
                    Math.Abs(_friendlyCurrentZoom - MaximumZoom) < 0.1 && // Comparison of floats with 0.1 margin error
                    !hit.collider.GetComponent<InterestZone>().HasBeenSeen)
                {
                    FocusedZone = hit.collider.gameObject.GetComponent<InterestZone>();
                    Lock();
                    FocusedZone.Focus();
                    DisplaySubtitlesAndTweet();
                }
            }
        }
    }

    private void DisplaySubtitlesAndTweet()
    {
        List<EActionTag> winningTags = ComputeWinningTags(FocusedZone.GetTags());

        int rng = UnityEngine.Random.Range(0, winningTags.Count);
        string comment = ComputeSubtitleForTag(winningTags[rng]);

        rng = UnityEngine.Random.Range(0, winningTags.Count);
        string tweet = ComputeWinningTweetForTag(winningTags[rng]);

        GameState.GetCurrentChannelInstance().Subtitles.GetComponent<Subtitles>().DisplaySubtitle(comment, GameMode.ActionFreezeDuration);
        GameState.GetCurrentChannelInstance().NewsTicker.GetComponent<NewsTicker>().DisplaySpecificNews(tweet);
    }

    private List<EActionTag> ComputeWinningTags(EActionTag[] tags)
    {
        int score = 0;
        List<EActionTag> positiveTags = new List<EActionTag>();
        List<EActionTag> negativeTags = new List<EActionTag>();
        foreach (EActionTag actionTag in tags)
        {
            foreach (EActionTag channelPositiveActionTag in GameState.GetCurrentChannelInstance().PositiveTags)
            {
                if (actionTag == channelPositiveActionTag)
                {
                    positiveTags.Add(actionTag);
                    score++;
                }
            }

            foreach (EActionTag channelNegativeActionTag in GameState.GetCurrentChannelInstance().NegativeTags)
            {
                if (actionTag == channelNegativeActionTag)
                {
                    negativeTags.Add(actionTag);
                    score--;
                }
            }
        }

        if (IsDebugEnabled)
        {
            Debug.Log("Tag Score = " + score);
        }

        if (score > 0)
        {
            if (IsDebugEnabled)
            {
                Debug.Log("Associated tags : " + positiveTags.ToArray().ToString());
            }
            return positiveTags;
        }
        else
        {
            if (IsDebugEnabled)
            {
                Debug.Log("Associated tags : " + negativeTags.ToArray().ToString());
            }
            return negativeTags;
        }
    }

    private String ComputeSubtitleForTag(EActionTag tag)
    {
        int rng = UnityEngine.Random.Range(0, GameState.GetCurrentChannelInstance().SpeakerComments[tag].Count);
        string comment = GameState.GetCurrentChannelInstance().SpeakerComments[tag][rng];
        if (IsDebugEnabled)
        {
            Debug.Log("ComputeSubtitleForTag returned : " + comment + "\nfor tag : " + tag);
        }
        return GameState.GetCurrentChannelInstance().SpeakerComments[tag][rng];
    }

    private String ComputeWinningTweetForTag(EActionTag tag)
    {
        int rng = UnityEngine.Random.Range(0, GameState.GetCurrentChannelInstance().Tweets[tag].Count);
        string tweet = GameState.GetCurrentChannelInstance().Tweets[tag][rng];
        if (IsDebugEnabled)
        {
            Debug.Log("ComputeTweetForTag returned : " + tweet + "\nfor tag : " + tag);
        }
        return tweet;
    }

    /// <summary>
    /// Camera collide the bounds of the map
    /// </summary>
    private void CollideManager()
    {
        RaycastHit hit;

        Ray ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.0F, 0)); // Top
        if (!Physics.Raycast(ray, out hit, 100))
        {
            _canMoveUp = false;
        }
        else
        {
            _canMoveUp = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(1.0F, 0.5F, 0)); // Right
        if (!Physics.Raycast(ray, out hit, 100))
        {
            _canMoveRight = false;
        }
        else
        {
            _canMoveRight = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 1.0F, 0)); // Bottom
        if (!Physics.Raycast(ray, out hit, 100))
        {
            _canMoveDown = false;
        }
        else
        {
            _canMoveDown = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.0F, 0.5F, 0)); // Left
        if (!Physics.Raycast(ray, out hit, 100))
        {
            _canMoveLeft = false;
        }
        else
        {
            _canMoveLeft = true;
        }
    }

    /// <summary>
    /// Camera zoom manager
    /// </summary>
    private void ZoomManager()
    {
        // Zoom management
        if (Input.GetKey(ZoomKey))   // Zoom
        {
            if (_currentZoom < MaximumZoom)
            {
                _currentZoom += SpeedZoom * Time.deltaTime;
                _friendlyCurrentZoom += SpeedZoom * Time.deltaTime;
            }
            if (_currentZoom > MaximumZoom)
            {
                _friendlyCurrentZoom = MaximumZoom;
                _currentZoom = MaximumZoom;
            }
        }
        else if (Input.GetKey(UnzoomKey))    // Unzoom
        {
            if (_currentZoom > MinimumZoom)
            {
                _currentZoom -= SpeedZoom * Time.deltaTime;
                _friendlyCurrentZoom -= SpeedZoom * Time.deltaTime;
            }
            if (_currentZoom < MinimumZoom)
            {
                _friendlyCurrentZoom = MinimumZoom;
                _currentZoom = MinimumZoom;
            }
        }
    }

    public void ResetZoom()
    {
        _currentZoom = -13.9f;
        _friendlyCurrentZoom = 0f;
        gameObject.transform.localPosition = new Vector3(0, 0, -13.9f);
    }

    private void Lock()
    {
        IsLocked = true;
        StartCoroutine(Lock_Coroutine(GameMode.ActionFreezeDuration));
    }

    private IEnumerator Lock_Coroutine(float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return 0;
        }
        IsLocked = false;
        ResetZoom();
        yield return 0;
    }
    #endregion
}