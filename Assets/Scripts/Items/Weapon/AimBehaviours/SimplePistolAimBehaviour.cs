namespace Items.Weapon.AimBehaviours
{
    public class SimplePistolAimBehaviour : WeaponAimBehaviour
    {
        public SimplePistolAimBehaviour(WeaponController weapon) : base(weapon)
        {
        }

        public override void Aim(bool doAim)
        {
            Character.CharacterController.Instance.UI.Aim.Set(doAim);
            //TODO move camera + zoom + make character face direction
        }

        public override void Shoot()
        {
            
        }
    }
}