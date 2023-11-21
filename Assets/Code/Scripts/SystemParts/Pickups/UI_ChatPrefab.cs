using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class UI_ChatPrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float fadeDuration;

    public void SetupText(string msg)
    {
        textComponent.text = msg;
        textComponent.CrossFadeColor(Color.clear, fadeDuration, false, true);
        Invoke(nameof(Disappear), fadeDuration / 2);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}