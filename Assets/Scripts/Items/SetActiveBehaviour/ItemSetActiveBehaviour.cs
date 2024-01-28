namespace Items.SetActiveBehaviour
{
    public class ItemSetActiveBehaviour
    {
        protected ItemController Item { get; set; }
        
        public ItemSetActiveBehaviour(ItemController item)
        {
            Item = item;
        }

        public virtual void SetItemActive(bool isActive)
        {
            
        }
    }
}