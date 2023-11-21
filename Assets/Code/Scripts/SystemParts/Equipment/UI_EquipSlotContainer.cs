using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipSlotContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonIcon;
    [SerializeField] private Button button;

    public TextMeshProUGUI ButtonText => buttonText;
    public Image ButtonIcon => buttonIcon;
    public Button Button => button;
}