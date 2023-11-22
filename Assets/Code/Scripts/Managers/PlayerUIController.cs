using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject popupMenu;
    private InventoryDisplay _playerInventory;
    private CraftDisplay _playerCrafting;
    private EquipmentDisplay _playerEquipment;

    private int _currentFilter;
    private int _lengthFilter = 2; // 0=Inventory 1=Crafting 2=Equipment

    private void Awake()
    {
        _playerInventory = GetComponent<InventoryDisplay>();
        _playerCrafting = GetComponent<CraftDisplay>();
        _playerEquipment = GetComponent<EquipmentDisplay>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlayInventorySound();
            ScreenBg();
            ShowInventory();
            _currentFilter = 0;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ScreenBg();
            ShowCrafting();
            _currentFilter = 1;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScreenBg();
            ShowEquipment();
            _currentFilter = 2;
        }
    }

    private void ScreenBg()
    {
        var ena = popupMenu.activeInHierarchy;
        popupMenu.SetActive(!ena);
    }

    private void ShowInventory()
    {
        _playerInventory.Show();
        _playerCrafting.Hide();
        _playerEquipment.Hide();
    }

    private void ShowCrafting()
    {
        _playerInventory.Hide();
        _playerCrafting.Show();
        _playerEquipment.Hide();
    }

    private void ShowEquipment()
    {
        _playerInventory.Hide();
        _playerCrafting.Hide();
        _playerEquipment.Show();
    }

    [ContextMenu("NextFilter")]
    public void NextFilter()
    {
        _currentFilter++;
        if (_currentFilter > _lengthFilter)
        {
            _currentFilter = 0;
        }

        SwitchUi();
    }

    [ContextMenu("PrevFilter")]
    public void PreviousFilter()
    {
        _currentFilter--;
        if (_currentFilter < 0)
        {
            _currentFilter = _lengthFilter;
        }

        SwitchUi();
    }

    private void SwitchUi()
    {
        switch (_currentFilter)
        {
            case 0:
                ShowInventory();
                break;

            case 1:
                ShowCrafting();
                break;

            case 2:
                ShowEquipment();
                break;

            default:
                ShowInventory();
                break;
        }
    }
}