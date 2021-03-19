using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gotoandplay
{
    public class LevelItemView : MonoBehaviour
    {
        public Text levelText;
        public int levelIndex;
        string levelKey = "";

        public GameObject lockedState;
        public GameObject unlockedState;

        bool isUnlocked = false;

        void Start()
        {

        }

        public void SetLevelNo (int value) {
            levelText.text = "" + value;
        }

        public void SetLevelIndex(int value, string saveKey) {
            levelIndex = value;
            levelKey = saveKey + value;

            var unlockValue = PlayerPrefs.GetInt(levelKey, 0);
            isUnlocked = levelIndex == 0 || unlockValue == 1;

            RefreshUI();
        }

        public void OnItemPressed() {
            if(isUnlocked)
            {
                Debug.Log("Loading level wiht index: " + levelIndex);
                Messenger<int>.Invoke(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, levelIndex);
            } else
            {
                Debug.Log("This level is not yet unlocked!");
            }
        }

        void RefreshUI ()
        {
            lockedState.SetActive(!isUnlocked);
            unlockedState.SetActive(isUnlocked);
        }
    }

}