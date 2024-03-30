using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PraxilabsTask
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _numberOfItems;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void SetImage(Sprite icon) => _image.sprite = icon;
        public void SetNumberOfItems(int numberOfItems) => _numberOfItems.SetText(numberOfItems.ToString());
    }
}
