using UnityEngine;

namespace gotoandplay
{
    public class FireEvent : MonoBehaviour
    {
        public string eventName;

        void Start()
        {

        }

        public void InvokeEvent()
        {
            Messenger<string>.Invoke(eventName, eventName);
        }

    }
}