using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace gotoandplay
{
    public class LevelTransition : MonoBehaviour
    {
        public static LevelTransition Instance;

        public Animator mAnimator;

        public UnityEvent onTransitionInComplete;
        public UnityEvent onTransitionOutComplete;

        int loadLevelIndex = -1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            loadLevelIndex = -1;    // reset state
            PlayTransitionIn();
        }

        public void PlayTransitionIn()
        {
            if(mAnimator)
            {
                mAnimator.SetTrigger("enter");
            }
        }

        public void PlayTransitionOut()
        {
            if (mAnimator)
            {
                mAnimator.SetTrigger("exit");
            }
        }

        public void PlayTransitionOut(System.Action callback, float delay = 0.5f)
        {
            if (mAnimator)
            {
                mAnimator.SetTrigger("exit");
            }

            StartCoroutine(postDelayed(delay, callback));
        }

        public void OnTransitionInAnimComplete()
        {
            if(onTransitionInComplete != null)
            {
                onTransitionInComplete.Invoke();
            }
        }

        public void OnTransitionOutAnimComplete()
        {
            if (onTransitionOutComplete != null)
            {
                onTransitionOutComplete.Invoke();
            }
        }

        IEnumerator postDelayed(float delay, System.Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }
    }
}