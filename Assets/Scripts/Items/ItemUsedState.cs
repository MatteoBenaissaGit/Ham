using System;
using Data.Items;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items
{
    public class ItemUsedState : ItemBaseState
    {
        private bool _isActive;
        private bool _isAiming;
        
        public ItemUsedState(ItemController controller) : base(controller)
        {
        }
        
        /// <summary>
        /// This method is called when the used state is entered
        /// </summary>
        public override void Enter()
        {
            switch (Controller.Data.Type)
            {
                case ItemType.None:
                    Controller.transform.parent = Character.CharacterController.Instance.transform.transform;
                    break;
                case ItemType.SimplePistol:
                    Controller.transform.parent = CharacterController.Instance.GunIK;
                    break;
                case ItemType.ZipLine:
                    Controller.transform.parent = CharacterController.Instance.GunIK;
                    break;
                default:
                    Controller.transform.parent = Character.CharacterController.Instance.transform.transform;
                    break;
            }
            
            Controller.transform.localPosition = Vector3.zero;
            Controller.transform.localRotation = Quaternion.identity;
            
            CharacterController.Instance.Input.ItemInput.OnAim += Aim;
            CharacterController.Instance.Input.ItemInput.OnShootOnce += Shoot;
        }

        public override void Update()
        {
            if (_isAiming && _isActive)
            {
                AimStay();
            }
        }

        public override void FixedUpdate()
        {
        }
        
        public override void OnTriggerEnter(Collider collider)
        {
            
        }

        /// <summary>
        /// This method is called when the used state is exited
        /// </summary>
        public override void Exit()
        {
            Controller.transform.parent = null;
            Character.CharacterController character = Character.CharacterController.Instance;
            Controller.transform.up = character.transform.up;
            Controller.transform.position += character.Mesh.transform.forward * 2 + character.transform.up;
            
            CharacterController.Instance.Input.ItemInput.OnAim -= Aim;
            CharacterController.Instance.Input.ItemInput.OnShootOnce -= Shoot;
        }

        /// <summary>
        /// Set the item in an active state of use
        /// </summary>
        /// <param name="isActive">is the item active</param>
        public virtual void SetActive(bool isActive)
        {
            _isActive = isActive;
            
            Controller.SetActiveBehaviour?.SetItemActive(_isActive);
        }
        
        private void Aim(bool doAim)
        {
            if (_isActive == false)
            {
                return;
            }
            
            if (_isAiming == doAim)
            {
                return;
            }
            
            _isAiming = doAim;
            Controller.AimBehaviour?.Aim(doAim);
        }

        private void AimStay()
        {
            if (_isActive == false)
            {
                return;
            }
            
            Controller.AimBehaviour?.AimStay();
        }

        private void Shoot()
        {
            if (_isActive == false)
            {
                return;
            }
            
            if (_isAiming == false)
            {
                return;
            }
            
            Controller.AimBehaviour?.Shoot();
        }
    }
}