using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private InputController _inputController;

    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _midPointObject;

    // algo
    [Range(0f, 10f)][SerializeField] private float followSpeed;

    [Range(0f, 10f)][SerializeField] private float maxMouseDistanceFromPlayer;
    [Range(0f, 10f)][SerializeField] private float minMouseDistanceFromPlayer;

    private Vector3 _midPoint;

    private void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<CharacterController>().transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 startPosition = transform.position;

        var distanceFromPlayer = Vector2.Distance(_inputController.MousePosition, character.transform.position);

        var mouseDirection = (_inputController.MousePosition - (Vector2)character.transform.position).normalized;

        //Debug.Log(mouseDirection);
        Vector2 followPosition = new Vector2();

        if (distanceFromPlayer <= minMouseDistanceFromPlayer) //Snap directly to player
        {
            followPosition = character.transform.position;
        }
        else if (distanceFromPlayer > maxMouseDistanceFromPlayer) //Don't go any further
        {
            followPosition = (Vector2)character.transform.position + (mouseDirection * maxMouseDistanceFromPlayer);
        }
        else
        {
            followPosition = _inputController.MousePosition;
        }

        // Debug.Log($"distance between player and mouse:{distanceFromPlayer}");

        _midPoint = (Vector3)(followPosition + (Vector2)character.transform.position) / 2f;
        _midPoint.z = -10;
        _midPointObject.transform.position = _midPoint;  // new

        var newPosition = (Vector3)Vector2.Lerp(startPosition, _midPoint,
            followSpeed * Time.deltaTime);

        // _rb.velocity = (newPosition - transform.position).normalized * (followSpeed * 1000) ; // new

        newPosition.z = -10;

        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_midPoint, .1f);
    }
}