using System;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private PlayerModel playerModel;

        private void Start()
        {
            inputController.PrimaryFireEventStarted += Attack;
        }

        private void Update()
        {
            playerModel.Move(inputController.Movement);
            playerModel.LookAt((inputController.MousePosition - (Vector2)transform.position).normalized);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerModel.InteractableGo != null)
                {
                    playerModel.InteractableGo.Interaction();
                }
            }
        }

        private void Attack()
        {
            playerModel.Attack();
        }
    }
}