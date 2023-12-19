using System;
using Unity.Mathematics;
using UnityEngine;

namespace Camera
{
    public class CameraMoveWithInput : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 40f;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Character.CharacterController _characterController;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            float rotationInput = _characterController.Input.CameraMovementInput.CameraXMovement * _rotationSpeed * Time.deltaTime;
            _cameraTarget.Rotate(_characterController.Rigidbody.transform.up, rotationInput);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_cameraTarget.transform.position, _cameraTarget.transform.position + _characterController.Rigidbody.transform.up * 2);
            Gizmos.DrawLine(_cameraTarget.transform.position, _characterController.Camera.transform.position);
        }

#endif
    }
}