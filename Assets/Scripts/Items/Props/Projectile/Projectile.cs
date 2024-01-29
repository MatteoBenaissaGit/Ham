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
    
    public class Projectile : MonoBehaviour
    {
        [field:SerializeField] public ProjectileType Type { get; private set; }
        [field:SerializeField] public Rigidbody Rigidbody { get; set; }
        [field:SerializeField] public float Speed { get; set; }
        [field:SerializeField] public GravityBody GravityBody { get; set; }
        
        public ProjectileBehaviour Behaviour { get; private set; }
        public Vector3 Forward { get; set; }
        
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
            Behaviour?.Update();
        }

        private void FixedUpdate()
        {
            Behaviour?.FixedUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            Behaviour?.OnColliderEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            Behaviour?.OnColliderStay(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == null)
            {
                return;
            }
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