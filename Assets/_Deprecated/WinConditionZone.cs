using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class WinConditionZone : MonoBehaviour
{
    [SerializeField] private List<ItemSO> winConditionItems;
    public GameObject _fadeWin;

    private void Start()
    {
        var spriteR = GetComponent<SpriteRenderer>();
        var coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
        coll.offset = new Vector2(0, 0);
        var bounds = spriteR.bounds;
        var lossyScale = transform.lossyScale;
        coll.size = new Vector3(bounds.size.x / lossyScale.x,
            bounds.size.y / lossyScale.y,
            bounds.size.z / lossyScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            WinCheck();
        }
    }

    private void WinCheck()
    {
        var count = 0;
        var msg = $"You are missing one of the following: \n";
        foreach (var item in winConditionItems)
        {
            if (GameManager.Instance.PlayerInventory.CheckItemSO(item, 1))
            {
                count++;
            }
            else
            {
                msg += $"\n - {item.Identifier}";
            }
        }

        if (count == 3)
        {
            _fadeWin.SetActive(true);
        }
        else
        {
            GameManager.Instance.PopupManager.ShowMessage(msg);
        }
    }
}