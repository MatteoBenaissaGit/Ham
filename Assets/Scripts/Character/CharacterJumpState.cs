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
        private int _numberOfJumpInputs;
        private float _minimumTimeBeforeCheckingState;
        private JumpState _currentJumpState;

        public CharacterJumpState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Debug.Log("Jump enter");
            
            Controller.Animator.SetBool("isJumping", true);
            
            _numberOfJumpInputs = 0;
            _minimumTimeBeforeCheckingState = 0.1f;
            _currentJumpState = JumpState.Up;
            
            Controller.Rigidbody.AddForce(Controller.Rigidbody.transform.up * Controller.Data.JumpForce, ForceMode.Impulse);
        }

        public override void Update()
        {
            HandlingInputsAfterJump();
            
            _minimumTimeBeforeCheckingState -= Time.deltaTime;
            if (_minimumTimeBeforeCheckingState > 0)
            {
                return;
            }

            CheckJumpState();
            if (_currentJumpState == JumpState.Down)
            {
                CheckForGroundFall();
            }
        }

        public override void Quit()
        {
            Debug.Log("Jump quit");
            
            Controller.Animator.SetBool("isJumping", false);
            
            _numberOfJumpInputs = 0;
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        /// <summary>
        /// This method check the current jump state of the character, either it's going up, at its apex or down
        /// </summary>
        private void CheckJumpState()
        {
            Vector3 planetCenter = Controller.Gravity.Orbit.transform.position;
            float dotProduct = Vector3.Dot(Controller.Rigidbody.velocity, (Controller.transform.position - planetCenter).normalized);
            bool isGoingUp = dotProduct >= 0;

            _currentJumpState = isGoingUp ? JumpState.Up : JumpState.Down;
        }
        
        /// <summary>
        /// This method check for input after the initial one and add some up force is needed
        /// </summary>
        private void HandlingInputsAfterJump()
        {
            if (Controller.Input.CharacterControllerInput.Jump &&
                _numberOfJumpInputs <= Controller.Data.MaximumAddedInputAfterJump)
            {
                Controller.Rigidbody.AddForce(Controller.Rigidbody.transform.up * Controller.Data.ForceAddedPerInputAfterJump);
                _numberOfJumpInputs++;
            }
        }
        
        /// <summary>
        /// This method check if the character hit something under him while he's going down and switch to another state if needed
        /// </summary>
        private void CheckForGroundFall()
        {
            float raycastDistance = Controller.Data.RaycastTowardGroundToDetectFallDistance;
            if (Physics.Raycast(Controller.transform.position, -Controller.transform.up, out RaycastHit hit, raycastDistance) == false)
            {
                return;
            }

            if (hit.collider.gameObject == Controller.gameObject)
            {
                return;
            }

            bool inputMoving = Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical(); 
            Controller.StateManager.SwitchState(inputMoving ? Controller.StateManager.WalkState : Controller.StateManager.IdleState);
        }
    }
}