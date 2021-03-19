using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gotoandplay;

public class LevelSelectDemo : MonoBehaviour
{
    void Start()
    {
        
    }

    /// <summary>
    /// use this to unlock a level once you complete it.
    /// </summary>
    void UnlockLevel()
    {
        Messenger<string>.Invoke(LevelSelectConstants.EVENT_ON_LEVELCOMPLETE, LevelSelectConstants.EVENT_ON_LEVELCOMPLETE);
    }

    /// <summary>
    /// use this when the player press a certain level in the level select UI.
    /// </summary>
    /// <param name="levelIndexToLoad"></param>
    void OnLevelItemClick(int levelIndexToLoad)
    {
        Messenger<int>.Invoke(LevelSelectConstants.EVENT_LEVEL_ITEM_CLICK, levelIndexToLoad);
    }

    /// <summary>
    /// use this if you have a button that lets you go directly to the next level.
    /// </summary>
    void OnNextLevel()
    {
        Messenger.Invoke(LevelSelectConstants.EVENT_NEXT_LEVEL);
    }
}
