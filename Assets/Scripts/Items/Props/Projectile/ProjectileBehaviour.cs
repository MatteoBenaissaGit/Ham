using UnityEngine;

namespace Items.Props.Projectile
{
    public abstract class ProjectileBehaviour
    {
        protected Projectile Projectile { get; set; }

        public ProjectileBehaviour(Projectile projectile)
        {
            Projectile = projectile;
        }
        
        public abstract void Launch();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnColliderEnter(Collider collider);
        public abstract void OnColliderStay(Collider collider);
    }
}