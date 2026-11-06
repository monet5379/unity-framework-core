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
        [Button("Auto Get Components", ButtonSizes.Medium, HideUnlessOverridden = true)]
        public virtual void AutoGetComponents()
        {
            // 플레이하지 않을 때 에디터의 버튼을 통해 불리는 에디터 전용 함수.
            // 컴포넌트를 캐싱할 때만 사용한다.
        }

        [FoldoutGroup("Buttons", 999)]
        [Button("Auto Add Components", ButtonSizes.Medium, HideUnlessOverridden = true)]
        public virtual void AutoAddComponents()
        {
            AutoGetComponents();

            // 플레이하지 않을 때 에디터의 버튼을 통해 불리는 에디터 전용 함수.
            // 컴포넌트를 추가할 때만 사용한다.
            // 캐싱하는 변수가 비어있을 떄만 추가한다.
        }

        [FoldoutGroup("Buttons", 999)]
        [Button("Auto Setting", ButtonSizes.Medium, HideUnlessOverridden = true)]
        public virtual void AutoSetting()
        {
            // 플레이하지 않을 때 에디터의 버튼을 통해 불리는 에디터 전용 함수.
            // 캐싱된 변수의 초기값을 설정한다.
        }

        [FoldoutGroup("Buttons", 999)]
        [Button("Auto Naming", ButtonSizes.Medium, HideUnlessOverridden = true)]
        public virtual void AutoNaming()
        {
            // 플레이하지 않을 때 에디터의 버튼을 통해 불리는 에디터 전용 함수.
            // 오브젝트의 이름을 설정한다. SetGameObjectName() 함수를 통해 설정한다.
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
                coroutine = null;
            }
        }
    }
}
