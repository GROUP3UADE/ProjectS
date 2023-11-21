using System;
using TMPro;
using UnityEngine;

public class PopupMessageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject popupGo;


    public void ShowMessage(string message)
    {
        popupGo.SetActive(true);
        messageText.text = message;
        GameManager.Instance.GameStateManager.InactiveState();
    }

    public void HideMessage()
    {
        popupGo.SetActive(false);
        messageText.text = "";
        GameManager.Instance.GameStateManager.ActiveState();
    }
}