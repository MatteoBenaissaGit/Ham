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
            CharacterController characterController = Character.CharacterController.Instance;
            characterController.UI.Aim.Set(doAim);
            
            characterController.CameraController.SetCamera(
                doAim ? characterController.CameraController.Data.AimCamera : characterController.CameraController.Data.BaseCamera);

            characterController.GameplayData.IsLookingTowardCameraAim = doAim;
        }

        public override void Shoot()
        {
            Projectile projectile = Item.InstantiateGameObject(Item.Projectile.gameObject).GetComponent<Projectile>();
            
            projectile.transform.position = Item.Muzzle.position;
            projectile.transform.forward = Item.Muzzle.forward;
            
            projectile.Launch(Item.Muzzle.forward); //TODO set the forward to raycast result or raycast direction
        }
    }
}