using Character;

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

        protected void MakeCameraAim(bool doAim)
        {
            CharacterController characterController = Character.CharacterController.Instance;
            characterController.UI.Aim.Set(doAim);
            
            characterController.CameraController.SetCamera(
                doAim ? characterController.CameraController.Data.AimCamera : characterController.CameraController.Data.BaseCamera);

            characterController.GameplayData.IsLookingTowardCameraAim = doAim;
        }
    }
}