using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's falling without having jump first
    /// </summary>
    public class CharacterFallState : CharacterStateBase
    {
        private float _currentFallTime;
        private bool _canDoCoyoteTime;
        
        public CharacterFallState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.CameraController.SetCameraAfterCurrent(Controller.CameraController.Data.FallCamera);

            Vector3 currentWalkVelocity = Controller.GetCameraRelativeInputDirectionWorld() * Controller.Data.WalkSpeed;
            Controller.Rigidbody.AddForce(currentWalkVelocity, ForceMode.Impulse);
            Controller.GameplayData.IsGrounded = false;

            _canDoCoyoteTime = true;
            _currentFallTime = 0f;

            CharacterStateManager state = Controller.StateManager;
            bool wasInAStateThatAllowCoyoteTime = state.PreviousState != state.WalkState || state.PreviousState != state.IdleState;
            
            if (Controller.Data.DoCoyoteTime == false || wasInAStateThatAllowCoyoteTime == false)
            {
                _canDoCoyoteTime = false;
                StartFall();
            }
        }

        public override void Update()
        {
            _currentFallTime += Time.deltaTime;
            
            CheckForGround();
            HandleCoyoteTime();
        }

        public override void FixedUpdate()
        {
            HandleMovementInTheAir();
        }

        public override void Quit()
        {
            Controller.CameraController.EndCurrentCameraState();

            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Land);
            Controller.GameplayData.IsGrounded = true;
        }

        public override void OnColliderEnter(Collision collision)
        {
        }

        /// <summary>
        /// This method check if there is ground beneath the player when he's falling and do actions if so
        /// </summary>
        private void CheckForGround()
        {
            RaycastHit hit = Controller.GetRaycastTowardGround();
            if (hit.collider == null || hit.collider.gameObject == Controller.gameObject  || hit.collider.isTrigger)
            {
                return;
            }

            bool inputMoving = Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical(); 
            Controller.GameplayData.IsGrounded = true;
            
            if (Controller.Data.DoJumpBuffering && 
                Controller.Input.CharacterControllerInput.LastJumpInputTime < Controller.Data.JumpBufferTimeMaxBeforeLand)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
                return;
            }
            
            Controller.StateManager.SwitchState(inputMoving ? Controller.StateManager.WalkState : Controller.StateManager.IdleState);
        }

        /// <summary>
        /// This method handle the coyote time management
        /// </summary>
        private void HandleCoyoteTime()
        {
            if (_currentFallTime > Controller.Data.CoyoteTimeTimeToJumpAfterFall && _canDoCoyoteTime)
            {
                StartFall();
                _canDoCoyoteTime = false;
                return;
            }
        }

        public override void Jump(bool isPressingJump)
        {
            if (_canDoCoyoteTime == false)
            {
                return;
            }
            Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
        }

        /// <summary>
        /// This method handle the start of the fall in animator
        /// </summary>
        private void StartFall()
        {
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Fall);
        }
        
        /// <summary>
        /// This method handle the player movement while falling
        /// </summary>
        private void HandleMovementInTheAir()
        {
            Rigidbody rigidbody = Controller.Rigidbody;
            float speed = Controller.Data.FallAirMovementAmplitude;
            Vector3 forceDirection = Controller.GetCameraRelativeInputDirectionWorld();

            Vector3 force = forceDirection * (speed * Time.fixedDeltaTime);
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}