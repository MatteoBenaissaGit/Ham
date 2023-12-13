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
        public CharacterController Controller { get; private set; }

        public CharacterStateBase(CharacterController controller)
        {
            Controller = controller;
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
        /// This method is called when the state is exited by the controller
        /// </summary>
        public abstract void Quit();
    }
}
