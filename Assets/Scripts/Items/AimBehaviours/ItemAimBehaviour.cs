using System;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.AimBehaviours
{
    public abstract class ItemAimBehaviour
    {
        public ItemController Item { get; private set; }

        public ItemAimBehaviour(ItemController item)
        {
            Item = item;
        }

        public abstract void Aim(bool doAim);
        public abstract void AimStay();
        public abstract void Shoot();
        public abstract void ShootOnce();

        protected void MakeCameraAim(bool doAim)
        {
            CharacterController characterController = Character.CharacterController.Instance;
            characterController.UI.Aim.Set(doAim);
            
            characterController.CameraController.SetCamera(
                doAim ? characterController.CameraController.Data.AimCamera : characterController.CameraController.Data.BaseCamera);

            characterController.GameplayData.IsLookingTowardCameraAim = doAim;
        }

        private RaycastHit[] _hits = new RaycastHit[10];
        protected RaycastHit? GetClosestRaycastHitTowardCamera(Action actionIfNoHit = null)
        {
            Vector3 origin = CharacterController.Instance.CameraController.Camera.transform.position;
            Vector3 direction = CharacterController.Instance.CameraController.Camera.transform.forward;
            
            int hitAmount = Physics.RaycastNonAlloc(origin, direction, _hits);

            RaycastHit closestHit = default;
            float closestHitDistance = float.MaxValue;
            bool didHit = false;

            for (int i = hitAmount - 1; i >= 0; i--)
            {
                if (_hits[i].collider == null || _hits[i].collider.isTrigger)
                {
                    continue;
                }

                float distance = Vector3.Distance(CharacterController.Instance.CameraController.Camera.transform.position, _hits[i].point);
                if (distance > closestHitDistance)
                {
                    continue;
                }

                closestHitDistance = distance;
                closestHit = _hits[i];
                didHit = true;
            }

            if (didHit == false)
            {
                actionIfNoHit?.Invoke();
                return null;
            }

            return closestHit;
        }

    }
}