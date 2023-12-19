using System;
using UnityEditor;
using UnityEngine;

namespace Camera
{
    public class CameraFollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private float _positionSmoothSpeed = 10f;
        [SerializeField] private float _rotationSmoothSpeed = 50f;

        private float _positionDistance;
        private float _height;
        private Vector3 _baseRotation;

        private void Awake()
        {
            _positionDistance = Vector3.Distance(transform.position,_cameraTarget.position);
            _height = Mathf.Abs(transform.position.y - _cameraTarget.position.y);
            _baseRotation = transform.rotation.eulerAngles;
        }

        private void Update()
        {
            Vector3 desiredPosition = _cameraTarget.position + (-_cameraTarget.forward * _positionDistance) + _cameraTarget.up * _height;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _positionSmoothSpeed);

            Quaternion desiredRotation = Quaternion.Euler(_cameraTarget.rotation.eulerAngles + _baseRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _rotationSmoothSpeed);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(_cameraTarget.position, _cameraTarget.up, _positionDistance);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0,_height,0));
        }

#endif
    }
}
