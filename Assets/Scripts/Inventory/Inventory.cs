using UnityEngine;
using System.Collections.Generic;
using System;

namespace PraxilabsTask
{
    [CreateAssetMenu(menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject, IInventory
    {
        [SerializeField] private int _capacity;
        [SerializeField] private List<InventoryData> _startItems;

        public Dictionary<InventoryItem, int> AvailableItems { get; } = new();

        /// <summary>
        /// Number of items currently in the inventory
        /// </summary>
        public int Size => AvailableItems.Count;

        /// <summary>
        /// Max number if items that this inventory can hold
        /// </summary>
        public int Capacity { get => _capacity; set => _capacity = value; }

        public void CopyStartItems()
        {
            foreach (var item in _startItems)
                Add(item.Item, item.Count);
        }

        public bool Add(InventoryItem newItem, int numberOfItems = 1)
        {
            bool isFull = Size >= Capacity;
            if (isFull)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            bool itemFound = UpdateAvailableItems(newItem, numberOfItems);

            if (!itemFound)
                AvailableItems.Add(newItem, numberOfItems);

            return true;
        }

        private bool UpdateAvailableItems(InventoryItem newItem, int numberOfItems)
        {
            foreach (var availableItem in AvailableItems)
            {
                bool sameType = availableItem.Key.ItemType == newItem.ItemType;
                if (sameType)
                {
                    // exception handling: trying to add item not configured correctly
                    if (!AvailableItems.ContainsKey(newItem))
                    {
                        Debug.LogWarning($"Invalid Item with type {newItem.ItemType}");
                        continue;
                    }

                    AvailableItems[newItem] += numberOfItems;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// How many of this item player have in his inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int NumberOfItemsOf(InventoryItem item)
        {
            if (AvailableItems.ContainsKey(item))
                return AvailableItems[item];
            else
                return -1;
        }

        public void RemoveItem(InventoryItem item)
        {
            if (!AvailableItems.ContainsKey(item))
            {
                Debug.LogWarning("trying to removing item which is not exsit!");
                return;
            }

            if (AvailableItems[item] > 1)
                AvailableItems[item]--;
            else
                AvailableItems.Remove(item);
        }

        public void RemoveAllItems(InventoryItem item)
        {
            if (!AvailableItems.ContainsKey(item))
            {
                Debug.LogWarning("trying to removing item which is not exsit!");
                return;
            }

            AvailableItems.Remove(item);
        }

        public void Clear()
        {
            AvailableItems.Clear();
        }

        [Serializable]
        private class InventoryData
        {
            public InventoryItem Item;
            public int Count;
        }
    }
}