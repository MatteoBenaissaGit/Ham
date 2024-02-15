using System;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public abstract class ItemUseBehaviour
    {
        public ItemController Item { get; private set; }

        public ItemUseBehaviour(ItemController item)
        {
            Item = item;
        }

        public abstract void Initialize();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void Quit();
        public abstract void AimBehaviour(bool doAim);
        public abstract void AimStayBehaviour();
        public abstract void ShootBehaviour(bool isShooting);
        public abstract void ShootOnceBehaviour();

        protected void MakeCameraAim(bool doAim)
        {
            CharacterController characterController = Character.CharacterController.Instance;
            characterController.UI.Aim.Set(doAim);

            if (doAim)
            {
                characterController.CameraController.SetCameraAfterCurrent(characterController.CameraController.Data.AimCamera);
            }
            else
            {
                characterController.CameraController.EndCurrentCameraState(true);
            }

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