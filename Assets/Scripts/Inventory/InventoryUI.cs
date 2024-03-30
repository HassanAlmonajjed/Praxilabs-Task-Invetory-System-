using UnityEngine;
using System.Collections.Generic;

namespace PraxilabsTask
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryItemUI _inventoryItemUIPrefab;
        [SerializeField] private Transform _inventoryItemContainer;
        [SerializeField] private int _maxSelectableItems = 4;

        private readonly List<InventoryItem> _selectedItems = new();

        private void Start()
        {
            InitializeInventoryMenu();
        }

        private void InitializeInventoryMenu()
        {
            _inventory.CopyStartItems();
            foreach (var item in _inventory.AvailableItems)
            {
                var inventoryItemUI = Instantiate(_inventoryItemUIPrefab, _inventoryItemContainer);
                inventoryItemUI.Setup(item.Key, item.Value);
                inventoryItemUI.OnItemSelected += ItemClicked;
            }
        }

        public void ItemClicked(InventoryItemUI inventoryItemUI)
        {
            if (inventoryItemUI.IsSelected)
            {
                _selectedItems.Remove(inventoryItemUI.InventoryItem);
                inventoryItemUI.Toggle();
            }
            else
            {
                if (_selectedItems.Count >= _maxSelectableItems)
                {
                    Debug.Log("Max possible items reached");
                    return;
                }

                _selectedItems.Add(inventoryItemUI.InventoryItem);
                inventoryItemUI.Toggle();
            }
            
        }

        public void ItemUnSelected(InventoryItem inventoryItem)
        {

        }
    }
}