using System.Collections.Generic;
using UnityEngine;

namespace PraxilabsTask
{
    [CreateAssetMenu(menuName = "Inventory/Inventory Item/Bag")]
    public class BagInventoryItem : InventoryItem
    {
        [SerializeField] private List<InventoryItem> _inventoryItems;

        public override void Use()
        {
            foreach (var item in _inventoryItems) 
                item.Use();
        }
    }
}
