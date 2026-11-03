using System.Collections;
using UnityEngine;

namespace Monet5379
{
    public abstract class XBehaviour : MonoBehaviour
    {
        private bool _isDestroyed;

        protected bool IsDestroyed => _isDestroyed;

        public bool IsStarted { get; private set; }

        public virtual bool ActiveInHierarchy
        {
            get
            {
                if (!this)
                {
                    return false;
                }

                return gameObject.activeInHierarchy;
            }
        }

        [FoldoutGroup("Buttons", 999)]
        [Button("Auto Get Components", ButtonSizes.Medium)]
        public virtual void AutoGetComponents()
        {
        }

        [FoldoutGroup("Buttons", 999)]
        [Button("Auto Setting", ButtonSizes.Medium)]
        public virtual void AutoSetting()
        {
        }

#if UNITY_EDITOR

        public void SetGameObjectName(string name)
        {
            if (this)
            {
                gameObject.name = name;
            }
        }

#endif

        private void Start()
        {
            IsStarted = true;
            OnStart();
        }

        private void OnEnable()
        {
            OnEnabled();
        }

        private void OnDisable()
        {
            OnDisabled();
            StopAllCoroutines();
        }

        protected virtual void OnStart()
        {
            RegisterGlobalEvent();
        }

        protected virtual void OnEnabled()
        {
            if (IsStarted)
            {
                RegisterGlobalEvent();
            }
        }

        protected virtual void OnDisabled()
        {
            UnregisterGlobalEvent();
        }

        protected virtual void RegisterGlobalEvent()
        {
        }

        protected virtual void UnregisterGlobalEvent()
        {
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }

        public Coroutine StartXCoroutine(IEnumerator routine)
        {
            if (ActiveInHierarchy)
            {
                return StartCoroutine(routine);
            }

            return null;
        }

        public void StopXCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = null;
        }
    }
}