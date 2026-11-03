using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Monet5379;

namespace Monet5379.Inspectors.Internal
{
    internal sealed class FieldAttributeMeta
    {
        public FieldAttributeMeta(
            FieldInfo field,
            int declarationIndex,
            FoldoutGroupAttribute foldoutGroup,
            TitleAttribute title,
            LabelTextAttribute labelText,
            SuffixLabelAttribute suffixLabel,
            ReadOnlyAttribute readOnly,
            InfoBoxAttribute[] infoBoxes,
            ShowIfAttribute[] showIf,
            EnableIfAttribute[] enableIf,
            DisableIfAttribute[] disableIf,
            GUIColorAttribute guiColor)
        {
            Field = field;
            DeclarationIndex = declarationIndex;
            FoldoutGroup = foldoutGroup;
            Title = title;
            LabelText = labelText;
            SuffixLabel = suffixLabel;
            ReadOnly = readOnly;
            InfoBoxes = infoBoxes ?? Array.Empty<InfoBoxAttribute>();
            ShowIf = showIf ?? Array.Empty<ShowIfAttribute>();
            EnableIf = enableIf ?? Array.Empty<EnableIfAttribute>();
            DisableIf = disableIf ?? Array.Empty<DisableIfAttribute>();
            GuiColor = guiColor;
        }

        public FieldInfo Field { get; }

        public int DeclarationIndex { get; }

        public FoldoutGroupAttribute FoldoutGroup { get; }

        public TitleAttribute Title { get; }

        public LabelTextAttribute LabelText { get; }

        public SuffixLabelAttribute SuffixLabel { get; }

        public ReadOnlyAttribute ReadOnly { get; }

        public InfoBoxAttribute[] InfoBoxes { get; }

        public ShowIfAttribute[] ShowIf { get; }

        public EnableIfAttribute[] EnableIf { get; }

        public DisableIfAttribute[] DisableIf { get; }

        public GUIColorAttribute GuiColor { get; }

        public int SortOrder => FoldoutGroup?.Order ?? 0;

        public bool HasConditions =>
            ShowIf.Length > 0 || EnableIf.Length > 0 || DisableIf.Length > 0 || ReadOnly != null;
    }

    internal static class FieldAttributeCache
    {
        private const BindingFlags FIELD_FLAGS =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

        private static readonly Dictionary<Type, Dictionary<string, FieldAttributeMeta>> _cache = new();

        public static FieldAttributeMeta Get(Type type, string fieldName)
        {
            if (type == null || string.IsNullOrEmpty(fieldName))
            {
                return null;
            }

            GetMap(type).TryGetValue(fieldName, out FieldAttributeMeta meta);
            return meta;
        }

        public static bool HasConditions(Type type)
        {
            if (type == null)
            {
                return false;
            }

            foreach (FieldAttributeMeta meta in GetMap(type).Values)
            {
                if (meta.HasConditions)
                {
                    return true;
                }
            }

            return false;
        }

        private static Dictionary<string, FieldAttributeMeta> GetMap(Type type)
        {
            if (!_cache.TryGetValue(type, out Dictionary<string, FieldAttributeMeta> map))
            {
                map = Build(type);
                _cache[type] = map;
            }

            return map;
        }

        private static Dictionary<string, FieldAttributeMeta> Build(Type type)
        {
            Dictionary<string, FieldAttributeMeta> map = new(StringComparer.Ordinal);
            List<Type> chain = new();
            for (Type current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                chain.Add(current);
            }

            chain.Reverse();

            int declarationIndex = 0;
            for (int t = 0; t < chain.Count; t++)
            {
                FieldInfo[] fields = chain[t].GetFields(FIELD_FLAGS);
                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo field = fields[i];
                    if (field.IsStatic)
                    {
                        continue;
                    }

                    InfoBoxAttribute[] infoBoxes = field.GetCustomAttributes<InfoBoxAttribute>().ToArray();
                    FieldAttributeMeta meta = new(
                        field,
                        declarationIndex++,
                        field.GetCustomAttribute<FoldoutGroupAttribute>(),
                        field.GetCustomAttribute<TitleAttribute>(),
                        field.GetCustomAttribute<LabelTextAttribute>(),
                        field.GetCustomAttribute<SuffixLabelAttribute>(),
                        field.GetCustomAttribute<ReadOnlyAttribute>(),
                        infoBoxes,
                        field.GetCustomAttributes<ShowIfAttribute>().ToArray(),
                        field.GetCustomAttributes<EnableIfAttribute>().ToArray(),
                        field.GetCustomAttributes<DisableIfAttribute>().ToArray(),
                        field.GetCustomAttribute<GUIColorAttribute>());

                    map[field.Name] = meta;
                }
            }

            return map;
        }
    }
}
