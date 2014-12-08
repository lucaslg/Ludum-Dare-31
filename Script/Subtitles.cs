using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Subtitles : MonoBehaviour
{
    public GameObject Channel;

    private Channel _channel;
    private Text _textComponent;

    protected void Start()
    {
        _textComponent = gameObject.GetComponent<Text>();
        _channel = Channel.GetComponent<Channel>();
    }

    protected void Update()
    {
        if (CameraController.FocusedZone)
        {
            EActionTag[] InterestZoneTags = CameraController.FocusedZone.GetTags();
            foreach (var interestZoneTag in InterestZoneTags)
            {
                
            }
        }
    }
}
