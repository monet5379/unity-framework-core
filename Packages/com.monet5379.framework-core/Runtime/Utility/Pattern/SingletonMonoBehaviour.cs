using ConditionalLog;
using UnityEngine;

namespace Monet5379
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new();
        private static bool _applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    LogWarningApplicationQuit();
                    return null;
                }

                lock (_lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = Object.FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        CreateInstance();
                    }

                    return _instance;
                }
            }
        }

        public static void CreateInstance()
        {
            if (_instance != null)
            {
                LogUsingExistingInstance();
                return;
            }

            GameObject singletonGO = new($"@{typeof(T)}");
            _instance = singletonGO.AddComponent<T>();
            DontDestroyOnLoad(singletonGO);

            LogCreateSingleton(singletonGO);
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);

                GameLog.Progress($"싱글톤을 설정합니다: {this.GetHierarchyName()}");
            }
            else if (_instance != this)
            {
                GameLog.Warning($"중복된 싱글톤 인스턴스가 감지되어 파괴됩니다: {this.GetHierarchyName()}");
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        #region Logging

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void LogCreateSingleton(GameObject go)
        {
            GameLog.Progress($"[Singleton] {go.GetHierarchyPath()} 생성됨. DontDestroyOnLoad 적용.");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void LogUsingExistingInstance()
        {
            GameLog.Progress($"[Singleton] 이미 생성된 인스턴스를 사용합니다: {_instance.GetHierarchyPath()}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void LogWarningApplicationQuit()
        {
            GameLog.Warning($"[Singleton] 애플리케이션 종료 상태이므로 싱글톤 인스턴스를 반환하지 않습니다. Type: {typeof(T)}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void LogWarningMultipleInstances()
        {
            GameLog.Warning($"[Singleton] 여러 개의 인스턴스가 존재합니다. 싱글톤은 하나만 존재해야 합니다. Type: {typeof(T)}");
        }

        #endregion Logging
    }
}