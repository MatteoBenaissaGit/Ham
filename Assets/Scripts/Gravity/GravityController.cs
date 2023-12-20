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
        
        [SerializeField] private Character.CharacterController _characterController;

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
            
            Transform rigidbodyTransform = _characterController.Rigidbody.transform;
            
            GravityOrbitUp = 
                Orbit.Data.FixedDirection ? 
                    Orbit.transform.up : 
                    (rigidbodyTransform.position - Orbit.transform.position).normalized;

            //rigidbodyTransform.up = Vector3.Lerp(rigidbodyTransform.up, GravityOrbitUp, Orbit.Data.RotationSpeed);
            rigidbodyTransform.up = GravityOrbitUp;

            Vector3 forceToAdd = -GravityOrbitUp * (Orbit.Data.GravityForce * _characterController.Rigidbody.mass);
            Vector3 localForce = _characterController.Rigidbody.transform.InverseTransformDirection(forceToAdd);
            localForce.x = 0;
            localForce.z = 0;
            _characterController.Rigidbody.AddForce(localForce);
        }
    }
}