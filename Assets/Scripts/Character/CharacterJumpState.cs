using UnityEngine;

namespace Character
{
    public enum JumpState
    {
        Up = 0,
        Apex = 1,
        Down = 2
    }
    
    /// <summary>
    /// This state handle the character when he's jumping
    /// </summary>
    public class CharacterJumpState : CharacterStateBase
    {
        public JumpState CurrentJumpState { get; private set; }
        
        private const float VelocityMagnitudeToBeAtApex = 1f;

        private int _numberOfJumpInputs;
        private float _minimumTimeBeforeCheckingState;
        
        public CharacterJumpState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.GameplayData.IsGrounded = false;

            _numberOfJumpInputs = 0;
            _minimumTimeBeforeCheckingState = 0.1f;
            CurrentJumpState = JumpState.Up;

            Vector3 currentWalkVelocity = Controller.GetCameraRelativeInputDirection() * Controller.Data.WalkSpeed;
            Vector3 jumpForce = Controller.Rigidbody.transform.up * Controller.Data.JumpForce;
            Controller.Rigidbody.velocity = Vector3.zero;
            Controller.Rigidbody.AddForce(jumpForce + currentWalkVelocity, ForceMode.Impulse);
            
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Jump);
        }

        public override void Update()
        {
            HandlingInputsAfterJump();
            
            _minimumTimeBeforeCheckingState -= Time.deltaTime;
            if (_minimumTimeBeforeCheckingState <= 0)
            {
                CheckJumpState();
            }

            if (CurrentJumpState != JumpState.Up)
            {
                CheckForGroundFall();
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Quit()
        {
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Land);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        /// <summary>
        /// This method check the current jump state of the character, either it's going up, at its apex or down
        /// </summary>
        private void CheckJumpState()
        {
            Vector3 localVelocity = Controller.Rigidbody.transform.InverseTransformDirection(Controller.Rigidbody.velocity);
            if (Mathf.Abs(localVelocity.y) <= VelocityMagnitudeToBeAtApex)
            {
                CurrentJumpState = JumpState.Apex;
                return;
            }
            
            float dotProduct = Vector3.Dot(Controller.Rigidbody.velocity, (-Controller.GravityBody.GravityDirection));
            bool isGoingUp = dotProduct > 0;

            CurrentJumpState = isGoingUp ? JumpState.Up : JumpState.Down;

            Controller.GravityBody.GravityMultiplier = CurrentJumpState == JumpState.Down ? Controller.Data.FallGravityMultiplier : 1f;
        }
        
        /// <summary>
        /// This method check for input after the initial one and add some up force is needed
        /// </summary>
        private void HandlingInputsAfterJump()
        {
            if (Controller.Input.CharacterControllerInput.Jump == false
                || _numberOfJumpInputs > Controller.Data.MaximumAddedInputAfterJump
                || CurrentJumpState != JumpState.Up)
            {
                return;
            }
            
            Controller.Rigidbody.AddForce(Controller.Rigidbody.velocity.normalized * Controller.Data.ForceAddedPerInputAfterJump, ForceMode.Impulse);
            _numberOfJumpInputs++;
        }
        
        /// <summary>
        /// This method check if the character hit something under him while he's going down and switch to another state if needed
        /// </summary>
        private void CheckForGroundFall()
        {
            RaycastHit hit = Controller.GetRaycastTowardGround();
            if (hit.collider == null || hit.collider.gameObject == Controller.gameObject)
            {
                return;
            }

            bool inputMoving = Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical(); 
            Controller.GameplayData.IsGrounded = true;

            if (Controller.Data.DoJumpBuffering && 
                Controller.Input.CharacterControllerInput.LastJumpInputTime < Controller.Data.JumpBufferTimeMaxBeforeLand)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.JumpState, true);
                return;
            }
            
            Controller.StateManager.SwitchState(inputMoving ? Controller.StateManager.WalkState : Controller.StateManager.IdleState);
        }
    }
}