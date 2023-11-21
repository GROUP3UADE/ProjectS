using System;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Car : NPCModel
{
    private bool _isDriving;
    private PlayerModel _playerModelReference;
    [SerializeField] private Car_Stats stats;
    [SerializeField] private Button exitCarButton;
    [SerializeField] private GameObject carVisuals;

    private void Awake()
    {
        exitCarButton.onClick.AddListener(ExitCar);
    }

    public override void Interaction()
    {
        base.Interaction();
        if (IsInteractable)
        {
            EnterCar();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerModelReference == null)
        {
            _playerModelReference = other.GetComponent<PlayerModel>();
        }

        if (!_isDriving)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    private void ExitCar()
    {
        if (_isDriving)
        {
            _playerModelReference.OnCarDriveEndHandler();
            exitCarButton.gameObject.SetActive(false);
            _isDriving = false;
            ShowCar();
        }
    }

    private void EnterCar()
    {
        if (HasKey())
        {
            if (!_isDriving)
            {
                _playerModelReference.OnCarDriveStartHandler(stats.Speed);
                exitCarButton.gameObject.SetActive(true);
                _isDriving = true;
                HideCar();
            }
        }
        else
        {
            GameManager.Instance.PopupManager.ShowMessage(
                $"You need a {stats.Key.Identifier} to drive this \n (Kill 'The Survivor')");
        }
    }

    private void HideCar()
    {
        carVisuals.SetActive(false);
        interactionPopup.SetActive(false);
    }

    private void ShowCar()
    {
        transform.position = _playerModelReference.transform.position;
        carVisuals.SetActive(true);
    }

    private bool HasKey()
    {
        return GameManager.Instance.PlayerInventory.CheckItemSO(stats.Key, 1);
    }
}