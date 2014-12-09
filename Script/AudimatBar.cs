using UnityEngine;
using System.Collections;

public class AudimatBar : MonoBehaviour 
{

    void Update ()
    {
        gameObject.transform.localPosition = - new Vector3(0, 321f - GameState.GetCurrentChannelInstance().CurrentAudimat * 16.05f);
	}

}
