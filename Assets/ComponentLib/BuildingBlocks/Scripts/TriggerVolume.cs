using UnityEngine;
using UnityEngine.Events;

namespace gotoandplay
{
    public class TriggerVolume : MonoBehaviour
    {
        [Header("Global event to fire")]
        public string enterEventName;
        public string exitEventName;

        public UnityEvent onTriggerEnter;
        public UnityEvent onTriggerExit;

        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(!string.IsNullOrEmpty(enterEventName))
            {
                Messenger<string>.Invoke(enterEventName, enterEventName);
            }

            onTriggerEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!string.IsNullOrEmpty(exitEventName))
            {
                Messenger<string>.Invoke(exitEventName, exitEventName);
            }

            onTriggerExit.Invoke();
        }
    }
}
