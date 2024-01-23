using System;
using Data.Items;
using Items.Weapon;
using UI;
using UnityEngine;

namespace Items
{
    public class ItemController : MonoBehaviour
    {
        [field:SerializeField] public ItemData Data { get; private set; }
        [field:SerializeField] public GameObject FloatingMesh { get; private set; }
        [field:SerializeField] public Collider FloatingColliderCharacterDetection { get; private set; }
        
        public ItemBaseState CurrentState { get; private set; }
        public ItemFloatingState FloatingState { get; private set; }
        public ItemUsedState UsedState { get; private set; }

        protected virtual void Awake()
        {
            Initialize(new ItemFloatingState(this), new ItemUsedState(this));
        }

        /// <summary>
        /// Initialize the item
        /// </summary>
        /// <param name="floatingState">the floating state of the item</param>
        /// <param name="usedState">the used state of the item</param>
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

            if (other.TryGetComponent(out Character.CharacterController character) == false
                || character.UI.HotBar.IsThereSpaceInHotBar() == false)
            {
                return;
            }
            character.UI.HotBar.AddItemToBar(this);
            SetState(UsedState);
        }

        /// <summary>
        /// Set a new state for the weapon
        /// </summary>
        /// <param name="state">the state to enter</param>
        protected void SetState(ItemBaseState state)
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

        public void Drop()
        {
            SetState(FloatingState);
        }
    }
}
