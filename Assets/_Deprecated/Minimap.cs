using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform icon;

    private void LateUpdate()
    {
        var newPosition = playerRb.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        if (playerRb.velocity.x > 0)
        {
            RotateIcon(-90f);
        }
        if (playerRb.velocity.x < 0)
        {
            RotateIcon(90f);
        }
        if (playerRb.velocity.y < 0)
        {
            RotateIcon(180f);
        }
        if (playerRb.velocity.y > 0)
        {
            RotateIcon(0f);
        }
    }

    private void RotateIcon(float angle)
    {
        icon.transform.rotation = Quaternion.Lerp(icon.transform.rotation,Quaternion.Euler(0,0,angle), 0.2f);
    }
}
