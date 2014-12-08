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

public class InterestZone : MonoBehaviour
{
    #region Attributes

    public bool IsDebugEnabled = false;

    public bool HasBeenSeen { get; private set; }
    public EChannel ChannelTarget;
    public EActionTag[] Tags;
    
    #endregion

    // Use this for initialization
    protected void Start()
    {
        HasBeenSeen = false;
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    /// <summary>
    /// Active the interest zone
    /// </summary>
    public void Focus()
    {
        if (IsDebugEnabled)
        {
            Debug.Log(gameObject.name + " just get focused by the camera.");
        }
        HasBeenSeen = true;
    }

    /// <summary>
    /// Returns the tags of the InterestZone
    /// </summary>
    /// <returns>(ActionTag[])</returns>
    public EActionTag[] GetTags()
    {
        return Tags;
    }
}