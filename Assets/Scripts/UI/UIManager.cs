using MatteoBenaissaLibrary.SingletonClassBase;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// This class manage the player ui 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [field:SerializeField] public HotBarController HotBar { get; private set; }
        [field:SerializeField] public AimUIController Aim { get; private set; }
        [field:SerializeField] public InteractionUIController Interaction { get; private set; }
        [field:SerializeField] public EnergyBarController EnergyBar { get; private set; }
    }
}
