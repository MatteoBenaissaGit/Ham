using Inputs;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's moving on the ground
    /// </summary>
    public class CharacterWalkState : CharacterStateBase
    {
        public CharacterWalkState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Animator.SetBool("isWalking", true);
        }

        public override void Update()
        {
            if (Controller.Input.CharacterControllerInput.Jump)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
            }
            
            if (Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical() == false)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.IdleState);
            }
            
            CheckForFall();
        }

        public override void FixedUpdate()
        {
            HandleMovement();
        }

        public override void Quit()
        {
            Controller.Animator.SetBool("isWalking", false);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        /// <summary>
        /// Handle the movement input and apply them to the rigidbody
        /// </summary>
        private void HandleMovement()
        {
            Rigidbody rigidbody = Controller.Rigidbody;
            rigidbody.MovePosition(rigidbody.position + Controller.GetLocalInputDirection() * (Controller.Data.WalkSpeed * Time.fixedDeltaTime));
        }
        
        /// <summary>
        /// Check if the player is falling while walking and change its state if necessary
        /// </summary>
        private void CheckForFall()
        {
            RaycastHit hit = Controller.GetRaycastTowardGround();
            if (hit.collider != null && hit.collider.gameObject != Controller.gameObject)
            {
                return;
            }

            Controller.GameplayData.IsGrounded = false;
            Controller.StateManager.SwitchState(Controller.StateManager.FallState);
        }
    }
}