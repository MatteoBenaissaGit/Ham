using UnityEngine;

namespace Items.Weapon
{
    public class WeaponFloatingState : ItemFloatingState
    {
        public WeaponFloatingState(ItemController controller) : base(controller)
        {
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            MakeFloatingMeshFloatOnAboveGround();
        }

        public override void Exit()
        {
        }
    }
}