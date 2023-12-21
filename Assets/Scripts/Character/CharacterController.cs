using System;
using Data.Character;
using Gravity;
using Inputs;
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
        [field:SerializeField] public UnityEngine.Camera Camera { get; private set; }  
        
        public CharacterStateManager StateManager { get; private set; }
        public InputManager Input { get; private set; }
        public CharacterGameplayData GameplayData { get; private set; }

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
            
            CharacterControllerInput input = Input.CharacterControllerInput;
            float horizontalInput = input.HorizontalMovement;
            float verticalInput = input.VerticalMovement;

            Vector3 cameraForward = Camera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 localForward = Camera.transform.forward * verticalInput;
            Vector3 localSide = Camera.transform.right * horizontalInput;
            Vector3 localMoveDirection = (localForward + localSide).normalized;

            if (localMoveDirection.magnitude > 0.1f)
            {
                Quaternion rightDirection = Quaternion.Euler(0f, localMoveDirection.x * (1 * Time.fixedDeltaTime), 0f);
                Quaternion newRotation = Quaternion.Slerp(Rigidbody.rotation, Rigidbody.rotation * rightDirection, Time.fixedDeltaTime * 3f);;
                Rigidbody.MoveRotation(newRotation);
            }
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
        public Vector3 GetLocalInputDirection()
        {
            CharacterControllerInput input = Input.CharacterControllerInput;
            Vector2 movementInput = new Vector2(input.HorizontalMovement, input.VerticalMovement).normalized;

            Vector3 inputDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
            Vector3 localDirection = Camera.transform.TransformDirection(inputDirection);

            return localDirection;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (-transform.up * Data.RaycastTowardGroundToDetectFallDistance));
            Gizmos.color = new Color(0.03f, 1f, 0f);
            Gizmos.DrawLine(transform.position, transform.position + (Rigidbody.velocity));
        }

#endif
    }
}
