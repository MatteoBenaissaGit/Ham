using UnityEngine;

namespace Character
{
    /// <summary>
    /// An abstract class defining the methods contained in character's states
    /// </summary>
    public abstract class CharacterStateBase
    {
        /// <summary>
        /// The CharacterController the state is attached to 
        /// </summary>
        protected CharacterController Controller { get; private set; }

        public CharacterStateBase(CharacterController controller)
        {
            Controller = controller;
            
            Controller.Input.CharacterControllerInput.OnJump += Jump;
        }

        /// <summary>
        /// This method is called when the state is entered by the controller
        /// </summary>
        public abstract void Enter();
        
        /// <summary>
        /// This method is called every frame by the controller
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// This method is called every fixed time by the controller
        /// </summary>
        public abstract void FixedUpdate();
        
        /// <summary>
        /// This method is called when the state is exited by the controller
        /// </summary>
        public abstract void Quit();
        
        /// <summary>
        /// This method is called when the controller collide on something
        /// </summary>
        public abstract void OnColliderEnter(Collision collision);
        
        /// <summary>
        /// This method is called when the jump button is pressed
        /// </summary>
        public abstract void Jump(bool isPressingJump);
    }
}
