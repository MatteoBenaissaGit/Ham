using UnityEngine;

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
        
        public ItemUsedState(ItemController item) : base(item)
        {
        }
        
        public override void Enter()
        {
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
        }

        private void Set(bool isActive)
        {
            
        }
    }
}