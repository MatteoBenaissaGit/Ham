using System;
using Gravity;
using UnityEngine;

namespace Items.Props.Projectile
{
    public enum ProjectileType
    {
        None = 0,
        Fruit = 1
    }
    
    /// <summary>
    /// This class manage the projectiles 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [field:SerializeField] public ProjectileType Type { get; private set; }
        [field:SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field:SerializeField] public Collider Collider { get; private set; }
        [field:SerializeField] public Transform Mesh { get; private set; }
        [field:SerializeField] public GravityBody GravityBody { get; private set; }
        
        [field:Header("Values")]
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public float TimeBeforeDisappearing { get; private set; }
        
        public ProjectileBehaviour Behaviour { get; private set; }
        public Vector3 Forward { get; private set; }
        public float ExistingTime { get; private set; }
        public bool HasHit { get; private set; }
        public bool IsDestroyed { get; set; }
        
        private void Awake()
        {
            switch (Type)
            {
                case ProjectileType.None:
                    break;
                case ProjectileType.Fruit:
                    Behaviour = new ProjectileFruitBehaviour(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Launch(Vector3 forward)
        {
            Forward = forward;
            Behaviour.Launch();
        }

        private void Update()
        {
            if (IsDestroyed)
            {
                return;
            }
            
            ExistingTime += Time.deltaTime;
            
            if (ExistingTime > TimeBeforeDisappearing && HasHit == false)
            {
                IsDestroyed = true;
                Behaviour?.Destroy();
                return;
            }

            Behaviour?.Update();
        }

        private void FixedUpdate()
        {
            Behaviour?.FixedUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == null)
            {
                return;
            }

            HasHit = true;
            Behaviour?.OnColliderEnter(collision.collider);
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            if (collisionInfo.collider == null)
            {
                return;
            }
            Behaviour?.OnColliderStay(collisionInfo.collider);
        }
    }
}