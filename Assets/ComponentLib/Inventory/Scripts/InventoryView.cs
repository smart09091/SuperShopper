using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.GameFoundation;

namespace gotoandplay
{
    public class InventoryView : MonoBehaviour
    {
        public string viewTag = "equipment";
        public enum InventoryViewMode
        {
            EQUIP,
            CONSUME,
            SELL
        }

        public InventoryViewMode viewMode = InventoryViewMode.EQUIP;

        public GameObject itemPrefab;

        ScrollRect mScrollRect;
        [Header("GameObject Field")]
        [Space]
        [Tooltip("Optionally allows specifying an alternate parent container for automatically rendered Transaction Item Prefabs. If not defined, StoreView's Transform will be the parent by default.")]
        public Transform itemContainer;

        [System.Serializable]
        public class ItemEquipEvent : UnityEvent<InventoryItemView> { }
        public ItemEquipEvent onEquipItem;

        [System.Serializable]
        public class ItemSellEvent : UnityEvent<InventoryItemView> { }
        public ItemSellEvent onSellItem;

        private readonly List<InventoryItem> mInventoryItems = new List<InventoryItem>();
        private List<InventoryItemView> mInventoryItemVIews = new List<InventoryItemView>();

        void Start()
        {
            SubscribeEvents();
        }

        void Init()
        {
            mScrollRect = GetComponentInChildren<ScrollRect>();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        void SubscribeEvents()
        {
            Inventory.Instance.initializeComplete += OnGameFoundationInit;
            Inventory.Instance.inventoryItemChanged += OnInventoryItemChanged;
            Messenger<InventoryItemView>.AddListener("InventoryItemClick", OnInventoryItemClick);
        }

        void UnSubscribeEvents()
        {
            Inventory.Instance.initializeComplete -= OnGameFoundationInit;
            Inventory.Instance.inventoryItemChanged -= OnInventoryItemChanged;
            Messenger<InventoryItemView>.RemoveListener("InventoryItemClick", OnInventoryItemClick);
        }

        void OnGameFoundationInit()
        {
            RefreshUI();
        }

        void OnInventoryItemChanged(InventoryItem item)
        {
            RefreshUI();
            // InitInventoryItem(item);
        }

        void RefreshUI()
        {
            /// clear all inventory items
            for (int i = 0; i < mInventoryItemVIews.Count; i++)
            {
                Destroy(mInventoryItemVIews[i].gameObject);
            }

            /// reset the array if its populated.
            if (mInventoryItemVIews.Count > 0)
            {
                mInventoryItemVIews.Clear();
            }

            // load items by tag, or load all if no tag specified
            if (!string.IsNullOrEmpty(viewTag))
            {
                InventoryManager.FindItemsByTag(viewTag, mInventoryItems);
            }
            else
            {
                /// fallback
                InventoryManager.GetItems(mInventoryItems);
            }

            for (int i = 0; i < mInventoryItems.Count; i++)
            {
                var inventoryItem = mInventoryItems[i];
                InitInventoryItem(inventoryItem);
            }
        }

        /// <summary>
        /// this creates the inventory item in the UI.
        /// </summary>
        void InitInventoryItem(InventoryItem item)
        {
            var newItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, itemContainer);
            var itemData = newItem.GetComponent<InventoryItemView>();
            if (itemData)
            {
                InventoryItemDefinition itemDefinition = GameFoundation.catalogs.inventoryCatalog.FindItem(item.definition.key);
                Sprite itemSprite = itemDefinition.GetDetail<AssetsDetail>()?.GetAsset<Sprite>(itemData.iconKey);

                itemData.SetLabel(item.definition.key);
                itemData.SetIcon(itemSprite);
                itemData.SetActionLabel(GetActionLabelByViewMode());
                itemData.SetItem(item);
                mInventoryItemVIews.Add(itemData);
            }
        }

        string GetActionLabelByViewMode()
        {
            switch (viewMode)
            {
                case InventoryViewMode.EQUIP:
                    return "Equip";
                case InventoryViewMode.SELL:
                    return "Sell";
            }

            return "";
        }

        /// <summary>
        /// this is called when the action button on the inventory item is clicked.
        /// </summary>
        void OnInventoryItemClick(InventoryItemView item)
        {
            switch (viewMode)
            {
                case InventoryViewMode.EQUIP:
                    onEquipItem.Invoke(item);
                    break;
                case InventoryViewMode.SELL:
                    onSellItem.Invoke(item);
                    break;
            }
        }

        public void SetViewMOde(InventoryViewMode mode)
        {
            viewMode = mode;
            RefreshUI();
        }
    }
}