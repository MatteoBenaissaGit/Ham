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
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        private void HandleMovement()
        {
            CharacterControllerInput input = Controller.Input.CharacterControllerInput;
            Vector2 movementInput = new Vector2(input.HorizontalMovement, -input.VerticalMovement).normalized;
            Vector3 walkMovement = new Vector3(movementInput.x, 0, movementInput.y) * Controller.Data.WalkSpeed;
        }
    }
}