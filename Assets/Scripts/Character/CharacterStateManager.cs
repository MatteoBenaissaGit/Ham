using UnityEngine;

namespace Character
{
    /// <summary>
    /// This class handle the management of the character states
    /// </summary>
    public class CharacterStateManager
    {
        public CharacterStateBase CurrentState { get; private set; }
        public CharacterIdleState IdleState { get; private set; }
        public CharacterWalkState WalkState { get; private set; }
        public CharacterJumpState JumpState { get; private set; }
        
        /// <summary>
        /// This method initialize the state classes
        /// </summary>
        /// <param name="controller">The CharacterController running the states</param>
        public void Initialize(CharacterController controller)
        {
            IdleState = new CharacterIdleState(controller);
            WalkState = new CharacterWalkState(controller);
            JumpState = new CharacterJumpState(controller);
            
            SwitchState(IdleState);
        }

        /// <summary>
        /// This method handle the switch between two states
        /// </summary>
        /// <param name="state">the state to switch to</param>
        public void SwitchState(CharacterStateBase state)
        {
            if (state == CurrentState)
            {
                Debug.Log("You're trying to switch to a state you're already in.");
                return;
            }

            CurrentState?.Quit();

            CurrentState = state;
            CurrentState.Enter();
        }

        /// <summary>
        /// This method update the current state every-frame
        /// </summary>
        public void UpdateState()
        {
            CurrentState?.Update();
        }
    }
}