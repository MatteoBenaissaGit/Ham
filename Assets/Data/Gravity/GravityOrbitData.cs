﻿using UnityEngine;

namespace Data.Gravity
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gravity", order = 2)]
    public class GravityOrbitData : ScriptableObject
    {
        [field:Tooltip("This is the speed a gravity controller is rotated toward the center when he's in the orbit")]
        [field:SerializeField] public float RotationSpeed { get; private set; } = 1f;
        [field:SerializeField] public float GravityForce { get; private set; } = 10f;
        [field:SerializeField] public bool FixedDirection { get; private set; }
    }
}