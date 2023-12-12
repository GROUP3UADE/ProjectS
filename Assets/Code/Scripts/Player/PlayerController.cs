using System;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        #region Serializables

        [SerializeField] 
        private InputController inputController;
        [SerializeField] 
        private PlayerModel playerModel;
        [SerializeField]
        private WeaponPivotBehaviour _weaponController;

        #endregion

        #region Miembros privados

        private Vector2 _lookDir;

        #endregion


        private void Start()
        {
            inputController.PrimaryFireEventStarted += Attack;
        }

        private void Update()
        {
            _lookDir = (inputController.MousePosition - (Vector2)transform.position).normalized;
            print(_lookDir);
            playerModel.Move(inputController.Movement);
            playerModel.LookAt(_lookDir);
            
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

        public void EquipWeapon(GameObject w) => _weaponController.EquipWeapon(w);
    }
}