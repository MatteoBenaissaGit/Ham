using System;
using UnityEngine;

namespace Gravity
{
    /// <summary>
    /// This class handle the gravity of an rigidbody in a gravity orbit
    /// </summary>
    public class GravityController : MonoBehaviour
    {
        public bool ApplyOrbitGravity { get; set; }
        public GravityOrbit Orbit { get; set; }
        public Vector3 GravityOrbitUp { get; private set; }
        
        [SerializeField] private Rigidbody _rigidbody;

        private void Awake()
        {
            ApplyOrbitGravity = true;
        }

        private void FixedUpdate()
        {
            if (ApplyOrbitGravity == false || Orbit == null)
            {
                return;
            }
            
            Transform rigidbodyTransform = _rigidbody.transform;
            
            GravityOrbitUp = 
                Orbit.Data.FixedDirection ? 
                    Orbit.transform.up : 
                    (rigidbodyTransform.position - Orbit.transform.position).normalized;

            rigidbodyTransform.up = Vector3.Lerp(rigidbodyTransform.up, GravityOrbitUp, Orbit.Data.RotationSpeed);
            
            _rigidbody.AddForce(-rigidbodyTransform.up * (Orbit.Data.GravityForce * _rigidbody.mass));
        }
    }
}