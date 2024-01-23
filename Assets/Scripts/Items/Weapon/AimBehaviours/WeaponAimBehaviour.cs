namespace Items.Weapon.AimBehaviours
{
    public abstract class WeaponAimBehaviour
    {
        public WeaponController Weapon { get; private set; }

        public WeaponAimBehaviour(WeaponController weapon)
        {
            Weapon = weapon;
        }

        public abstract void Aim(bool doAim);
        public abstract void Shoot();
    }
}