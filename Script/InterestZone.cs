using UnityEngine;
using System.Collections;

public enum EActionTag
{
    // Define the tags
    /* misère, violence policière, délinquance, peace, chaos, ordre */
    Order = 0,
    PoliceViolence = 1,
    Peace = 2,
    Chaos = 3,
    Crime = 4,
    Misery = 5
}

// Required component
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(SpriteRenderer))]
public class InterestZone : MonoBehaviour
{
    #region Attributes

    public bool IsActive { get; private set; }

    private Animation _animation;

    public float TimeActive = 2f;
    private float _currentTime;

    public EChannel ChannelTarget;
    public EActionTag[] Tags;

    #endregion

    // Use this for initialization
	void Start () 
    {
        IsActive = false;

        // Initialisation
        _animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsActive)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime < 0)
                IsActive = false;
        }
	}

    /// <summary>
    /// Active the interest zone
    /// </summary>
    public void ActiveZone()
    {
        _currentTime = TimeActive;
        animation.Play();
    }

    /// <summary>
    /// Return the tags of the InterstZone
    /// </summary>
    /// <returns>(ActionTag[])</returns>
    public EActionTag[] GetTags()
    {
        return Tags;
    }
}
