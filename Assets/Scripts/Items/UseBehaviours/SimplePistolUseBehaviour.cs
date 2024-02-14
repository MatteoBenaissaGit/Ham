using Character;
using Items.Props.Projectile;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public class SimplePistolUseBehaviour : ItemUseBehaviour
    {
        public SimplePistolUseBehaviour(ItemController item) : base(item)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            
        }

        public override void Quit()
        {
        }

        public override void AimBehaviour(bool doAim)
        {
            MakeCameraAim(doAim);
            CharacterController.Instance.OnCharacterAction.Invoke(doAim ? CharacterGameplayAction.Aim : CharacterGameplayAction.StopAim);
        }

        public override void AimStayBehaviour()
        {
            
        }

        public override void ShootBehaviour()
        {
            
        }

        private const float NoRaycastHitLaunchDistance = 45f;
        public override void ShootOnceBehaviour()
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