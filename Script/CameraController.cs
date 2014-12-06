using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    // Speed
    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;

    // Zoom
    public float minimumZoom = 0f;
    public float maximumZoom = 2f;
    public float speedZoom = 1f;
    private float currentZoom;


	// Use this for initialization
	void Start () 
    {
        currentZoom = transform.position.z;
        // Zoom Management 
        minimumZoom += currentZoom;
        maximumZoom += currentZoom;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float AxisX = Input.GetAxis("Horizontal");
        float AxisY = Input.GetAxis("Vertical");

        ZoomManager();

        // Update the position
        transform.position = new Vector3(transform.position.x + AxisX * horizontalSpeed * Time.deltaTime, transform.position.y + AxisY * verticalSpeed * Time.deltaTime, currentZoom);
	}

    #region Functions
    void ZoomManager()
    {
        // Zoom management
        if (Input.GetKey(KeyCode.PageUp))   // Zoom
        {
            currentZoom += speedZoom * Time.deltaTime;

            if (currentZoom > maximumZoom)
                currentZoom = maximumZoom;
        }
        else if (Input.GetKey(KeyCode.PageDown))    // Unzoom
        {
            currentZoom -= speedZoom * Time.deltaTime;

            if (currentZoom < minimumZoom)
                currentZoom = minimumZoom;
        }
    }
    #endregion
}
