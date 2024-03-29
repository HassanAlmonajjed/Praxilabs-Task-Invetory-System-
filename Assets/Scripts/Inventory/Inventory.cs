using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Inventory/Players Inventory")]
public class Inventory : ScriptableObject, IInventory
{
    [SerializeField] private int _capacity;

    private readonly Dictionary<IInventoryItem, int> _availableItems = new();

    public int Size => _availableItems.Count;

    public int Capacity { get => _capacity; set => _capacity = value; }

    public bool Add(IInventoryItem item)
    {
        if (Size >= Capacity)
            return false;

        if (_availableItems.ContainsKey(item))
            _availableItems[item]++;
        else
            _availableItems.Add(item, 1);

        return true;
    }

    /// <summary>
    /// How many of this item player have in his inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int NumberOfItemsOf(IInventoryItem item)
    {
        if (_availableItems.ContainsKey(item))
            return _availableItems[item];
        else
            return -1;
    }

    public void Clear() => _availableItems.Clear();

    public void RemoveItem(IInventoryItem item)
    {
        if (!_availableItems.ContainsKey(item))
        {
            Debug.LogWarning("trying to removing item which is not exsit!");
            return;
        }

        if (_availableItems[item] > 1)
            _availableItems[item]--;
        else
            _availableItems.Remove(item);
    }

    public void RemoveAffItems(IInventoryItem item)
    {
        if (!_availableItems.ContainsKey(item))
        {
            Debug.LogWarning("trying to removing item which is not exsit!");
            return;
        }

        _availableItems.Remove(item);
    }
}
