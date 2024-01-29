using UnityEngine;

namespace Items.Props.Projectile
{
    public class ProjectileFruitBehaviour : ProjectileBehaviour
    {
        public ProjectileFruitBehaviour(Projectile projectile) : base(projectile)
        {
        }
        
        public override void Launch()
        {
            Projectile.Rigidbody.AddForce(Projectile.Speed * Projectile.Forward, ForceMode.Impulse);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnColliderEnter(Collider collider)
        {
        }

        public override void OnColliderStay(Collider collider)
        {
            
        }
    }
}