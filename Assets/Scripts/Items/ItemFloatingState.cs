using UnityEngine;

namespace Items
{
    public class ItemFloatingState : ItemBaseState
    {
        public ItemFloatingState(ItemController controller) : base(controller)
        {
        }
        
        /// <summary>
        /// This method is called when the floating state is entered
        /// </summary>
        public override void Enter()
        {
            Controller.FloatingMesh.gameObject.SetActive(true);
            Controller.FloatingColliderCharacterDetection.enabled = true;
        }

        public override void Update()
        {
            MakeFloatingMeshFloatOnAboveGround();
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnTriggerEnter(Collider collider)
        {
            
        }

        /// <summary>
        /// This method is called when the floating state is exited
        /// </summary>
        public override void Exit()
        {
            Controller.FloatingColliderCharacterDetection.enabled = false;
            Controller.FloatingMesh.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Make the item's mesh float and rotate above the ground
        /// </summary>
        protected void MakeFloatingMeshFloatOnAboveGround()
        {
            float rotationSpeed = 0.25f;
            Controller.transform.Rotate(Controller.transform.up, rotationSpeed);

            RaycastHit hit = Controller.GetRaycastTowardGround();
            Collider collider = hit.collider;
            float distanceToFloat = 1f;
            
            if (collider == null || collider.isTrigger || Vector3.Distance(Controller.transform.position, hit.point) > distanceToFloat)
            {
                float moveSpeed = 0.1f;
                Controller.transform.position -= Controller.transform.up * moveSpeed;
            }
        }
    }
}