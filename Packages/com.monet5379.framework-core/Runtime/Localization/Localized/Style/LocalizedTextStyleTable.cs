using System;
using UnityEngine;

namespace Monet5379
{
    [CreateAssetMenu(fileName = "LocalizedTextStyleTable", menuName = "Monet5379/UI/Localized Text Style Table")]
    public class LocalizedTextStyleTable : ScriptableObject
    {
        [SerializeField]
        private LocalizedTextStyleEntry[] _entries = Array.Empty<LocalizedTextStyleEntry>();

        public bool TryGetStyle(LocalizedTextUsage usage, out LocalizedTextStyleEntry entry)
        {
            entry = default;
            if (usage == LocalizedTextUsage.None || _entries == null)
            {
                return false;
            }

            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].Usage == usage)
                {
                    entry = _entries[i];
                    return true;
                }
            }

            return false;
        }
    }
}
