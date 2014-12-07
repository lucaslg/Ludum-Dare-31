using UnityEngine;
using System.Collections;

/// <summary>
/// Action Script : Manage the trigger of IntersetZone
/// </summary>
public class ActionScript : MonoBehaviour {

	public float _firstAction = 20f;
	public float _secondAction = 40f;


	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Countdown ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator Countdown()
	{
		for (float timer = 3; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		Debug.Log("This message appears after 3 seconds!");
	}
}
