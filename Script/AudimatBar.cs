using UnityEngine;
using System.Collections;

public class AudimatBar : MonoBehaviour 
{

    void Update ()
    {
        gameObject.transform.localPosition = - new Vector3(0, 249.3f - GameState.GetCurrentChannelInstance().CurrentAudimat * 12.465f);
	}

}
