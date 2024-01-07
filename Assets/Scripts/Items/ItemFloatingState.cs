using UnityEngine;

namespace Items
{
    public class ItemFloatingState : ItemBaseState
    {
        public ItemFloatingState(ItemController item) : base(item)
        {
        }
        
        public override void Enter()
        {
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

        public override void Exit()
        {
        }
        
        protected void MakeFloatingMeshFloatOnAboveGround()
        {
            float rotationSpeed = 0.25f;
            Item.transform.Rotate(Item.transform.up, rotationSpeed);

            RaycastHit hit = Item.GetRaycastTowardGround();
            Collider collider = hit.collider;
            float distanceToFloat = 1f;
            
            if (collider == null || collider.isTrigger || Vector3.Distance(Item.transform.position, hit.point) > distanceToFloat)
            {
                float moveSpeed = 0.1f;
                Item.transform.position -= Item.transform.up * moveSpeed;
            }
        }
    }
}