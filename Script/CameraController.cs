using UnityEngine;
using System.Collections;

// Required component
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(BoxCollider2D))]
public class CameraController : MonoBehaviour
{

    #region Attributes

    // Speed
    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;

    // Zoom
    public float minimumZoom = 0f;
    public float maximumZoom = 2f;
    public float speedZoom = 1f;
    private float currentZoom;

    // Locking system
    public float lockTime;
    private bool locking;   // Is the camera locked
    private float currentLockTime;  // Current locktime

    // Keymap
    public KeyCode zoomKey = KeyCode.PageUp, unzoomKey = KeyCode.PageDown;

    private Vector2 direction;
    private Vector2 lockedPosition;

    // Limitation
    private Rect mapBoundsCollider = new Rect(5.7f, 2.25f, 11.4f, 4.5f);

    #endregion

    // Use this for initialization
    void Start()
    {
        currentZoom = transform.position.z;
        // Zoom Management 
        minimumZoom += currentZoom;
        maximumZoom += currentZoom;

        // Locking system
        locking = false;
        currentLockTime = 0f;

        mapBoundsCollider = new Rect(transform.position.x - mapBoundsCollider.xMin, transform.position.y - mapBoundsCollider.yMin, mapBoundsCollider.width, mapBoundsCollider.height);
     }

    // Update is called once per frame
    void Update()
    {
        if (!locking)
        {
            float AxisX = Input.GetAxis("Horizontal");
            float AxisY = Input.GetAxis("Vertical");

            ZoomManager();

            // Update the position
            transform.position = new Vector3(transform.position.x + AxisX * horizontalSpeed * Time.deltaTime, transform.position.y + AxisY * verticalSpeed * Time.deltaTime, currentZoom);

            CollideManager();
        }
        else
        {
            // Go to the InterestZone
            GoToLockedPosition();

            currentLockTime -= Time.deltaTime;
            if (currentLockTime < 0f)
                locking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Action")
        {
            // Gestion audimate
            InterestZone zone = collider.GetComponent<InterestZone>();
            zone.ActiveZone();  // Active the zone

            // Lock the camera on the action
            //Lock(5f, zone);
            //GameState.CurrentChannel.AddActionToChannel(zone);
        }
    }

    #region Camera Functions

    /// <summary>
    /// Camera collide the bounds of the map
    /// </summary>
    void CollideManager()
    {
        if (transform.position.x > mapBoundsCollider.xMax)
        {
            transform.position = new Vector3(mapBoundsCollider.xMax, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < mapBoundsCollider.xMin)
        {
            transform.position = new Vector3(mapBoundsCollider.xMin, transform.position.y, transform.position.z);
        }
        if (transform.position.y > mapBoundsCollider.yMax)
        {
            transform.position = new Vector3(transform.position.x, mapBoundsCollider.yMax, transform.position.z);
        }
        else if (transform.position.y < mapBoundsCollider.yMin)
        {
            transform.position = new Vector3(transform.position.x, mapBoundsCollider.yMin, transform.position.z);
        }
    }

    /// <summary>
    /// Camera zoom manager
    /// </summary>
    void ZoomManager()
    {
        // Zoom management
        if (Input.GetKey(zoomKey))   // Zoom
        {
            currentZoom += speedZoom * Time.deltaTime;

            if (currentZoom > maximumZoom)
                currentZoom = maximumZoom;
        }
        else if (Input.GetKey(unzoomKey))    // Unzoom
        {
            currentZoom -= speedZoom * Time.deltaTime;

            if (currentZoom < minimumZoom)
                currentZoom = minimumZoom;
        }
    }

    /// <summary>
    /// Lock the camera on an action for _lockTime seconds
    /// </summary>
    /// <param name="_lockTime">(float) Time locked camera</param>
    /// <param name="zone">(IntersetZone) Zone to focus</param>
    void Lock(float _lockTime, InterestZone zone)
    {
        if (!locking)
        {
            locking = true;
            currentLockTime = _lockTime;
            lockedPosition = new Vector2(zone.transform.position.x, zone.transform.position.y);
        }
    }

    /// <summary>
    /// Go to the locked position
    /// </summary>
    void GoToLockedPosition()
    {
        // Go to the point
        Vector3 localDirection = ((Vector3)lockedPosition - this.transform.position);

        // Remember to use the move script
        direction = Vector3.Normalize(localDirection);

        transform.Translate(new Vector2(direction.x * horizontalSpeed * Time.deltaTime, direction.y * verticalSpeed * Time.deltaTime));
    }

    #endregion
}
