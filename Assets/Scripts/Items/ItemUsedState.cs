using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items
{
    public class ItemUsedState : ItemBaseState
    {
        private bool _isActive;
        
        public ItemUsedState(ItemController controller) : base(controller)
        {
        }
        
        /// <summary>
        /// This method is called when the used state is entered
        /// </summary>
        public override void Enter()
        {
            Controller.transform.parent = Character.CharacterController.Instance.transform.transform;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }
        
        public override void OnTriggerEnter(Collider collider)
        {
            
        }

        /// <summary>
        /// This method is called when the used state is exited
        /// </summary>
        public override void Exit()
        {
            Controller.transform.parent = null;
            Character.CharacterController character = Character.CharacterController.Instance;
            Controller.transform.up = character.transform.up;
            Controller.transform.position += character.Mesh.transform.forward * 2 + character.transform.up;
        }

        /// <summary>
        /// Set the item in an active state of use
        /// </summary>
        /// <param name="isActive">is the item active</param>
        public virtual void SetActive(bool isActive)
        {
            _isActive = isActive;
        }
    }
}