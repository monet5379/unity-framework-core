using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monet5379
{
    internal static class SingletonMonoBehaviourRegistry
    {
        static readonly List<Action> _resets = new();

        public static void RegisterReset(Action reset)
        {
            _resets.Add(reset);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetAll()
        {
            for (int i = 0; i < _resets.Count; i++)
            {
                _resets[i]();
            }
        }
    }
}
