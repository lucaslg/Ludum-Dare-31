using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Subtitles : MonoBehaviour
{
    private Text _textComponent;

    protected void Start()
    {
        _textComponent = gameObject.GetComponent<Text>();
        _textComponent.enabled = false;
    }

    public void DisplaySubtitle(string subtitle, float duration)
    {
        StartCoroutine(SubtitleDisplayManager(subtitle, duration));
    }

    public void Clear()
    {
        _textComponent.text = string.Empty;
    }

    private IEnumerator SubtitleDisplayManager(string subtitle, float duration)
    {
        _textComponent.text = subtitle;
        _textComponent.enabled = true;
        yield return new WaitForSeconds(duration);
        Clear();
        _textComponent.enabled = false;
    }
}
