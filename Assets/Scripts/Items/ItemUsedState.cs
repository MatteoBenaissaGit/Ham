using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items
{
    public class ItemUsedState : ItemBaseState
    {
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                Set(_isActive);
            } 
        }
        
        private bool _isActive;
        
        public ItemUsedState(ItemController controller) : base(controller)
        {
        }
        
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

        public override void Exit()
        {
            Controller.transform.parent = null;
            Character.CharacterController character = Character.CharacterController.Instance;
            Controller.transform.up = character.transform.up;
            Controller.transform.position += character.Mesh.transform.forward * 2 + character.transform.up;
        }

        private void Set(bool isActive)
        {
            
        }
    }
}