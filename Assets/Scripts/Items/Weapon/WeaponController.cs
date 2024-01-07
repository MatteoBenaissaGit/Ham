using System;

namespace Items.Weapon
{
    public class WeaponController : ItemController
    {
        protected override void Awake()
        {
            Initialize(new WeaponFloatingState(this), new WeaponUsedState(this));
        }
    }
}