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
    private bool CanMoveUp;
    private bool CanMoveRight;
    private bool CanMoveDown;
    private bool CanMoveLeft;

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
            if (!CanMoveUp && AxisY < 0)
            {
                AxisY = 0;
            }
            if (!CanMoveDown && AxisY > 0)
            {
                AxisY = 0;
            }
            if (!CanMoveLeft && AxisX < 0)
            {
                AxisX = 0;
            }
            if (!CanMoveRight && AxisX > 0)
            {
                AxisX = 0;
            }

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
        
        RaycastHit hit;

        Ray ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.0F, 0)); // Top
        if (!Physics.Raycast(ray, out hit, 100))
        {
            CanMoveUp = false;
        }
        else
        {
            CanMoveUp = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(1.0F, 0.5F, 0)); // Right
        if (!Physics.Raycast(ray, out hit, 100))
        {
            CanMoveRight = false;
        }
        else
        {
            CanMoveRight = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 1.0F, 0)); // Bottom
        if (!Physics.Raycast(ray, out hit, 100))
        {
            CanMoveDown = false;
        }
        else
        {
            CanMoveDown = true;
        }

        ray = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.0F, 0.5F, 0)); // Left
        if (!Physics.Raycast(ray, out hit, 100))
        {
            CanMoveLeft = false;
        }
        else
        {
            CanMoveLeft = true;
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
