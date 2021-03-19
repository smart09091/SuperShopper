using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultLayers;
using UnityEngine.GameFoundation.DefaultLayers.Persistence;
using UnityEngine.Promise;

namespace gotoandplay
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public delegate void InitializeComplete();
        public event InitializeComplete initializeComplete;
        public delegate void InventoryItemChanged(InventoryItem item);
        public event InventoryItemChanged inventoryItemChanged;

        private PersistenceDataLayer mDataLayer;
        const string PERSISTENCE_SAVE_NAME = "gotoandplay.persistence.save.game_name";    // TODO: dont forget to change the game name to your game name.
        private readonly List<InventoryItem> mInventoryItems = new List<InventoryItem>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            InitGameFoundation();
        }

        void InitGameFoundation()
        {
            mDataLayer = new PersistenceDataLayer(new LocalPersistence(PERSISTENCE_SAVE_NAME, new JsonDataSerializer()));
            GameFoundation.Initialize(mDataLayer, OnGameFoundationInit, Debug.LogError);
        }

        void OnGameFoundationInit()
        {
            InventoryManager.itemAdded += OnInventoryItemChanged;
            InventoryManager.itemRemoved += OnInventoryItemChanged;

            if (initializeComplete != null)
            {
                Debug.Log("we have listeners.");
                initializeComplete.Invoke();
            }
            else
            {
                Debug.Log("we have no listeners.");
            }
        }

        void OnInventoryItemChanged(InventoryItem item)
        {
            if (inventoryItemChanged != null)
            {
                inventoryItemChanged.Invoke(item);
            }
        }

        /// remove item from the inventory (sell, deleted action)
        public void RemoveItem(InventoryItem item)
        {
            if (item != null)
            {
                InventoryManager.RemoveItem(item);
            }
        }

        /// <summary>
        /// START of Save/Load methods
        /// </summary>
        public void Save()
        {
            // Deferred is a struct that helps you track the progress of an asynchronous operation of Game Foundation.
            Deferred saveOperation = mDataLayer.Save();

            // Check if the operation is already done.
            if (saveOperation.isDone)
            {
                LogSaveOperationCompletion(saveOperation);
            }
            else
            {
                StartCoroutine(WaitForSaveCompletion(saveOperation));
            }
        }

        public void Load()
        {
            // Don't forget to stop listening to events before un-initializing.
            InventoryManager.itemAdded -= OnInventoryItemChanged;
            InventoryManager.itemRemoved -= OnInventoryItemChanged;

            GameFoundation.Uninitialize();

            GameFoundation.Initialize(mDataLayer, OnGameFoundationInit, Debug.LogError);
        }

        private static void LogSaveOperationCompletion(Deferred saveOperation)
        {
            // Check if the operation was successful.
            if (saveOperation.isFulfilled)
            {
                Debug.Log("Saved!");
            }
            else
            {
                Debug.LogError($"Save failed! Error: {saveOperation.error}");
            }
        }

        private static IEnumerator WaitForSaveCompletion(Deferred saveOperation)
        {
            // Wait for the operation to complete.
            yield return saveOperation.Wait();

            LogSaveOperationCompletion(saveOperation);
        }
        /// <summary>
        /// END of Save/Load methods
        /// </summary>

        /// <summary>
        /// Sample add item.
        /// </summary>
        public void SampleAddItem()
        {
            InventoryManager.CreateItem("sword");
        }

        /// <summary>
        /// sample loading of currency from the database
        /// </summary>
        void LoadCurrencyValues()
        {
            // get local currency, coin, gems
            var coin = GameFoundation.catalogs.currencyCatalog.FindItem("coin"); // coin, gem
            var coinQty = WalletManager.GetBalance(coin);

            var gem = GameFoundation.catalogs.currencyCatalog.FindItem("gem");
            var gemQty = WalletManager.GetBalance(gem);

            Debug.Log("Coins: " + coinQty + " | Gems: " + gemQty);
        }

        /// sample call to remove all inventory items
        void RemoveAllItems()
        {
            InventoryManager.RemoveAllItems();
        }

    }
}
