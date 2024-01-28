using System;
using Unity.Mathematics;
using UnityEngine;

namespace Camera
{
    public class CameraMoveWithInput : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Character.CharacterController _characterController;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 localRotation = _cameraTarget.transform.localRotation.eulerAngles;
            
            //x movement
            float rotationInputX = _characterController.Input.CameraMovementInput.CameraXMovement 
                                  * _characterController.CameraController.Data.CameraXMovementSpeed 
                                  * _characterController.CameraController.CurrentCameraInformation.RotationSpeedMultiplier.x
                                  * Time.deltaTime;
            localRotation.y += rotationInputX;
            
            //y movement
            float rotationInputY = _characterController.Input.CameraMovementInput.CameraYMovement 
                                   * _characterController.CameraController.Data.CameraYMovementSpeed 
                                   * _characterController.CameraController.CurrentCameraInformation.RotationSpeedMultiplier.y
                                   * Time.deltaTime;
            localRotation.x += rotationInputY;
            
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