using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's falling without having jump first
    /// </summary>
    public class CharacterFallState : CharacterStateBase
    {
        public CharacterFallState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Animator.SetBool("isJumping", true);
            Controller.GameplayData.IsGrounded = false;
        }

        public override void Update()
        {
            CheckForGround();
        }

        public override void Quit()
        {
            Controller.Animator.SetBool("isJumping", false);
            Controller.GameplayData.IsGrounded = true;
        }

        public override void OnColliderEnter(Collision collision)
        {
        }

        private void CheckForGround()
        {
            RaycastHit hit = Controller.GetRaycastTowardGround();
            if (hit.collider == null || hit.collider.gameObject == Controller.gameObject)
            {
                return;
            }

            bool inputMoving = Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical(); 
            Controller.GameplayData.IsGrounded = true;
            Controller.StateManager.SwitchState(inputMoving ? Controller.StateManager.WalkState : Controller.StateManager.IdleState);
        }
    }
}