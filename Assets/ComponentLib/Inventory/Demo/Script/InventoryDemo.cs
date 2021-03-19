using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class InventoryDemo : MonoBehaviour
    {
        public InventoryView inventoryView;

        void Start()
        {
            inventoryView.onSellItem.AddListener(RemoveItem);
        }

        private void OnDestroy()
        {
            inventoryView.onSellItem.RemoveListener(RemoveItem);
        }

        public void SetModeEquip()
        {
            inventoryView.SetViewMOde(InventoryView.InventoryViewMode.EQUIP);
        }

        public void SetModeSell()
        {
            inventoryView.SetViewMOde(InventoryView.InventoryViewMode.SELL);
        }

        public void RemoveItem(InventoryItemView item)
        {
            Inventory.Instance.RemoveItem(item.GetItem());
        }
    }
}
