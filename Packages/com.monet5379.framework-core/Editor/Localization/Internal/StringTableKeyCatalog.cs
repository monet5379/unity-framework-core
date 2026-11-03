using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Monet5379.Inspectors.Internal
{
    internal static class StringTableKeyCatalog
    {
        internal const string NoneChoiceLabel = "(None)";

        private const string StringUiAssetPath = "Assets/Addressables/JSON/StringUI.json";
        private const string StringDialogueAssetPath = "Assets/Addressables/JSON/StringDialogue.json";

        [Serializable]
        private sealed class KeyRow
        {
            public string Key;
        }

        [Serializable]
        private sealed class KeyRowsWrapper
        {
            public KeyRow[] Items;
        }

        public static IReadOnlyList<string> GetSortedKeys()
        {
            HashSet<string> seen = new(StringComparer.Ordinal);
            List<string> keys = new();

            CollectKeys(StringUiAssetPath, seen, keys);
            CollectKeys(StringDialogueAssetPath, seen, keys);

            keys.Sort(StringComparer.Ordinal);
            return keys;
        }

        public static List<string> BuildChoices(string currentValue, IReadOnlyList<string> sortedKeys)
        {
            List<string> choices = new() { NoneChoiceLabel };

            if (!string.IsNullOrEmpty(currentValue) && !ContainsKey(sortedKeys, currentValue))
            {
                choices.Add(currentValue);
            }

            for (int i = 0; i < sortedKeys.Count; i++)
            {
                choices.Add(sortedKeys[i]);
            }

            return choices;
        }

        private static bool ContainsKey(IReadOnlyList<string> sortedKeys, string key)
        {
            for (int i = 0; i < sortedKeys.Count; i++)
            {
                if (sortedKeys[i] == key)
                {
                    return true;
                }
            }

            return false;
        }

        public static string StoredValueForChoiceLabel(string choiceLabel)
        {
            return choiceLabel == NoneChoiceLabel ? string.Empty : choiceLabel;
        }

        private static void CollectKeys(string assetPath, HashSet<string> seen, List<string> keys)
        {
            TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);
            if (asset == null || string.IsNullOrEmpty(asset.text))
            {
                return;
            }

            KeyRow[] rows = ParseRows(asset.text);
            for (int i = 0; i < rows.Length; i++)
            {
                string key = rows[i]?.Key;
                if (string.IsNullOrEmpty(key) || !seen.Add(key))
                {
                    continue;
                }

                keys.Add(key);
            }
        }

        private static KeyRow[] ParseRows(string json)
        {
            string wrapped = "{\"Items\":" + json + "}";
            KeyRowsWrapper wrapper = JsonUtility.FromJson<KeyRowsWrapper>(wrapped);
            return wrapper?.Items ?? Array.Empty<KeyRow>();
        }
    }
}