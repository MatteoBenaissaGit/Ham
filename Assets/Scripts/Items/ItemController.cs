using System;
using Data.Items;
using Items.Weapon;
using UnityEngine;

namespace Items
{
    public class ItemController : MonoBehaviour
    {
        [field:SerializeField] public ItemData Data { get; private set; }
        [field:SerializeField] public GameObject FloatingMesh { get; private set; }
        
        protected ItemBaseState CurrentState;
        protected ItemFloatingState FloatingState;
        protected ItemUsedState UsedState;

        protected virtual void Awake()
        {
            Initialize(new ItemFloatingState(this), new WeaponUsedState(this));
        }

        protected virtual void Initialize(ItemFloatingState floatingState, ItemUsedState usedState)
        {
            FloatingState = floatingState;
            UsedState = usedState;
            
            SetState(FloatingState);

            if (TryGetComponent(out Collider tryCollider) == false)
            {
                Debug.LogWarning("no collider on this item, won't be able to detect trigger enter !");
                return;
            }
            tryCollider.isTrigger = true;
        }

        private void Update()
        {
            CurrentState?.Update();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null)
            {
                return;
            }
            
            CurrentState?.OnTriggerEnter(other);
        }

        private void SetState(ItemBaseState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
        
        /// <summary>
        /// Create and get the result of a raycast hit toward the ground from the item's bottom
        /// </summary>
        /// <returns>The RaycastHit of the raycast</returns>
        public RaycastHit GetRaycastTowardGround()
        {
            float raycastDistance = 10f;
            Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance);
            return hit;
        }
    }
}
