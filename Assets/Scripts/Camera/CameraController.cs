﻿using System;
using Data.Camera;
using DG.Tweening;
using UnityEngine;
using CameraData = Data.Camera.CameraData;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] public CameraData Data { get; private set; }
        [field:SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [field: SerializeField] public CameraFollowTransform FollowTransform { get; private set; }
        [field: SerializeField] public CameraMoveWithInput MoveWithInput { get; private set; }

        public CameraInformation CurrentCameraInformation { get; private set; }
        
        private Vector3 _baseRotationEuler;

        private void Awake()
        {
            _baseRotationEuler = Camera.transform.localRotation.eulerAngles;
            CurrentCameraInformation = Data.BaseCamera;
        }

        /// <summary>
        /// Change the camera fov, offset and rotation
        /// </summary>
        /// <param name="cameraInformation">The camera information to apply</param>
        /// <param name="ease">The fov change ease</param>
        public void SetCamera(CameraInformation cameraInformation, Ease ease = Ease.Flash)
        {
            CurrentCameraInformation = cameraInformation;
            
            Camera.DOKill();
            Camera.transform.DOKill();
            
            SetFov(CurrentCameraInformation.Fov, CurrentCameraInformation.SpeedToChange, ease);    
            SetOffset(CurrentCameraInformation.Offset, CurrentCameraInformation.SpeedToChange, ease);
            SetRotation(CurrentCameraInformation.Rotation, CurrentCameraInformation.SpeedToChange, ease);
        }
        
        /// <summary>
        /// Change the fov of the camera
        /// </summary>
        /// <param name="fov">The fov target</param>
        /// <param name="duration">The time to change it</param>
        /// <param name="ease">The fov change ease</param>
        public void SetFov(float fov, float duration = 0.5f, Ease ease = Ease.Flash)
        {
            Camera.DOFieldOfView(fov, duration).SetEase(ease);
        }

        /// <summary>
        /// Set the camera offset 
        /// </summary>
        /// <param name="offset">the amount of offset as a Vector2</param>
        /// <param name="duration">the offset change time</param>
        /// <param name="ease">The offset change ease</param>
        public void SetOffset(Vector2 offset, float duration = 0.5f, Ease ease = Ease.Flash)
        {
            Camera.transform.DOLocalMove(offset, duration);
        }

        /// <summary>
        /// Set the camera offset 
        /// </summary>
        /// <param name="eulerAngles">the new euler Angles of the camera</param>
        /// <param name="duration">the rotation change time</param>
        /// <param name="ease">The rotation change ease</param>
        public void SetRotation(Vector3 eulerAngles, float duration = 0.5f, Ease ease = Ease.Flash)
        {
            Camera.transform.DOLocalRotate(_baseRotationEuler + eulerAngles, duration);
        }
    }
}