using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    [SerializeField] protected ItemType _itemType;
    [SerializeField] protected Rarity _rarity;
    [SerializeField] protected int _stat;
    [SerializeField] protected int _stackSize;

    public abstract void Use();
}
