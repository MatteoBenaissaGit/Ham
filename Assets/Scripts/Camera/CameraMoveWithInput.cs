using System;
using Unity.Mathematics;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Camera
{
    public class CameraMoveWithInput : MonoBehaviour
    {
        [SerializeField] private Character.CharacterController _characterController;

        private Transform _cameraTarget;
        private Transform _cameraTargetMovementRelative;
        private float _currentYRotation;
        
        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Start()
        {
            _cameraTarget = CharacterController.Instance.CameraController.CameraTarget;
            _cameraTargetMovementRelative = CharacterController.Instance.CameraController.CameraTargetMovementRelative;
        }

        private void Update()
        {
            Vector3 localRotation = _cameraTarget.transform.localRotation.eulerAngles;
            Vector3 localRotationMovementRelative = _cameraTargetMovementRelative.transform.localRotation.eulerAngles;
            
            //x movement
            float rotationInputX = _characterController.Input.CameraMovementInput.CameraXMovement 
                                  * _characterController.CameraController.Data.CameraXMovementSpeed 
                                  * _characterController.CameraController.CurrentCameraInformation.RotationSpeedMultiplier.x
                                  * Time.deltaTime;
            localRotation.y += rotationInputX;
            
            //movement relative rotation
            localRotationMovementRelative.y += rotationInputX;
            _cameraTargetMovementRelative.localRotation = Quaternion.Euler(localRotationMovementRelative);
            
            //y movement
            float rotationInputY = _characterController.Input.CameraMovementInput.CameraYMovement 
                                   * _characterController.CameraController.Data.CameraYMovementSpeed 
                                   * _characterController.CameraController.CurrentCameraInformation.RotationSpeedMultiplier.y
                                   * Time.deltaTime;
            //clamp y
            if (_currentYRotation + rotationInputY > CharacterController.Instance.CameraController.Data.CameraYClamp.y || 
                _currentYRotation + rotationInputY < CharacterController.Instance.CameraController.Data.CameraYClamp.x )
            {
                rotationInputY = 0;
            }
            _currentYRotation += rotationInputY;
            localRotation.x += rotationInputY;
            
            _cameraTarget.localRotation = Quaternion.Euler(localRotation);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (_cameraTarget == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            Vector3 upPoint = _cameraTarget.transform.position + _cameraTarget.transform.up * 2;
            Gizmos.DrawLine(_cameraTarget.transform.position, upPoint);
            Gizmos.DrawLine(_cameraTarget.transform.position, _characterController.CameraController.Camera.transform.position);
            Gizmos.DrawLine(upPoint, _characterController.CameraController.Camera.transform.position);
        }

#endif
    }
}