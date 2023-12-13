using System;
using UnityEngine;

namespace Gravity
{
    /// <summary>
    /// This class handle the gravity of an rigidbody in a gravity orbit
    /// </summary>
    public class GravityController : MonoBehaviour
    {
        public bool ApplyOrbitGravity { get; set; } = true;
        public GravityOrbit Orbit { get; set; }
        
        [SerializeField] private Rigidbody _rigidbody;

        private void FixedUpdate()
        {
            if (ApplyOrbitGravity == false || Orbit == null)
            {
                return;
            }
            
            Transform rigidbodyTransform = _rigidbody.transform;
            
            Vector3 gravityOrbitUp = 
                Orbit.Data.FixedDirection ? 
                    Orbit.transform.up : 
                    (rigidbodyTransform.position - Orbit.transform.position).normalized;

            rigidbodyTransform.up = Vector3.Lerp(rigidbodyTransform.up, gravityOrbitUp, Orbit.Data.RotationSpeed);
            
            _rigidbody.AddForce(-gravityOrbitUp * (Orbit.Data.GravityForce * _rigidbody.mass));
        }
    }
}