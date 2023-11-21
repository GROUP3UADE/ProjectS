using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class WinItemZone : MonoBehaviour
{
    [SerializeField] private Quest winQuest;

    private void Awake()
    {
        var col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 1;
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("PlayerDetection"))
    //    {
    //        WinCheck();
    //    }
    //}

    //private void WinCheck()
    //{
    //    if (GameManager.Instance.QuestManager.CompletedQuests.Contains(winQuest))
    //    {
    //        SceneManager.LoadScene("Victory");
    //    }
    //}
}