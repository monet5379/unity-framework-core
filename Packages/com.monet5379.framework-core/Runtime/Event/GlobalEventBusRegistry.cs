using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monet5379
{
    internal static class GlobalEventBusRegistry
    {
        static readonly List<Action> _clearActions = new();

        public static void RegisterClear(Action clear)
        {
            _clearActions.Add(clear);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Reset()
        {
            for (int i = 0; i < _clearActions.Count; i++)
            {
                _clearActions[i]();
            }
        }
    }
}
