using System;
using Data.Items;
using Items.AimBehaviours;
using Items.Props;
using Items.Props.Projectile;
using Items.SetActiveBehaviour;
using UnityEngine;

namespace Items
{
    public class ItemController : MonoBehaviour
    {
        [field:SerializeField] public ItemData Data { get; private set; }
        [field:SerializeField] public GameObject FloatingMesh { get; private set; }
        [field:SerializeField] public Collider FloatingColliderCharacterDetection { get; private set; }
        
        [field:Header("Weapon")]
        [field:SerializeField] public Transform GunIK { get; private set; }
        [field:SerializeField] public Transform Muzzle { get; private set; }
        [field:SerializeField] public Projectile Projectile { get; private set; }
        
        public ItemBaseState CurrentState { get; private set; }
        public ItemFloatingState FloatingState { get; private set; }
        public ItemUsedState UsedState { get; private set; }
        public ItemAimBehaviour AimBehaviour { get; private set; }
        public ItemSetActiveBehaviour SetActiveBehaviour { get; private set; }
        
        protected virtual void Awake()
        {
            SetBehaviours();

            Initialize(new ItemFloatingState(this), new ItemUsedState(this));
        }

        /// <summary>
        /// Set the behaviours of the item depending on its type
        /// </summary>
        private void SetBehaviours()
        {
            switch (Data.Type)
            {
                case ItemType.None:
                    break;
                case ItemType.SimplePistol:
                    AimBehaviour = new SimplePistolAimBehaviour(this);
                    SetActiveBehaviour = new SimplePistolSetActiveBehaviour(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            SetState(UsedState);
            character.UI.HotBar.AddItemToBar(this);
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

        /// <summary>
        /// Drop the item 
        /// </summary>
        public void Drop()
        {
            SetState(FloatingState);
        }

        public GameObject InstantiateGameObject(GameObject gameObjectToInstantiate)
        {
            return Instantiate(gameObjectToInstantiate);
        }
    }
}
