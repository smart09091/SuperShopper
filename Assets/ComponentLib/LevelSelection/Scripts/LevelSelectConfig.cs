using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSelectConfig", menuName = "LevelSelect/LevelSelectConfig", order = 0)]
public class LevelSelectConfig : ScriptableObject
{
    public string SaveKeyPrefix = "default.key.prefix_";
    public List<SceneReference> levels = new List<SceneReference>();
}