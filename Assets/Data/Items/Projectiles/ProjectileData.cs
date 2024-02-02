using UnityEngine;

namespace Data.Items.Projectiles
{
    public enum ProjectileType
    {
        None = 0,
        Fruit = 1
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectile", order = 4)]
    public class ProjectileData : ScriptableObject
    {
        [field:SerializeField] public ProjectileType Type { get; private set; }
        [field:SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SpeedMultiplierPerDistance { get; private set; } = 0.1f;
        [field: SerializeField] public float SpeedMultiplierIfNoRaycastHit { get; private set; } = 2f;
        [field: SerializeField] public float MaxSpeedMultiplierPerDistance { get; private set; } = 5f;
        [field:SerializeField] public float TimeBeforeDisappearing { get; private set; }
    }
}