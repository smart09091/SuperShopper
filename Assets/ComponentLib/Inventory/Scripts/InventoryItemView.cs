using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;

namespace gotoandplay
{
    public class InventoryItemView : MonoBehaviour
    {
        InventoryItem itemData;

        public Text mLabel;
        public Text actionLabel;
        public Image mIcon;
        public string iconKey = "item_icon";
        public bool useIconNativeSize;

        void Start()
        {
        }

        public void SetItem(InventoryItem item) {
            itemData = item;
        }

        public InventoryItem GetItem() {
            return itemData;
        }

        public void SetLabel(string value)
        {
            mLabel.text = value;
        }

        public void SetActionLabel(string value)
        {
            actionLabel.text = value;
        }

        public void SetIcon(Sprite sprite)
        {
            mIcon.sprite = sprite;
            if (useIconNativeSize)
                mIcon.SetNativeSize();
        }

        public void OnItemClick()
        {
            Messenger<InventoryItemView>.Invoke("InventoryItemClick", this);
        }
    }

}