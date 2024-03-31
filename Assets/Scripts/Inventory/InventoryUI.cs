using UnityEngine;
using System.Collections.Generic;
using System;

namespace PraxilabsTask
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private int _maxSelectableItems = 4;

        [Header("UI")]
        [SerializeField] private Inventory _premierInventory;
        [SerializeField] private Inventory _secondaryInventory;
        [SerializeField] private InventoryUI _secondaryInventoryIU;
        [SerializeField] private InventoryItemUI _inventoryItemUIPrefab;
        [SerializeField] private Transform _inventoryItemContainer;

        private readonly List<InventoryItem> _selectedItems = new();

        private void Start()
        {
            InitializeInventoryMenu();
        }

        private void InitializeInventoryMenu()
        {
            _premierInventory.CopyStartItems();
            UpdateInventoryMenu();
        }

        public void UpdateInventoryMenu()
        {
            foreach (Transform item in _inventoryItemContainer)
                Destroy(item.gameObject);

            foreach (var item in _premierInventory.AvailableItems)
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
            _premierInventory.RemoveItem(item);
        });

        /// <summary>
        /// Called from "Drop Button" to use the selected items
        /// </summary>
        public void DropOne() => ExecuteActionWithSelectedItems(item => _premierInventory.RemoveItem(item));

        /// <summary>
        /// Called from "Drop All Button" to use the selected items
        /// </summary>
        public void DropAll() => ExecuteActionWithSelectedItems(item => _premierInventory.RemoveAllItems(item));

        /// <summary>
        /// Called from "Move Button" to use the selected items
        /// </summary>
        public void Move() 
        {
            ExecuteActionWithSelectedItems(item =>
            {
                _secondaryInventory.Add(item, _premierInventory.NumberOfItemsOf(item));
                _premierInventory.RemoveAllItems(item);
            });

            _secondaryInventoryIU.UpdateInventoryMenu();
        }
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
    }
}