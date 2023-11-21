using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
   [SerializeField] private float destroyTime = 2.5f;

    void Start()
    {
        // Destruye el objeto despu�s del tiempo especificado
        Destroy(gameObject, destroyTime);
    }
}
