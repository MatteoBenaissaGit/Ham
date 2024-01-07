using UnityEngine;

namespace Items
{
    public abstract class ItemBaseState
    {
        public ItemController Item { get; private set; }

        public ItemBaseState(ItemController item)
        {
            Item = item;
        }
        
        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnTriggerEnter(Collider collider);
        public abstract void Exit();
    }
}