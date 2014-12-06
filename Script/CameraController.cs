using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float AxisX = Input.GetAxis("Horizontal");
        float AxisY = Input.GetAxis("Vertical");

        // Update the position
        transform.Translate(new Vector3(AxisX * horizontalSpeed * Time.deltaTime, AxisY * verticalSpeed * Time.deltaTime, 0));
	}
}
