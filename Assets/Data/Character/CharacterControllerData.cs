using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterController", order = 1)]
    public class CharacterControllerData : ScriptableObject
    {
        [field: Header("View")]
        [field: SerializeField] public float JumpTrailLength { get; private set; } = 0.5f;
        
        [field: Header("Walk")]
        [field: SerializeField]
        public float WalkSpeed { get; private set; } = 1f;

        [field: SerializeField] public float WalkRotationSpeed { get; private set; } = 3f;
        [field: SerializeField] public float AccelerationTime { get; private set; } = 1f;

        [field: SerializeField]
        public AnimationCurve AccelerationCurve { get; private set; } = AnimationCurve.Linear(0, 0, 1, 1);

        [field: Header("Jump")]
        [field: SerializeField]
        public float JumpForce { get; private set; } = 1f;

        [field: SerializeField] public float ForceAddedPerInputAfterJump { get; private set; } = 0.1f;
        [field: SerializeField] public int MaximumAddedInputAfterJump { get; private set; } = 20;
        [field: SerializeField] public float RaycastTowardGroundToDetectFallDistance { get; private set; } = 2f;

        [field: Header("Coyote Time")]
        [field: SerializeField] public bool DoCoyoteTime { get; private set; } = true;
        [field: SerializeField] public float CoyoteTimeTimeToJumpAfterFall { get; private set; } = 0.5f;
    }
}