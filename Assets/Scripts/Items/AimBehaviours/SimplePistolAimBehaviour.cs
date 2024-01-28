using Character;

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
            
            //TODO make character face direction
        }

        public override void Shoot()
        {
            
        }
    }
}