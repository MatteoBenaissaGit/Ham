using System;
using System.Collections.Generic;
using Camera;
using Data.Character;
using Gravity;
using Inputs;
using Unity.Mathematics;
using UnityEngine;

namespace Character
{
    public enum CharacterGameplayAction
    {
        Walk = 0,
        StopWalk = 1,
        Jump = 2,
        Fall = 3,
        Land = 4
    }
    
    public class CharacterGameplayData
    {
        public bool IsGrounded { get; set; }
    }
    
    public class CharacterController : MonoBehaviour
    {
        [field:SerializeField] public CharacterControllerData Data { get; private set; }
        [field:SerializeField] public GravityBody GravityBody { get; private set; }
        [field:SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field:SerializeField] public Transform Mesh { get; private set; }
        [field:SerializeField] public CameraController CameraController { get; private set; }  
        
        public CharacterStateManager StateManager { get; private set; }
        public InputManager Input { get; private set; }
        public CharacterGameplayData GameplayData { get; private set; }
        public Action<CharacterGameplayAction> OnCharacterAction { get; set; }

        #region MonoBehaviour methods

        private void Awake()
        {
            GameplayData = new CharacterGameplayData();
            
            Input = new InputManager();
            Input.Initialize();
            
            StateManager = new CharacterStateManager();
            StateManager.Initialize(this);
        }

        private void Update()
        {
            StateManager.UpdateState();
            
            MakeMeshRotationFollowInputs();
        }

        private void FixedUpdate()
        {
            StateManager.FixedUpdateState();
        }

        private void OnCollisionEnter(Collision collision)
        {
            StateManager.CurrentState.OnColliderEnter(collision);
        }

        private void OnDisable()
        {
            Input.Disable();
        }

        #endregion

        /// <summary>
        /// This method rotate the mesh toward where he is moving depending on inputs
        /// </summary>
        private void MakeMeshRotationFollowInputs()
        {
            if (Input.CharacterControllerInput.IsMovingHorizontalOrVertical() == false)
            {
                return;
            }

            Quaternion baseLocalRotation = Mesh.localRotation;
            Mesh.LookAt(Rigidbody.transform.position + GetCameraRelativeInputDirection());
            Vector3 lookAtRotation = Mesh.localRotation.eulerAngles;
            lookAtRotation.x = 0;
            lookAtRotation.z = 0;
            Mesh.localRotation = baseLocalRotation;
            Quaternion desiredRotation = Quaternion.Euler(lookAtRotation);
            Mesh.localRotation = Quaternion.Slerp(baseLocalRotation, desiredRotation, Data.WalkRotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Create and get the result of a raycast hit toward the ground from the character's feet
        /// </summary>
        /// <returns>The RaycastHit of the raycast</returns>
        public RaycastHit GetRaycastTowardGround()
        {
            float raycastDistance = Data.RaycastTowardGroundToDetectFallDistance;
            Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance);
            return hit;
        }

        /// <summary>
        /// This method return the input direction relative to the camera
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCameraRelativeInputDirection()
        {
            CharacterControllerInput input = Input.CharacterControllerInput;
            Vector2 movementInput = new Vector2(input.HorizontalMovement, input.VerticalMovement).normalized;
            Vector3 inputDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
            Vector3 localDirection = CameraController.CameraTarget.transform.TransformDirection(inputDirection);

            return localDirection;
        }

#if UNITY_EDITOR

        private Dictionary<Vector3, JumpState> _jumpGizmos = new Dictionary<Vector3, JumpState>();
        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false)
            {
                return;
            }
            
            //ground detection
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (-transform.up * Data.RaycastTowardGroundToDetectFallDistance));
            
            //gravity direction
            if (GravityBody != null && GravityBody.GravityDirection != Vector3.zero)
            {
                Gizmos.color = new Color(0f, 0.86f, 1f);
                Gizmos.DrawLine(transform.position, transform.position + (-GravityBody.GravityDirection * 5));
            }
            
            //movement direction
            Gizmos.color = Color.green;
            Vector3 directionEndPoint = Rigidbody.transform.position + GetCameraRelativeInputDirection() * 3;
            Gizmos.DrawLine(Rigidbody.transform.position, directionEndPoint);
            Gizmos.DrawSphere(directionEndPoint, 0.25f);
            
            //jump
            CharacterJumpState jump = StateManager.JumpState;
            if (StateManager.CurrentState == jump)
            {
                Vector3 position = Rigidbody.transform.position;
                if (_jumpGizmos.ContainsKey(position) == false)
                {
                    _jumpGizmos.Add(Rigidbody.transform.position, jump.CurrentJumpState);
                }
            }
            else if (_jumpGizmos.Count > 200)
            {
                _jumpGizmos.Clear();
            }
            foreach (var jumpPosition in _jumpGizmos)
            {
                Gizmos.color = jumpPosition.Value switch
                {
                    JumpState.Up => new Color(0f, 1f, 0f, 0.74f),
                    JumpState.Apex => new Color(1f, 0.92f, 0.02f, 0.56f),
                    JumpState.Down => new Color(1f, 0f, 0f, 0.33f),
                    _ => throw new Exception()
                };
                Gizmos.DrawSphere(jumpPosition.Key,0.25f);
            }
        }

#endif
    }
}
