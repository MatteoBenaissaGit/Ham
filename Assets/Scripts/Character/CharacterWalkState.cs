using Inputs;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's moving on the ground
    /// </summary>
    public class CharacterWalkState : CharacterStateBase
    {
        private float _currentAccelerationTime;
        
        public CharacterWalkState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _currentAccelerationTime = 0;
            
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Walk);
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
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.StopWalk);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        /// <summary>
        /// Handle the movement input and apply them to the rigidbody.
        /// Is called in FixedUpdate.
        /// </summary>
        private void HandleMovement()
        {
            _currentAccelerationTime += Time.fixedDeltaTime;
            float accelerationValue = Mathf.Clamp01(_currentAccelerationTime / Controller.Data.AccelerationTime);
            float accelerationMultiplier = Controller.Data.AccelerationCurve.Evaluate(accelerationValue);
            
            Rigidbody rigidbody = Controller.Rigidbody;
            float speed = Controller.Data.WalkSpeed * accelerationMultiplier;
            rigidbody.MovePosition(rigidbody.position + Controller.GetCameraRelativeInputDirection() * (speed * Time.fixedDeltaTime));
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