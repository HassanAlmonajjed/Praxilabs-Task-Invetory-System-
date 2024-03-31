using UnityEngine;

namespace PraxilabsTask
{
    public abstract class InventoryItem : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; set; }
        [field: SerializeField] public Rarity Rarity { get; protected set; }
        [field: SerializeField] public int Stat { get; protected set; }
        [field: SerializeField] public int StackSize { get; protected set; }

        public abstract void Use();
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        Bag
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic
    }
}