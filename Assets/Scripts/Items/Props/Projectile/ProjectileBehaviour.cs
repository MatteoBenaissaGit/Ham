using UnityEngine;

namespace Items.Props.Projectile
{
    /// <summary>
    /// This class is parenting all projectile's behaviours
    /// </summary>
    public abstract class ProjectileBehaviour
    {
        protected Projectile Projectile { get; set; }

        public ProjectileBehaviour(Projectile projectile)
        {
            Projectile = projectile;
        }
        
        /// <summary>
        /// This method is used at the launch/creation of the projectile
        /// </summary>
        public abstract void Launch();
       
        public abstract void Update();
       
        public abstract void FixedUpdate();
       
        public abstract void OnColliderEnter(Collider collider);
       
        public abstract void OnColliderStay(Collider collider);
        
        /// <summary>
        /// This method is called to destroy the projectile
        /// </summary>
        public abstract void Destroy();
    }
}