using System;
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
            _cameraTarget.Rotate(Vector3.up, _characterController.Input.CameraMovementInput.CameraXMovement * _rotationSpeed * Time.deltaTime);
        }
    }
}