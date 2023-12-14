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
            
            HandleMovement();
        }

        public override void Quit()
        {
            Controller.Animator.SetBool("isWalking", false);
            
            Controller.Rigidbody.velocity = new Vector3(0,Controller.Rigidbody.velocity.y, 0);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        private void HandleMovement()
        {
            CharacterControllerInput input = Controller.Input.CharacterControllerInput;
            Vector2 movementInput = new Vector2(input.HorizontalMovement, input.VerticalMovement).normalized;

            Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
            Vector3 localMovement = Controller.Camera.transform.TransformDirection(moveDirection) * Controller.Data.WalkSpeed;

            Controller.Rigidbody.velocity = new Vector3(localMovement.x, Controller.Rigidbody.velocity.y, localMovement.z);
        }
    }
}