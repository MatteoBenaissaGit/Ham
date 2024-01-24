using System;
using Unity.Mathematics;
using UnityEngine;

namespace Camera
{
    public class CameraMoveWithInput : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Character.CharacterController _characterController;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            float rotationInput = _characterController.Input.CameraMovementInput.CameraXMovement * _rotationSpeed * Time.deltaTime;
            Vector3 localRotation = _cameraTarget.transform.localRotation.eulerAngles;
            localRotation.y += rotationInput;
            _cameraTarget.localRotation = Quaternion.Euler(localRotation);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 upPoint = _cameraTarget.transform.position + _cameraTarget.transform.up * 2;
            Gizmos.DrawLine(_cameraTarget.transform.position, upPoint);
            Gizmos.DrawLine(_cameraTarget.transform.position, _characterController.CameraController.Camera.transform.position);
            Gizmos.DrawLine(upPoint, _characterController.CameraController.Camera.transform.position);
        }

#endif
    }
}