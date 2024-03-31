using UnityEngine;

namespace PraxilabsTask
{
    [CreateAssetMenu(menuName = "Inventory/Inventory Item/Weapon")]
    public class WeaponInventoryItem : InventoryItem
    {
        public override void Use()
        {
            Debug.Log($"Deal {Stat} Damage");
        }
    }
}
