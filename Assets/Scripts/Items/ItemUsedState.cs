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
        private bool _needToAimToShoot = true;
        
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
                    Controller.transform.parent = CharacterController.Instance.transform.transform;
                    break;
                case ItemType.SimplePistol:
                    Controller.transform.parent = CharacterController.Instance.GunIK;
                    break;
                case ItemType.ZipLine:
                    Controller.transform.parent = CharacterController.Instance.GunIK;
                    break;
                case ItemType.Jetpack:
                    Controller.transform.parent = CharacterController.Instance.BackpackIK;
                    _needToAimToShoot = false;
                    break;
                default:
                    Controller.transform.parent = CharacterController.Instance.transform.transform;
                    break;
            }
            
            Controller.transform.localPosition = Vector3.zero;
            Controller.transform.localRotation = Quaternion.identity;
            
            CharacterController.Instance.Input.ItemInput.OnAim += Aim;
            CharacterController.Instance.Input.ItemInput.OnShoot += Shoot;
            CharacterController.Instance.Input.ItemInput.OnShootOnce += ShootOnce;
            
            Controller.UseBehaviour?.Initialize();
            
            Controller.UsedMesh.SetActive(_isActive);
        }

        public override void Update()
        {
            if (_isAiming && _isActive)
            {
                AimStay();
            }
            
            Controller.UseBehaviour?.Update();
        }

        public override void FixedUpdate()
        {
            Controller.UseBehaviour?.FixedUpdate();
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
            CharacterController.Instance.Input.ItemInput.OnShoot -= Shoot;
            CharacterController.Instance.Input.ItemInput.OnShootOnce -= ShootOnce;
            
            Controller.UseBehaviour?.Quit();
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

        public void Aim(bool doAim)
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
            Controller.UseBehaviour?.AimBehaviour(doAim);
        }

        private void AimStay()
        {
            if (_isActive == false)
            {
                return;
            }
            
            Controller.UseBehaviour?.AimStayBehaviour();
        }

        public void ShootOnce()
        {
            if (_isActive == false)
            {
                return;
            }
            
            if (_isAiming == false && _needToAimToShoot)
            {
                return;
            }
            
            Controller.UseBehaviour?.ShootOnceBehaviour();
        }

        public void Shoot(bool isShooting)
        {
            if (_isActive == false)
            {
                return;
            }
            
            if (_isAiming == false && _needToAimToShoot)
            {
                return;
            }
            
            Controller.UseBehaviour?.ShootBehaviour(isShooting);
        }
    }
}