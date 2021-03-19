using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace gotoandplay
{
    public class LevelSelectView : MonoBehaviour
    {
        public Transform listParent;
        public GameObject listItemPrefab;
        
        [Space]
        public UnityEvent onLevelItemClick;

        public GameObject uiMain;
        public GameObject uiLevelSelect;

        void Start()
        {
            if (LevelSelectController.Instance)
            {
                RefreshUI();
            }

            SubscribeEvents();

            Init();
        }

        void Init()
        {
            if(LevelSelectController.Instance)
            {
                if (LevelSelectController.Instance.loadedLevelIndex == -1)
                {
                    // default to level 1 if none selected.
                    LevelSelectController.Instance.SetLoadedLevelIndex(0);
                }
            }
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        void SubscribeEvents()
        {
            Messenger<int>.AddListener(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, OnLevelItemPressed);
        }

        void UnSubscribeEvents()
        {
            Messenger<int>.RemoveListener(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, OnLevelItemPressed);
        }

        void OnLevelItemPressed(int levelIndex)
        {
            // TODO :: kinda redundant already
            //Debug.Log("Imma called yo! Level index: " + levelIndex);
            onLevelItemClick.Invoke();
        }

        void RefreshUI()
        {
            // don't initialize list if parent is not specified.
            if(listParent == null)
            {
                return;
            }

            //SetCurrentLevel(LevelSelectController.Instance.loadedLevelIndex);

            if (LevelSelectController.Instance.levelConfig)
            {
                var config = LevelSelectController.Instance.levelConfig;

                for (int i = 0; i < config.levels.Count; i++)
                {
                    var listItem = Instantiate(listItemPrefab, listParent);
                    var levelItem = listItem.GetComponent<LevelItemView>();
                    levelItem.SetLevelNo(i + 1);
                    levelItem.SetLevelIndex(i, config.SaveKeyPrefix);

                    // get level completed state :: (config.SaveKeyPrefix + i)
                    // get level stars state
                }
            }
        }

        public void ShowLevelSelect()
        {
            uiMain.SetActive(false);
            uiLevelSelect.SetActive(true);
        }

        public void ShowMainHud()
        {
            uiMain.SetActive(true);
            uiLevelSelect.SetActive(false);
        }

        public void ShowMainMenu()
        {
            if(LevelTransition.Instance)
            {
                LevelTransition.Instance.PlayTransitionOut(() =>
                {
                    SceneManager.LoadScene("mainmenu");
                });
            }
            else
            {
                SceneManager.LoadScene("mainmenu");
            }
        }
    }

}