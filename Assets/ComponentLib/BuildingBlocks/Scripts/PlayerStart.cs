using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

namespace gotoandplay
{
    public class PlayerStart : MonoBehaviour
    {
        [Header("The player object to spawn")]
        public GameObject playerPrefab;

        [Header("Delay before actual spawn")]
        public float spawnDelay;

        [Header("Should destroy spawner on create?")]
        public bool autoDestroy;

        void Start()
        {
            Invoke("Init", spawnDelay);
        }

        void Init()
        {
            var player = LeanPool.Spawn(playerPrefab);

            // check if we have an attached reset to position component
            var resetToPosition = GetComponent<ResetToPosition>();
            if(resetToPosition)
            {
                resetToPosition.target = player.transform;
            }

            // should we remove this gameobject once the task is done?
            if (autoDestroy)
            {
                LeanPool.Despawn(gameObject);
            }
        }

    }
}
