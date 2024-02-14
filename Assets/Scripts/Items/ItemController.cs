using System;
using Data.Items;
using Items.Props;
using Items.Props.Projectile;
using Items.SetActiveBehaviour;
using Items.UseBehaviours;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Items
{
    public class ItemController : MonoBehaviour
    {
        [field:SerializeField] public ItemData Data { get; private set; }
        [field:SerializeField] public GameObject FloatingMesh { get; private set; }
        [field:SerializeField] public GameObject UsedMesh { get; private set; }
        [field:SerializeField] public Collider FloatingColliderCharacterDetection { get; private set; }
        
        [field:Header("Weapon")]
        [field:SerializeField] public Transform[] GunIKs { get; private set; } = new Transform[]{};
        [field:SerializeField] public Transform Muzzle { get; private set; }
        [field:SerializeField] public Projectile Projectile { get; private set; }

        [field: Header("Preview")]
        [field: SerializeField] public GameObject[] PreviewMeshes { get; private set; } = new GameObject[] { };
        [field: SerializeField] public Material PreviewMaterial { get; private set; }
        [field: SerializeField] public Material PreviewMaterialError { get; private set; }
        
        public ItemBaseState CurrentState { get; private set; }
        public ItemFloatingState FloatingState { get; private set; }
        public ItemUsedState UsedState { get; private set; }
        public ItemUseBehaviour UseBehaviour { get; private set; }
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
            SetActiveBehaviour = new ItemSetActiveBehaviour(this);

            switch (Data.Type)
            {
                case ItemType.SimplePistol:
                    UseBehaviour = new SimplePistolUseBehaviour(this);
                    break;
                case ItemType.ZipLine:
                    UseBehaviour = new ZiplineUseBehaviour(this);
                    break;
                case ItemType.Jetpack:
                    UseBehaviour = new JetpackUseBehaviour(this);
                    SetActiveBehaviour = new JetpackSetActiveBehaviour(this);
                    break;
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

        public void Destroy()
        {
            if (CurrentState == FloatingState)
            {
                Object.Destroy(transform.gameObject);
                return;
            }

            if (UseBehaviour != null)
            {
                Character.CharacterController.Instance.Input.ItemInput.OnAim -= UsedState.Aim;
                Character.CharacterController.Instance.Input.ItemInput.OnShoot -= UsedState.Shoot;
                Character.CharacterController.Instance.Input.ItemInput.OnShootOnce -= UsedState.ShootOnce;
            }

            Character.CharacterController.Instance.UI.HotBar.DestroyItem(this);
        }
    }
}
