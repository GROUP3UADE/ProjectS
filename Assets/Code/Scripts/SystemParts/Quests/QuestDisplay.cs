using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    // Aca hacemos otra pantalla dentro del menu de "pausa"
    // No se si quiero mostrarlo en el HUD tamb
    // No se si vamos a tener integracion con mapa/minimapa
    private QuestManager _questManager;
    [SerializeField] private GameObject popupMenu;

    private void Awake()
    {
        _questManager = GetComponent<QuestManager>();
        _questManager.OnQuestReceived += DisplayActiveQuests;
        _questManager.OnQuestReceived += RecieveQuestPopup;
        _questManager.OnQuestCompleted += DisplayCompletedQuests;
        _questManager.OnQuestCompleted += CompleteQuestPopup;
    }

    public void Show()
    {
        //Show stuff
        popupMenu.SetActive(true);
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    // Modo console debug
    private void DisplayActiveQuests(Quest q)
    {
        var msg = "";
        foreach (var quest in _questManager.ActiveQuests)
        {
            msg += $"Quest: {quest.QuestName} is Active \n";
        }

        Debug.Log(msg);
    }

    // En quest manager cambie el evento a que siempre tire una quest, se puede sacar para el libro de quests
    private void RecieveQuestPopup(Quest q)
    {
        var msg = "";
        msg += $"Quest: {q.QuestName} received! \n";
        GameManager.Instance.PopupManager.ShowMessage(msg);
    }

    // Modo console debug
    private void DisplayCompletedQuests(Quest q)
    {
        var msg = "";
        foreach (var quest in _questManager.CompletedQuests)
        {
            msg += $"Quest: {quest.QuestName} completed! \n";
        }

        Debug.Log(msg);
    }

    private void CompleteQuestPopup(Quest q)
    {
        var msg = "";
        msg += $"Quest: {q.QuestName} completed! \n";
        GameManager.Instance.PopupManager.ShowMessage(msg);
    }
}