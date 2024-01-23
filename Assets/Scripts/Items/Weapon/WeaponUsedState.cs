using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.Weapon
{
    public class WeaponUsedState : ItemUsedState
    {
        private WeaponController _weapon;
        private bool _isAiming;
        
        public WeaponUsedState(ItemController controller) : base(controller)
        {
            _weapon = Controller as WeaponController;
        }

        public override void Enter()
        {
            Controller.transform.parent = CharacterController.Instance.GunIK;
            Controller.transform.localPosition = Vector3.zero;
            Controller.transform.localRotation = Quaternion.identity;
            
            CharacterController.Instance.Input.ItemInput.OnAim += Aim;
            CharacterController.Instance.Input.ItemInput.OnShootOnce += Shoot;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            
            Controller.gameObject.SetActive(isActive);
        }

        public override void Exit()
        {
            base.Exit();
            
            CharacterController.Instance.Input.ItemInput.OnAim -= Aim;
            CharacterController.Instance.Input.ItemInput.OnShootOnce -= Shoot;
        }

        private void Aim(bool doAim)
        {
            if (_isAiming == doAim)
            {
                return;
            }
            
            _isAiming = doAim;
            _weapon.AimBehaviour.Aim(doAim);
        }

        private void Shoot()
        {
            if (_isAiming == false)
            {
                return;
            }
            
            _weapon.AimBehaviour.Shoot();
        }
    }
}