using System;
using Items.Weapon.AimBehaviours;
using UnityEngine;

namespace Items.Weapon
{
    public enum WeaponType
    {
        None = 0,
        FruitPistol = 1
    }
    
    public class WeaponController : ItemController
    {
        [field:SerializeField] public WeaponType Type { get; private set; }
        [field:SerializeField] public Transform GunIK { get; private set; }
        [field:SerializeField] public Transform Muzzle { get; private set; }
        
        public WeaponAimBehaviour AimBehaviour { get; private set; }
        
        protected override void Awake()
        {
            switch (Type)
            {
                case WeaponType.None:
                    throw new Exception("no type provided");
                    break;
                case WeaponType.FruitPistol:
                    AimBehaviour = new SimplePistolAimBehaviour(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Initialize(new WeaponFloatingState(this), new WeaponUsedState(this));
        }
    }
}