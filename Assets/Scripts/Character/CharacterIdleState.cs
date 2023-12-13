namespace Character
{
    /// <summary>
    /// This state handle the character when he's not moving at all
    /// </summary>
    public class CharacterIdleState : CharacterStateBase
    {
        public CharacterIdleState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override void Quit()
        {
        }
    }
}