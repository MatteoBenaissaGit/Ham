using System;
using Data.Character;
using Gravity;
using Inputs;
using UnityEngine;

namespace Character
{
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

        #region MonoBehaviour methods

        private void Awake()
        {
            Input = new InputManager();
            Input.Initialize();
            
            StateManager = new CharacterStateManager();
            StateManager.Initialize(this);
        }

        private void Update()
        {
            StateManager.UpdateState();

            FollowRigidbodyRotation();
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

        private void FollowRigidbodyRotation()
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

            Vector3 right = Camera.transform.right;

            Vector3 moveDirection = (horizontal * right + vertical * forward).normalized;

            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                Mesh.rotation = Quaternion.Slerp(Mesh.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (-transform.up * Data.RaycastTowardGroundToDetectFallDistance));
        }

#endif
    }
}
