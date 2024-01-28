namespace Items.AimBehaviours
{
    public abstract class ItemAimBehaviour
    {
        public ItemController Item { get; private set; }

        public ItemAimBehaviour(ItemController item)
        {
            Item = item;
        }

        public abstract void Aim(bool doAim);
        public abstract void Shoot();
    }
}