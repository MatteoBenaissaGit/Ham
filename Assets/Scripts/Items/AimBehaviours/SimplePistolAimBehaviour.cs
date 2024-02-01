using Character;
using Items.Props.Projectile;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.AimBehaviours
{
    public class SimplePistolAimBehaviour : ItemAimBehaviour
    {
        public SimplePistolAimBehaviour(ItemController item) : base(item)
        {
        }

        public override void Aim(bool doAim)
        {
            MakeCameraAim(doAim);
            CharacterController.Instance.OnCharacterAction.Invoke(doAim ? CharacterGameplayAction.Aim : CharacterGameplayAction.StopAim);
        }

        public override void AimStay()
        {
            
        }

        public override void Shoot()
        {
            
        }

        private const float NoRaycastHitLaunchDistance = 30f;
        public override void ShootOnce()
        {
            Projectile projectile = Item.InstantiateGameObject(Item.Projectile.gameObject).GetComponent<Projectile>();

            projectile.transform.position = Item.Muzzle.position;
            projectile.transform.forward = Item.Muzzle.forward;

            Transform camera = CharacterController.Instance.CameraController.Camera.transform;
            
            RaycastHit? hit = GetClosestRaycastHitTowardCamera();
            if (hit == null)
            {
                Vector3 cameraForwardAimPosition = camera.position + camera.forward * NoRaycastHitLaunchDistance;
                projectile.Launch(cameraForwardAimPosition);
                return;
            }
            projectile.Launch(hit.Value.point, true);
        }
    }
}