using Character;

namespace Items.Weapon.AimBehaviours
{
    public class SimplePistolAimBehaviour : WeaponAimBehaviour
    {
        public SimplePistolAimBehaviour(WeaponController weapon) : base(weapon)
        {
        }

        public override void Aim(bool doAim)
        {
            CharacterController characterController = Character.CharacterController.Instance;
            characterController.UI.Aim.Set(doAim);
            
            characterController.CameraController.SetCamera(
                doAim ? characterController.CameraController.Data.AimCamera : characterController.CameraController.Data.BaseCamera);
            
            //TODO make character face direction
        }

        public override void Shoot()
        {
            
        }
    }
}