using Interfaces;
using UnityEngine;

namespace Items.Props.ZipLine
{
    public class ZipLineTipController : MonoBehaviour, IInteractable
    {
        [SerializeField] private ZipLineController _zipLine;
        
        public void CharacterIsInRange(bool isInRange)
        {
        }

        public void Interact()
        {
        }
    }
}