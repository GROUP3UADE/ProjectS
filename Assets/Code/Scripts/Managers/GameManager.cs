using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    public ItemDatabase ItemDatabase { get; private set; }
    public CraftDatabase CraftDatabase { get; private set; }
    public EquipmentDatabase EquipDatabase { get; private set; }
    public PlayerInventory PlayerInventory { get; private set; }
    public PopupMessageManager PopupManager { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public PlayerModel Player { get; private set; }
    public PlayerHealthBar PlayerHealthBar { get; private set; }
    public QuestManager QuestManager { get; private set; }
    public KillCountManager KillCountManager { get; private set; }

    //public GameObject _winPanel;
    //public GameObject _GOPanel;

    private void Awake()
    {
        MakeSingleton();
        LoadComponents();
    }

    private void Start()
    {
        AudioManager.Instance.PlayAmbienceMusic();
    }

    private void LoadComponents()
    {
        ItemDatabase = GetComponent<ItemDatabase>();
        CraftDatabase = GetComponent<CraftDatabase>();
        EquipDatabase = GetComponent<EquipmentDatabase>();
        PlayerInventory = GetComponent<PlayerInventory>();
        PopupManager = GetComponent<PopupMessageManager>();
        GameStateManager = GetComponent<GameStateManager>();
        Player = FindObjectOfType<PlayerModel>();
        PlayerHealthBar = GetComponent<PlayerHealthBar>();
        QuestManager = GetComponent<QuestManager>();
        KillCountManager = GetComponent<KillCountManager>();
    }

    public void GameOver()
    {
        //_GOPanel.SetActive(true);
        SceneManager.LoadScene("GameOver");
    }

    public void GameWin()
    {
        //_winPanel.SetActive(true);
        SceneManager.LoadScene("WinScene");
    }
}