using DG.Tweening;
using UnityEngine;

namespace Items.Props.Projectile
{
    /// <summary>
    /// This class handle the behaviour of the fruit projectile
    /// </summary>
    public class ProjectileFruitBehaviour : ProjectileBehaviour
    {
        private const float MeshRotationSpeed = 10f;
        private const float TimeBeforeFreezing = 3f;
        private const float TimeBeforeDestroying = 8f;

        private float _timeOnColliderStay;
        private bool _froze;

        public ProjectileFruitBehaviour(Projectile projectile) : base(projectile)
        {
        }
        
        public override void Launch(Vector3? target = null)
        {
            float speedMultiplier = target == null
                ? Projectile.Data.SpeedMultiplierIfNoRaycastHit
                : Vector3.Distance(Projectile.transform.position, (Vector3)target) * Projectile.Data.SpeedMultiplierPerDistance;
            speedMultiplier = Mathf.Clamp(speedMultiplier, 1, Projectile.Data.MaxSpeedMultiplierPerDistance);
            //Debug.Log($"speed multiplier = {speedMultiplier}");
            
            Projectile.Rigidbody.AddForce(Projectile.transform.forward * Projectile.Data.Speed * speedMultiplier, ForceMode.Impulse);

            Projectile.Mesh.DOComplete();
            Projectile.Mesh.DOPunchScale(Vector3.one * 0.5f, 0.3f);
        }

        public override void Update()
        {
            if (_timeOnColliderStay > TimeBeforeFreezing && _froze == false)
            {
                _froze = true;
                Projectile.Rigidbody.isKinematic = true;
                Projectile.Collider.enabled = false;
            }

            if (_froze)
            {
                _timeOnColliderStay += Time.deltaTime;
            }

            if (_timeOnColliderStay > TimeBeforeDestroying)
            {
                Destroy();
            }
        }

        public override void FixedUpdate()
        {
            MakeMeshRotateWithVelocity();
        }

        public override void OnColliderEnter(Collider collider)
        {
        }

        public override void OnColliderStay(Collider collider)
        {
            _timeOnColliderStay += Time.deltaTime;
        }

        public override void Destroy()
        {
            Projectile.IsDestroyed = true;
            
            Projectile.transform.DOKill();
            Projectile.transform.DOScale(Vector3.zero, 1f);
            Object.Destroy(Projectile.gameObject, 1f);
        }
        
        /// <summary>
        /// This method rotate the mesh of the projectile following its velocity
        /// </summary>
        private void MakeMeshRotateWithVelocity()
        {
            Vector3 velocity = Projectile.Rigidbody.velocity;

            if (velocity.magnitude <= 0.1f)
            {
                return;
            }

            Vector3 baseForward = Projectile.Mesh.forward;
            Projectile.Mesh.forward = velocity;
            Vector3 projectileRight = Projectile.Mesh.right;
            Projectile.Mesh.forward = baseForward;
            Projectile.Mesh.Rotate(projectileRight, velocity.magnitude * MeshRotationSpeed);
        }
    }
}