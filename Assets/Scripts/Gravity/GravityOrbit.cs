using System;
using Data.Gravity;
using UnityEngine;

namespace Gravity
{
    /// <summary>
    /// This class handle the gravity management of an orbit
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GravityOrbit : MonoBehaviour
    {
        [field:SerializeField] public GravityOrbitData Data { get; private set; }

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GravityController gravityController) == false)
            {
                return;
            }

            gravityController.Orbit = this;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GravityController gravityController) == false
                || gravityController.Orbit != this)
            {
                return;
            }

            gravityController.Orbit = null;
        }
    }
}