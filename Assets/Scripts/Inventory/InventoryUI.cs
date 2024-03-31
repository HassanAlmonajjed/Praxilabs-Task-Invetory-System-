using UnityEngine;
using System.Collections.Generic;
using System;

namespace PraxilabsTask
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Inventory _otherInventory;
        [SerializeField] private InventoryUI _otherInventoryIU;
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
            UpdateInventoryMenu();
        }

        public void UpdateInventoryMenu()
        {
            foreach (Transform item in _inventoryItemContainer)
                Destroy(item.gameObject);

            foreach (var item in _inventory.AvailableItems)
                DrawItem(item.Key, item.Value);
        }

        private void DrawItem(InventoryItem item, int number)
        {
            var inventoryItemUI = Instantiate(_inventoryItemUIPrefab, _inventoryItemContainer);
            inventoryItemUI.Setup(item, number);
            inventoryItemUI.OnItemSelected += ItemClicked;
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

        /// <summary>
        /// Called from "Use Button" to use the selected items
        /// </summary>
        public void Use() => ExecuteActionWithSelectedItems(item => 
        {
            item.Use();
            _inventory.RemoveItem(item);
        });

        public void DropOne() => ExecuteActionWithSelectedItems(item => _inventory.RemoveItem(item));

        public void DropAll() => ExecuteActionWithSelectedItems(item => _inventory.RemoveAllItems(item));

        private void ExecuteActionWithSelectedItems(Action<InventoryItem> action)
        {
            if (_selectedItems.Count == 0)
            {
                Debug.Log("Select at least one item to use");
                return;
            }

            foreach (var item in _selectedItems)
                action(item);

            UpdateInventoryMenu();

            _selectedItems.Clear();
        }

        public void Move() => ExecuteActionWithSelectedItems(item => 
        {
            _otherInventory.Add(item, _inventory.NumberOfItemsOf(item));
            _otherInventoryIU.UpdateInventoryMenu();

            _inventory.RemoveAllItems(item);
        });
    }
}