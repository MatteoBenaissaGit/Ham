using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterController", order = 1)]
    public class CharacterControllerData : ScriptableObject
    {
        [field:Header("Walk")]
        [field:SerializeField] public float WalkSpeed { get; private set; } = 1f;
        
        [field:Header("Jump")]
        [field:SerializeField] public float JumpForce { get; private set; } = 1f;
    }
}