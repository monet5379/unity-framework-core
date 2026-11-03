using System;
using UnityEngine;

namespace Monet5379
{
    public static class LocalizationContext
    {
        public static ILocalizationSource Source { get; private set; }

        public static event Action LanguageChanged;

        public static void Register(ILocalizationSource source)
        {
            Source = source;
        }

        public static void RaiseLanguageChanged()
        {
            LanguageChanged?.Invoke();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            Source = null;
            LanguageChanged = null;
        }
    }
}
