using UnityEngine;

namespace PraxilabsTask
{
    [CreateAssetMenu(menuName = "Inventory/Inventory Item/Armor")]
    public class ArmorInventoryItem : InventoryItem
    {
        public override void Use()
        {
            Debug.Log($"Player Armor increased by {Stat}");
        }
    }
}