using Interfaces;
using UnityEngine;

namespace Items.Props.ZipLine
{
    public class ZipLineTipController : MonoBehaviour, IInteractable
    {
        public bool CanBeInteractedWith { get; set; } = true;
        
        [SerializeField] private ZipLineController _zipLine;

        public void CharacterIsInRange(bool isInRange)
        {
        }

        public void Interact(Character.CharacterController characterController)
        {
        }
    }
}