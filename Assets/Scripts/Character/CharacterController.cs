using System;
using Camera;
using Data.Character;
using Gravity;
using Inputs;
using Unity.Mathematics;
using UnityEngine;

namespace Character
{
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
        [field:SerializeField] public Animator Animator { get; private set; }
        [field:SerializeField] public CameraController CameraController { get; private set; }  
        
        public CharacterStateManager StateManager { get; private set; }
        public InputManager Input { get; private set; }
        public CharacterGameplayData GameplayData { get; private set; }

        private GameObject _cameraRelativeDirection;

        #region MonoBehaviour methods

        private void Awake()
        {
            GameplayData = new CharacterGameplayData();
            
            Input = new InputManager();
            Input.Initialize();
            
            StateManager = new CharacterStateManager();
            StateManager.Initialize(this);

            _cameraRelativeDirection = new GameObject("CameraRelativeDirection");
            _cameraRelativeDirection.transform.parent = gameObject.transform;
            _cameraRelativeDirection.transform.localPosition = Vector3.zero;
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

            Vector3 cameraRotationEuler = CameraController.Camera.transform.rotation.eulerAngles;
            _cameraRelativeDirection.transform.rotation = Quaternion.Euler(
                new Vector3(Rigidbody.transform.rotation.eulerAngles.x,cameraRotationEuler.y,cameraRotationEuler.z));
            //Vector3 localDirection = _cameraRelativeDirection.transform.TransformDirection(inputDirection);
            Vector3 localDirection = CameraController.Camera.transform.TransformDirection(inputDirection);

            return localDirection;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (-transform.up * Data.RaycastTowardGroundToDetectFallDistance));

            if (GravityBody != null && GravityBody.GravityDirection != Vector3.zero)
            {
                Gizmos.color = new Color(0f, 0.86f, 1f);
                Gizmos.DrawLine(transform.position, transform.position + (-GravityBody.GravityDirection * 5));
            }
        }

#endif
    }
}
