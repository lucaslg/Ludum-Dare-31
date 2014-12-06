using UnityEngine;
using System.Collections;

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

    private SpriteRenderer spriteRenderer;
    private Animation animation;

    public float timeToPlay = 2f;
    private float currentTime;

    #endregion

    // Use this for initialization
	void Start () 
    {
        isActive = false;

        // Initialisation
        animation = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        currentTime = timeToPlay;
        animation.Play();
    }
}
