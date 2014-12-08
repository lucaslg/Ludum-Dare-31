using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    private Vector2 moveVector;
    
    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    // LayerOrder
    private int minimumLayerOrder = 3;
    private const float MAXIMUM = 2;

    private Vector3 lastPosition;   // Get the last position of character : calcul of the moveVector

	private bool isWalking;

    public bool lookRight = true;

    // Use this for initialization
    public void Start()
    {
        moveVector.x = 0;
        moveVector.y = 0;

		isWalking = false;

        lastPosition = transform.position;

        if (!lookRight)
            Flip();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Get the MoveVector
        moveVector = transform.position - lastPosition;
        lastPosition = transform.position;

        //spriteRenderer.sortingOrder = 1000 - (int)((MAXIMUM + (transform.position.y) * 100));

        // Animation manager (implemented in children)
        ManageAnimation();
    }

    #region Character Functions

    /// <summary>
    /// Flip the character
    /// </summary>
    void Flip()
    {
        lookRight = !lookRight;
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        // Switch back the character
        transform.localScale = scale;
    }

    /// <summary>
    /// Animation manager, facing and animator
    /// </summary>
    public void ManageAnimation()
    {
        // Facing management
        if (!lookRight && moveVector.x > 0)
        {
            Flip();
        }
        else if (lookRight && moveVector.x < 0)
        {
            Flip();
        }

        // Animation animation
        if (moveVector.x != 0 || moveVector.y != 0 && !isWalking)
        {
            animator.SetBool("walking", true);
			isWalking = true;
        }
        else if (isWalking)
        {
            animator.SetBool("walking", false);
			isWalking = false;
        }
    }

    #endregion
}

