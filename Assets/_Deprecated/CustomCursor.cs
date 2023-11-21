using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Transform mCursorVisual;
    [SerializeField] private Vector3 mDisplacement;
    [SerializeField] private InputController inputController;


    void Start()
    {
        // this sets the base cursor as invisible
        //Cursor.visible = false;
    }

    void Update()
    {
        // mCursorVisual.position = (Vector3)inputController.MousePosition + mDisplacement;
        // mCursorVisual.position = Input.mousePosition + mDisplacement;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}