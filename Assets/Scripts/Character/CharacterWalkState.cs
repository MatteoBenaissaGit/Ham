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
        }

        public override void Quit()
        {
            Controller.Animator.SetBool("isWalking", false);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }
    }
}