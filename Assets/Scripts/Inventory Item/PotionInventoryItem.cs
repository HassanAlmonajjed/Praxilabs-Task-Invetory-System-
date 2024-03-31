using UnityEngine;

namespace PraxilabsTask
{
    [CreateAssetMenu(menuName = "Inventory/Inventory Item/Potion")]
    public class PotionInventoryItem : InventoryItem
    {
        public override void Use()
        {
            Debug.Log($"Recover {Stat} Health");
        }
    }
}
