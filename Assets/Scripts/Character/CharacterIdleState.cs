using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's not moving at all
    /// </summary>
    public class CharacterIdleState : CharacterStateBase
    {
        public CharacterIdleState(CharacterController controller) : base(controller)
        {
        }

        public override string ToString()
        {
            return "Idle state";
        }

        public override void Enter()
        {
            Controller.GameplayData.IsGrounded = true;
        }

        public override void Update()
        {
            if (Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical())
            {
                Controller.StateManager.SwitchState(Controller.StateManager.WalkState);
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Quit()
        {
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        public override void Jump(bool isPressingJump)
        {
            if (isPressingJump == false || IsActive == false)
            {
                return;
            }
            Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
        }
    }
}