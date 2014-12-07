using UnityEngine;
using System.Collections;

// Required component
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(BoxCollider2D))]
public class CameraController : MonoBehaviour
{

    #region Attributes

    // Speed
    public float HorizontalSpeed = 1f;
    public float VerticalSpeed = 1f;

    // Zoom
    public float MinimumZoom = 0f;
    public float MaximumZoom = 1f;
    public float SpeedZoom = 1f;
    private float _currentZoom;

    // Locking system
    public float LockTime;
    private bool _isLocked;   // Is the camera locked
    private float _currentLockTime;  // Current locktime

    // Keymap
    public KeyCode  ZoomKey = KeyCode.PageUp, 
                    UnzoomKey = KeyCode.PageDown;

    private Vector2 _direction;
    private Vector2 _lockedPosition;

    // Limitation
    private bool _canMoveUp;
    private bool _canMoveRight;
    private bool _canMoveDown;
    private bool _canMoveLeft;

    #endregion

    // Use this for initialization
    protected void Start()
    {
        _currentZoom = transform.position.z;
        // Zoom Management 
        MinimumZoom += _currentZoom;
        MaximumZoom += _currentZoom;

        // Locking system
        _isLocked = false;
        _currentLockTime = 0f;
     }

    // Update is called once per frame
    protected void Update()
    {
        if (!_isLocked)
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
        else
        {
            // Go to the InterestZone
            GoToLockedPosition();

            _currentLockTime -= Time.deltaTime;
            if (_currentLockTime < 0f)
                _isLocked = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Action")
        {
            // Gestion audimate
            InterestZone zone = collider.GetComponent<InterestZone>();
            zone.Activate();  // Active the zone

            // Lock the camera on the action
            //Lock(5f, zone);
            //GameState.CurrentChannel.AddActionToChannel(zone);
        }
    }

    #region Camera Functions



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
            _currentZoom += SpeedZoom * Time.deltaTime;

            if (_currentZoom > MaximumZoom)
                _currentZoom = MaximumZoom;
        }
        else if (Input.GetKey(UnzoomKey))    // Unzoom
        {
            _currentZoom -= SpeedZoom * Time.deltaTime;

            if (_currentZoom < MinimumZoom)
                _currentZoom = MinimumZoom;
        }
    }

    /// <summary>
    /// Lock the camera on an action for _lockTime seconds
    /// </summary>
    /// <param name="_lockTime">(float) Time locked camera</param>
    /// <param name="zone">(IntersetZone) Zone to focus</param>
    private void Lock(float _lockTime, InterestZone zone)
    {
        if (!_isLocked)
        {
            _isLocked = true;
            _currentLockTime = _lockTime;
            _lockedPosition = new Vector2(zone.transform.position.x, zone.transform.position.y);
        }
    }

    /// <summary>
    /// Go to the locked position
    /// </summary>
    private void GoToLockedPosition()
    {
        // Go to the point
        Vector3 localDirection = ((Vector3)_lockedPosition - this.transform.position);

        // Remember to use the move script
        _direction = Vector3.Normalize(localDirection);

        transform.Translate(new Vector2(_direction.x * HorizontalSpeed * Time.deltaTime, _direction.y * VerticalSpeed * Time.deltaTime));
    }

    #endregion
}
