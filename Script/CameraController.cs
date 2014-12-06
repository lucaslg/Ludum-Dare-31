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

    #endregion

    // Use this for initialization
	void Start () 
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
            transform.position = new Vector3(transform.position.x + AxisX * horizontalSpeed * Time.deltaTime, transform.position.y + AxisY * verticalSpeed * Time.deltaTime, currentZoom);
        }
        else
        {
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

            
            //GameState.CurrentChannel.AddActionToChannel(zone);
        }
    }

    #region Camera Functions

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
    /// Lock the camera on an action
    /// </summary>
    /// <param name="_lockTime">(float) Time locked camera</param>
    /// <param name="zone">(IntersetZone) Zone to focus</param>
    void Lock(float _lockTime, InterestZone zone)
    {
        locking = true;
        currentLockTime = _lockTime;
    }

    #endregion
}
