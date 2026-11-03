using System;
using System.Collections.Generic;
using System.Reflection;
using ConditionalLog;
using UnityEngine;

namespace Monet5379
{
    public static class GlobalEventBus<TKey> where TKey : struct, Enum
    {
        sealed class Slot
        {
            public Slot(Type signature)
            {
                Signature = signature;
                Callbacks = new List<CallbackEntry>();
            }

            public Type Signature { get; }

            public List<CallbackEntry> Callbacks { get; }
        }

        sealed class CallbackEntry
        {
            public CallbackEntry(Delegate callback)
            {
                Method = callback.Method;
                if (callback.Target != null)
                {
                    Target = new WeakReference(callback.Target);
                }
            }

            public MethodInfo Method { get; }

            public WeakReference Target { get; }

            public bool IsDead => Target != null && !Target.IsAlive;
        }

        static readonly Dictionary<TKey, Slot> _slots = new();

        static GlobalEventBus()
        {
            GlobalEventBusRegistry.RegisterClear(Clear);
        }

        static void Clear()
        {
            _slots.Clear();
        }

        public static void Register<TCallback>(TKey type, TCallback callback)
            where TCallback : Delegate
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            Type signature = typeof(TCallback);
            if (!_slots.TryGetValue(type, out Slot slot))
            {
                slot = new Slot(signature);
                _slots.Add(type, slot);
            }
            else if (slot.Signature != signature)
            {
                LogSignatureMismatch(type);
                return;
            }

            if (Find(slot.Callbacks, callback) >= 0)
            {
                return;
            }

            slot.Callbacks.Add(new CallbackEntry(callback));
        }

        public static void Unregister<TCallback>(TKey type, TCallback callback)
            where TCallback : Delegate
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (!_slots.TryGetValue(type, out Slot slot))
            {
                return;
            }

            if (slot.Signature != typeof(TCallback))
            {
                LogSignatureMismatch(type);
                return;
            }

            int index = Find(slot.Callbacks, callback);
            if (index < 0)
            {
                return;
            }

            slot.Callbacks.RemoveAt(index);
            if (slot.Callbacks.Count == 0)
            {
                _slots.Remove(type);
            }
        }

        public static bool Send(TKey type, Type signature, object[] args)
        {
            if (!_slots.TryGetValue(type, out Slot slot))
            {
                return false;
            }

            if (slot.Signature != signature)
            {
                GameLog.Warning(CoreEventLogTags.Global, "Global Event 시그니처가 등록과 다릅니다. {0}", type);
                return false;
            }

            List<CallbackEntry> callbacks = slot.Callbacks;
            if (callbacks.Count == 0)
            {
                return false;
            }

            bool invoked = false;
            CallbackEntry[] copy = callbacks.ToArray();
            for (int i = 0; i < copy.Length; i++)
            {
                CallbackEntry entry = copy[i];
                if (entry.IsDead)
                {
                    callbacks.Remove(entry);
                    continue;
                }

                object target = entry.Target == null ? null : entry.Target.Target;
                GameLog.Info(
                    CoreEventLogTags.Global,
                    "콜백({0}) 호출: {1}.{2}",
                    type,
                    entry.Method.DeclaringType,
                    entry.Method.Name);
                try
                {
                    invoked = true;
                    entry.Method.Invoke(target, args);
                }
                catch (TargetInvocationException exception)
                {
                    Debug.LogException(exception.InnerException ?? exception);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            if (callbacks.Count == 0)
            {
                _slots.Remove(type);
            }

            return invoked;
        }

        static int Find(List<CallbackEntry> callbacks, Delegate callback)
        {
            for (int i = 0; i < callbacks.Count; i++)
            {
                CallbackEntry entry = callbacks[i];
                if (entry.Method != callback.Method)
                {
                    continue;
                }

                if (callback.Target == null)
                {
                    if (entry.Target == null)
                    {
                        return i;
                    }

                    continue;
                }

                if (entry.Target != null && ReferenceEquals(entry.Target.Target, callback.Target))
                {
                    return i;
                }
            }

            return -1;
        }

        static void LogSignatureMismatch(TKey type)
        {
            GameLog.Warning(CoreEventLogTags.Global, "Global Event에 다른 시그니처가 이미 등록되어 있습니다. {0}", type);
        }
    }
}
