using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gotoandplay
{
    public class ResetToPosition : MonoBehaviour
    {
        public string eventName;
        public Transform target;

        void Start()
        {
            Messenger<string>.AddListener(eventName, OnHandleEvent);
        }

        void OnHandleEvent(string eventName)
        {
            target.position = transform.position;
        }

    }
}
