using UnityEngine;

namespace PraxilabsTask
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryItemUI _inventoryItemUIPrefab;
        [SerializeField] private Transform _inventoryItemContainer;

        private void Start()
        {
            _inventory.CopyStartItems();
            foreach (var item in _inventory.AvailableItems)
            {
                var inventoryItemUI = Instantiate(_inventoryItemUIPrefab, _inventoryItemContainer);
                inventoryItemUI.SetImage(item.Key.Icon);
                inventoryItemUI.SetNumberOfItems(item.Value);
            }
        }
    }
}