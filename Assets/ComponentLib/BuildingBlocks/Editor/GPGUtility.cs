using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace gotoandplay
{
    public class GPGUtility : MonoBehaviour
    {

        [MenuItem("gotoandplay/Clear Local Prefs")]
        static void ClearLocalSharedPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}