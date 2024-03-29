
public interface IInventoryItem
{
    public ItemType ItemType { get; set; }
    public Rarity Rarity { get; }
    public int Stat {  get; }
    public int StackSize { get; }

    public void Use();
}

public enum ItemType
{
    Weapon,
    Armor,
    Potion
}

public enum Rarity
{
    Common,
    Rare,
    Epic
}

public enum Stat
{

}