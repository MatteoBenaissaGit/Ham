﻿using UnityEngine;

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

            Controller.Rigidbody.velocity = Vector3.Lerp(Controller.Rigidbody.velocity, Vector3.zero, 0.1f);
        }

        public override void Quit()
        {
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }
    }
}