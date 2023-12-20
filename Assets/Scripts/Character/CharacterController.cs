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
        [field:SerializeField] public GravityController Gravity { get; private set; }
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
            float horizontal = input.HorizontalMovement;
            float vertical = input.VerticalMovement;

            Vector3 forward = Camera.transform.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 localForward = Camera.transform.forward * vertical;
            Vector3 localSide= Camera.transform.right * horizontal;
            Vector3 moveDirection = (localForward + localSide).normalized;

            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Rigidbody.transform.up);
                Mesh.rotation = Quaternion.Lerp(Mesh.rotation, targetRotation, Time.deltaTime * 10f);
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
        /// This method allows you to get the local velocity 
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLocalVelocity()
        {
            return Rigidbody.transform.InverseTransformDirection(Rigidbody.velocity);
        }

        /// <summary>
        /// This method allows you to set the controller's rigidbody velocity from a localVelocity
        /// </summary>
        /// <param name="localVelocity">the local velocity you want to apply</param>
        public void SetRigidbodyLocalVelocity(Vector3 localVelocity)
        {
            Vector3 worldVelocity = Rigidbody.transform.TransformDirection(localVelocity);
            Rigidbody.velocity = worldVelocity;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (-transform.up * Data.RaycastTowardGroundToDetectFallDistance));


            if (Gravity.Orbit != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Rigidbody.transform.position, - Rigidbody.transform.up * 100);
                Gizmos.color = new Color(1f, 0.48f, 0f);
                Gizmos.DrawLine(Rigidbody.transform.position, Gravity.Orbit.transform.position);
                Gizmos.color = new Color(0.99f, 0f, 1f);
                Gizmos.DrawLine(Rigidbody.transform.position, (Rigidbody.position - Gravity.Orbit.transform.position).normalized * 110);
                
            }
        }

#endif
    }
}
