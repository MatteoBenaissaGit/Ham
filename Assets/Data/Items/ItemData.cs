using UnityEngine;

namespace Data.Items
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 3)]
    public class ItemData : ScriptableObject
    {
        [field:SerializeField] public string ID { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public Sprite HotBarSprite { get; private set; }
    }
}