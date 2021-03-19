using UnityEngine;
using UnityEngine.SceneManagement;

namespace gotoandplay
{
    public class LevelSelectController : MonoBehaviour
    {

        public static LevelSelectController Instance;
        public LevelSelectConfig levelConfig;

        public int loadedLevelIndex = -1;

        private void Awake()
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
            // TODO save key per level
            SubscribeEvents();
            Application.targetFrameRate = 60;
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        void SubscribeEvents()
        {
            Messenger.AddListener(LevelSelectConstants.EVENT_NEXT_LEVEL, OnNextLevel);
            Messenger<int>.AddListener(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, OnLevelItemPressed);
            Messenger<string>.AddListener(LevelSelectConstants.EVENT_ON_LEVELCOMPLETE, OnLevelComplete);
        }

        void UnSubscribeEvents()
        {
            Messenger.RemoveListener(LevelSelectConstants.EVENT_NEXT_LEVEL, OnNextLevel);
            Messenger<int>.RemoveListener(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, OnLevelItemPressed);
            Messenger<string>.RemoveListener(LevelSelectConstants.EVENT_ON_LEVELCOMPLETE, OnLevelComplete);
        }

        public void SetLoadedLevelIndex(int value)
        {
            loadedLevelIndex = value;
        }

        public void LevelComplete(System.Action<bool> callback)
        {
            if (loadedLevelIndex != -1)
            {
                PlayerPrefs.SetInt(levelConfig.SaveKeyPrefix + loadedLevelIndex, 1); // 1 true | 0 false
                callback.Invoke(true);
            }
            else
            {
                callback.Invoke(false);
            }
        }

        void UnlockLevel(int levelIndex)
        {
            var nextLevelKey = levelConfig.SaveKeyPrefix + levelIndex;
            PlayerPrefs.SetInt(nextLevelKey, 1);
        }

        /// <summary>
        /// event handlers
        /// </summary>
        /// <param name="levelIndex"></param>
        void OnLevelComplete(string eventName)
        {
            int nextLevelIndex = loadedLevelIndex + 1;
            UnlockLevel(nextLevelIndex);
            Debug.Log("Next level with index: " + nextLevelIndex + " unlocked!");
        }

        void OnLevelItemPressed(int levelIndex)
        {
            loadedLevelIndex = levelIndex;
            PreLoadLevel(levelIndex);
        }

        public void OnNextLevel()
        {
            SetLoadedLevelIndex(loadedLevelIndex + 1);
            PreLoadLevel(loadedLevelIndex);
        }

        public void PreLoadLevel(int value)
        {
            loadedLevelIndex = value;
            if (LevelTransition.Instance)
            {
                LevelTransition.Instance.PlayTransitionOut(() =>
                {
                    LoadLevel();
                });
            }
            else
            {
                LoadLevel();
            }
        }

        public void LoadLevel()
        {
            var value = loadedLevelIndex;

            if (levelConfig != null && value < levelConfig.levels.Count)
            {
                SceneManager.LoadScene(levelConfig.levels[value]);
            }
            else
            {
                // TODO return to level 1 if you complete all levels.
                SetLoadedLevelIndex(0);
                SceneManager.LoadScene(levelConfig.levels[0]);

                Debug.Log("This is the last level.");
            }
        }
    }

}