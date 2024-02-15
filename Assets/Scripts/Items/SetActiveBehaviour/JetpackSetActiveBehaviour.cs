namespace Items.SetActiveBehaviour
{
    public class JetpackSetActiveBehaviour : ItemSetActiveBehaviour
    {
        public JetpackSetActiveBehaviour(ItemController item) : base(item)
        {
        }

        public override void SetItemActive(bool isActive)
        {
            base.SetItemActive(isActive);
            
            Character.CharacterController.Instance.UI.EnergyBar.SetBar(isActive);
        }
    }
}