using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, IInventory
{
    [SerializeField] private int _capacity;
    private readonly Dictionary<IInventoryItem, int> _availableItems = new();

    /// <summary>
    /// Number of items currently in the inventory
    /// </summary>
    public int Size => _availableItems.Count;

    /// <summary>
    /// Max number if items that this inventory can hold
    /// </summary>
    public int Capacity { get => _capacity; set => _capacity = value; }

    public bool Add(IInventoryItem inventoryItem)
    {
        bool isFull = Size >= Capacity;
        if (isFull)
            return false;

        // avoid add new item inside the loop!
        bool itemFound = false;
        foreach (var item in _availableItems)
        {
            if (item.Key.ItemType == inventoryItem.ItemType)
            {
                _availableItems[inventoryItem]++;
                itemFound = true;
                break;
            }
        }

        if (!itemFound) 
                _availableItems.Add(inventoryItem, 1);

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

    public void RemoveAllItems(IInventoryItem item)
    {
        if (!_availableItems.ContainsKey(item))
        {
            Debug.LogWarning("trying to removing item which is not exsit!");
            return;
        }

        _availableItems.Remove(item);
    }

    public void Clear()
    {
        _availableItems.Clear();
    }
}
