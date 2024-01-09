using UnityEngine;

namespace Items
{
    public abstract class ItemBaseState
    {
        public ItemController Controller { get; private set; }

        public ItemBaseState(ItemController controller)
        {
            Controller = controller;
        }
        
        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnTriggerEnter(Collider collider);
        public abstract void Exit();
    }
}