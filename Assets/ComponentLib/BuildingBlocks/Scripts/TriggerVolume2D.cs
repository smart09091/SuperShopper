using UnityEngine;
using UnityEngine.Events;

namespace gotoandplay
{
    public class TriggerVolume2D : MonoBehaviour
    {
        [Header("Global event to fire")]
        public string enterEventName;
        public string exitEventName;

        public UnityEvent onTriggerEnter;
        public UnityEvent onTriggerExit;

        void Start()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(enterEventName))
            {
                Messenger<string>.Invoke(enterEventName, enterEventName);
            }

            onTriggerEnter.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(exitEventName))
            {
                Messenger<string>.Invoke(exitEventName, exitEventName);
            }

            onTriggerExit.Invoke();
        }
    }
}