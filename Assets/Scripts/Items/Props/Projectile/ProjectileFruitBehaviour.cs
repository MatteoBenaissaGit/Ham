using DG.Tweening;
using UnityEngine;

namespace Items.Props.Projectile
{
    /// <summary>
    /// This class handle the behaviour of the fruit projectile
    /// </summary>
    public class ProjectileFruitBehaviour : ProjectileBehaviour
    {
        private const float MeshRotationSpeed = 2f;
        private const float TimeBeforeFreezing = 2f;
        private const float TimeBeforeDestroying = 3f;

        private float _timeOnColliderStay;
        private bool _froze;

        public ProjectileFruitBehaviour(Projectile projectile) : base(projectile)
        {
        }
        
        public override void Launch()
        {
            Projectile.Rigidbody.AddForce(Projectile.Speed * Projectile.Forward, ForceMode.Impulse);

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
            Vector3 velocity = Projectile.Rigidbody.velocity;

            if (velocity.magnitude <= 0.1f)
            {
                return;
            }

            Projectile.Mesh.forward = velocity;
            Projectile.Mesh.Rotate(Projectile.Mesh.right, velocity.magnitude * MeshRotationSpeed);
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
    }
}