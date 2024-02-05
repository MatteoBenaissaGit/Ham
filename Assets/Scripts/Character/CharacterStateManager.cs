using UnityEngine;

namespace Character
{
    /// <summary>
    /// This class handle the management of the character states
    /// </summary>
    public class CharacterStateManager
    {
        public CharacterStateBase CurrentState { get; private set; }
        public CharacterStateBase PreviousState { get; private set; }
        public CharacterIdleState IdleState { get; private set; }
        public CharacterWalkState WalkState { get; private set; }
        public CharacterJumpState JumpState { get; private set; }
        public CharacterFallState FallState { get; private set; }
        public CharacterZipLineState ZipLineState { get; private set; }

        /// <summary>
        /// This method initialize the state classes
        /// </summary>
        /// <param name="controller">The CharacterController running the states</param>
        public void Initialize(CharacterController controller)
        {
            IdleState = new CharacterIdleState(controller);
            WalkState = new CharacterWalkState(controller);
            JumpState = new CharacterJumpState(controller);
            FallState = new CharacterFallState(controller);
            ZipLineState = new CharacterZipLineState(controller);
            
            SwitchState(IdleState);
        }

        /// <summary>
        /// This method handle the switch between two states
        /// </summary>
        /// <param name="state">the state to switch to</param>
        /// <param name="disableSecurityCheck">check if the state you want to switch to is already the current state</param>
        public void SwitchState(CharacterStateBase state, bool disableSecurityCheck = false)
        {
            if (state == CurrentState && disableSecurityCheck == false)
            {
                Debug.LogError($"You're switching to a state you're already in. {CurrentState.ToString()}");
                return;
            }

            PreviousState = CurrentState;
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

        /// <summary>
        /// This method fixed update the current state
        /// </summary>
        public void FixedUpdateState()
        {
            CurrentState?.FixedUpdate();
        }
    }
}