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

        public override void Enter()
        {
        }

        public override void Update()
        {
            if (Controller.Input.CharacterControllerInput.Jump)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
            }

            if (Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical())
            {
                Controller.StateManager.SwitchState(Controller.StateManager.WalkState);
            }

            Vector3 localVelocity = Controller.GetLocalVelocity();
            Vector3 newLocalVelocity = Vector3.Lerp(localVelocity, new Vector3(0, localVelocity.y, 0), 0.1f);
            Controller.SetRigidbodyLocalVelocity(newLocalVelocity);
        }

        public override void Quit()
        {
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }
    }
}