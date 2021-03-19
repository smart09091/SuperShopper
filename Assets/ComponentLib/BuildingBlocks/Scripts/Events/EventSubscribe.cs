using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace gotoandplay
{
    public class EventSubscribe : MonoBehaviour
    {
        public EventSubModel[] events;

        void Start()
        {
            Init();
        }

        void Init()
        {
            for(int i = 0; i < events.Length; i++)
            {
                EventSubModel eventSubModel = events[i];
                Messenger<string>.AddListener(eventSubModel.eventName, OnHandleEvent);
            }
        }

        void OnHandleEvent(string eventName)
        {
            for(int i = 0; i < events.Length; i++)
            {
                if(eventName.Equals(events[i].eventName))
                {
                    events[i].onEventTrigger.Invoke();
                }
            }
        }
    }

    [System.Serializable]
    public class EventSubModel
    {
        public string eventName;
        public UnityEvent onEventTrigger;
    }
}