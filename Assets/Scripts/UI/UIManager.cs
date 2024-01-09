using MatteoBenaissaLibrary.SingletonClassBase;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// This class manage the player ui 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [field:SerializeField] public Character.CharacterController Character { get; private set; }
        [field:SerializeField] public HotBarController HotBar { get; private set; }
    }
}
