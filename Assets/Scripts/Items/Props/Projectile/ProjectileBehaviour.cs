using UnityEngine;

namespace Items.Props.Projectile
{
    public abstract class ProjectileBehaviour
    {
        public abstract void Launch();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnColliderEnter(Collider collider);
        public abstract void OnColliderStay(Collider collider);
    }
}