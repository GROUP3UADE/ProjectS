using System;
using UnityEngine;

public class NPCModel : MonoBehaviour, IInteractable
{
    protected bool IsInteractable;
    [SerializeField] protected GameObject interactionPopup;
    [SerializeField] protected NPCData data;

    public virtual void Interaction()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var pm = other.GetComponent<PlayerModel>();
        if (pm)
        {
            IsInteractable = true;
            PopupUpdate(IsInteractable);
            pm.SaveInteractable(this);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        var pm = other.GetComponent<PlayerModel>();
        if (pm)
        {
            IsInteractable = false;
            PopupUpdate(IsInteractable);
            pm.SaveInteractable(null);
        }
    }

    private void PopupUpdate(bool state)
    {
        interactionPopup.SetActive(state);
    }

    // Usar esto para cada npc nuevo y utilizar apropiadamente
    // public override void Interaction()
    // {
    //     base.Interaction();
    // }
    //
    // public override void OnTriggerEnter2D(Collider2D other)
    // {
    //     base.OnTriggerEnter2D(other);
    // }
    //
    // public override void OnTriggerExit2D(Collider2D other)
    // {
    //     base.OnTriggerExit2D(other);
    // }
}