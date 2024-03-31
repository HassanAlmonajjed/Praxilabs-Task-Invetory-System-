using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace PraxilabsTask
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] private Image _IconImage;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Sprite _highlightFrame;
        [SerializeField] private Sprite _normalFrame;
        [SerializeField] private TextMeshProUGUI _numberOfItems;

        private Button _button;
        public bool IsSelected { get; private set; }
        public InventoryItem InventoryItem { get; set; }

        public event Action<InventoryItemUI> OnItemSelected;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnItemClicked);
        }

        public void Setup(InventoryItem inventoryItem, int numberOfItems)
        {
            _IconImage.sprite = inventoryItem.Icon;
            _numberOfItems.SetText(numberOfItems.ToString());
            InventoryItem = inventoryItem;
        }

        private void OnItemClicked() => OnItemSelected?.Invoke(this);

        public void Toggle()
        {
            if (IsSelected)
                UnSelect();
            else
                Select();
        }

        private void Select()
        {
            IsSelected = true;
            Highlight();
        }

        private void UnSelect()
        {
            IsSelected = false;
            UnHighlight();
        }

        private void Highlight() => _frameImage.sprite = _highlightFrame;
        private void UnHighlight() => _frameImage.sprite = _normalFrame;
    }
}
