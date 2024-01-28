using UnityEngine;

namespace Items.SetActiveBehaviour
{
    public class SimplePistolSetActiveBehaviour : ItemSetActiveBehaviour
    {
        public SimplePistolSetActiveBehaviour(ItemController item) : base(item)
        {
        }

        public override void SetItemActive(bool isActive)
        {
            base.SetItemActive(isActive);
            
            Item.FloatingMesh.SetActive(isActive);
        }
    }
}