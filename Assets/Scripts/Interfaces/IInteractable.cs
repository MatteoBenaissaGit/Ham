using Character;

namespace Interfaces
{
    public interface IInteractable
    {
        public bool CanBeInteractedWith { get; set; }
        public void CharacterIsInRange(bool isInRange);
        public void Interact(CharacterController characterController);
    }
}