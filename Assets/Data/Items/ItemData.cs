﻿using Items;
using UnityEngine;

namespace Data.Items
{
    public enum ItemType
    {
        None = 0,
        SimplePistol = 1,
        ZipLine = 2,
        Jetpack = 3
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 3)]
    public class ItemData : ScriptableObject
    {
        [field:SerializeField] public string ID { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public Sprite HotBarSprite { get; private set; }
        [field:SerializeField] public ItemType Type { get; private set; }
    }
}