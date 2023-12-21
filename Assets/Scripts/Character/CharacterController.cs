using System;
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

            if (GetLocalInputDirection().magnitude <= 0.1f)
            {
                return;
            }
            
            Vector2 input = Input.CharacterControllerInput.GetInputVector();

            Vector3 localForward = Camera.transform.forward * input.y;
            Vector3 localSide = Camera.transform.right * input.x;
            Vector3 localDirection = (localForward + localSide).normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(localDirection, Vector3.up);
            Mesh.rotation = Quaternion.Slerp(Mesh.rotation, targetRotation, Data.WalkRotationSpeed * Time.deltaTime); 
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
