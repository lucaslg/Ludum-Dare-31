using UnityEngine;
using System.Collections;

public enum ActionTag
{
    // Define the tags
    /* misère, violence policière, délinquance, peace, chaos, ordre */
    ORDER = 0,
    POLICE_VIOLENCE = 1,
    PEACE = 2,
    CHAOS = 3,
    CRIME = 4,
    MISERY = 5
}

// Required component
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(SpriteRenderer))]
public class InterestZone : MonoBehaviour
{

    #region Attributes

    private bool isActive;  // Is the Zone active?
    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    private Animation animation;

    public float timeActive = 2f;
    private float currentTime;

    public EChannel ChannelTarget;
    public ActionTag[] tags;

    #endregion

    // Use this for initialization
	void Start () 
    {
        isActive = false;

        // Initialisation
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActive)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
                isActive = false;
        }
	}

    /// <summary>
    /// Active the interest zone
    /// </summary>
    public void ActiveZone()
    {
        currentTime = timeActive;
        animation.Play();
    }

    /// <summary>
    /// Return the tags of the InterstZone
    /// </summary>
    /// <returns>(ActionTag[])</returns>
    public ActionTag[] GetTags()
    {
        return tags;
    }
}
