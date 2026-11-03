using System;
using System.Collections.Generic;
using System.Reflection;
using Monet5379;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Monet5379.Inspectors.Internal
{
    internal static class InspectorLayoutBuilder
    {
        private const string FOLDOUT_VIEW_DATA_PREFIX = "Monet5379.InspectorLayout.Foldout.";

        private sealed class PropertyDrawEntry
        {
            public PropertyDrawEntry(SerializedProperty property, FieldAttributeMeta meta, int iteratorIndex)
            {
                Property = property;
                Meta = meta;
                IteratorIndex = iteratorIndex;
            }

            public SerializedProperty Property { get; }

            public FieldAttributeMeta Meta { get; }

            public int IteratorIndex { get; }
        }

        public static void Build(VisualElement root, SerializedObject serializedObject, Editor editor)
        {
            if (root == null || serializedObject == null || editor == null)
            {
                return;
            }

            serializedObject.Update();
            Type targetType = editor.target.GetType();
            Object context = editor.target;

            DrawScriptReference(root, serializedObject);

            List<PropertyDrawEntry> entries = CollectTopLevelEntries(serializedObject, targetType);
            entries.Sort(CompareEntries);

            Dictionary<string, Foldout> foldouts = new(StringComparer.Ordinal);
            for (int i = 0; i < entries.Count; i++)
            {
                PropertyDrawEntry entry = entries[i];
                VisualElement groupContainer = ResolveGroupContainer(root, targetType, entry.Meta, foldouts);
                DrawEntry(groupContainer, entry, serializedObject, context, string.Empty);
            }

            root.Bind(serializedObject);
            ButtonDrawer.Append(root, editor);
        }

        private static void DrawEntry(
            VisualElement container,
            PropertyDrawEntry entry,
            SerializedObject serializedObject,
            Object context,
            string conditionPathPrefix)
        {
            VisualElement entryRoot = new();
            container.Add(entryRoot);

            DrawDecorations(entryRoot, entry, serializedObject, context);
            PropertyFieldDrawResult drawResult = DrawPropertyField(entryRoot, entry, serializedObject, context);
            BindEntryConditions(entryRoot, drawResult, entry, serializedObject, context, conditionPathPrefix);
        }

        private static void DrawScriptReference(VisualElement root, SerializedObject serializedObject)
        {
            SerializedProperty scriptProperty = serializedObject.FindProperty("m_Script");
            if (scriptProperty == null)
            {
                return;
            }

            PropertyField scriptField = new(scriptProperty);
            scriptField.SetEnabled(false);
            root.Add(scriptField);
        }

        private static List<PropertyDrawEntry> CollectTopLevelEntries(SerializedObject serializedObject, Type targetType)
        {
            List<PropertyDrawEntry> entries = new();
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            int iteratorIndex = 0;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (iterator.propertyPath == "m_Script")
                {
                    continue;
                }

                FieldAttributeMeta meta = FieldAttributeCache.Get(targetType, iterator.name);
                entries.Add(new PropertyDrawEntry(iterator.Copy(), meta, iteratorIndex++));
            }

            return entries;
        }

        private static int CompareEntries(PropertyDrawEntry a, PropertyDrawEntry b)
        {
            int orderA = a.Meta?.SortOrder ?? 0;
            int orderB = b.Meta?.SortOrder ?? 0;
            int orderCompare = orderA.CompareTo(orderB);
            if (orderCompare != 0)
            {
                return orderCompare;
            }

            int indexA = a.Meta?.DeclarationIndex ?? a.IteratorIndex;
            int indexB = b.Meta?.DeclarationIndex ?? b.IteratorIndex;
            int indexCompare = indexA.CompareTo(indexB);
            if (indexCompare != 0)
            {
                return indexCompare;
            }

            return a.IteratorIndex.CompareTo(b.IteratorIndex);
        }

        private static VisualElement ResolveGroupContainer(
            VisualElement root,
            Type targetType,
            FieldAttributeMeta meta,
            Dictionary<string, Foldout> foldouts)
        {
            if (meta?.FoldoutGroup == null || string.IsNullOrEmpty(meta.FoldoutGroup.GroupName))
            {
                return root;
            }

            string[] segments = meta.FoldoutGroup.GroupName.Split('/');
            VisualElement parent = root;
            string cumulativePath = string.Empty;

            for (int i = 0; i < segments.Length; i++)
            {
                string segment = segments[i];
                if (string.IsNullOrEmpty(segment))
                {
                    continue;
                }

                cumulativePath = cumulativePath.Length == 0 ? segment : cumulativePath + "/" + segment;
                if (!foldouts.TryGetValue(cumulativePath, out Foldout foldout))
                {
                    foldout = new Foldout
                    {
                        text = segment,
                        value = meta.FoldoutGroup.Expanded,
                        viewDataKey = FOLDOUT_VIEW_DATA_PREFIX + targetType.FullName + "." + cumulativePath,
                    };
                    parent.Add(foldout);
                    foldouts[cumulativePath] = foldout;
                }

                parent = foldout;
            }

            return parent;
        }

        private static void DrawDecorations(
            VisualElement container,
            PropertyDrawEntry entry,
            SerializedObject serializedObject,
            Object context)
        {
            FieldAttributeMeta meta = entry.Meta;
            if (meta == null)
            {
                return;
            }

            if (meta.Title != null && !string.IsNullOrEmpty(meta.Title.Title))
            {
                container.Add(CreateTitleBlock(meta.Title));
            }

            if (meta.InfoBoxes.Length > 0)
            {
                for (int i = 0; i < meta.InfoBoxes.Length; i++)
                {
                    HelpBox helpBox = CreateInfoBox(meta.InfoBoxes[i], context, serializedObject);
                    if (helpBox != null)
                    {
                        container.Add(helpBox);
                    }
                }
            }
        }

        private static VisualElement CreateTitleBlock(TitleAttribute title)
        {
            VisualElement block = new();
            block.style.marginTop = 4f;
            block.style.marginBottom = 2f;

            Label titleLabel = new(title.Title)
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                },
            };
            block.Add(titleLabel);

            if (!string.IsNullOrEmpty(title.Subtitle))
            {
                Label subtitleLabel = new(title.Subtitle);
                block.Add(subtitleLabel);
            }

            VisualElement line = new()
            {
                style =
                {
                    height = 1,
                    marginTop = 2,
                    marginBottom = 4,
                    backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.5f),
                },
            };
            block.Add(line);
            return block;
        }

        private static HelpBox CreateInfoBox(InfoBoxAttribute infoBox, Object context, SerializedObject serializedObject)
        {
            if (infoBox == null || string.IsNullOrEmpty(infoBox.Message))
            {
                return null;
            }

            string visibleMember = ResolveVisibleIfMember(infoBox);
            string message = ResolveInfoBoxMessage(infoBox.Message, context);
            HelpBox helpBox = new(message, MapMessageType(infoBox.MessageType));

            if (!string.IsNullOrEmpty(visibleMember))
            {
                SerializedProperty visibleProperty = serializedObject.FindProperty(visibleMember);
                if (visibleProperty != null && visibleProperty.propertyType == SerializedPropertyType.Boolean)
                {
                    BindInfoBoxVisibility(helpBox, visibleProperty);
                }
            }

            return helpBox;
        }

        private static void BindInfoBoxVisibility(HelpBox helpBox, SerializedProperty visibleProperty)
        {
            void UpdateVisibility()
            {
                helpBox.style.display = visibleProperty.boolValue ? DisplayStyle.Flex : DisplayStyle.None;
            }

            UpdateVisibility();
            helpBox.TrackPropertyValue(visibleProperty, _ => UpdateVisibility());
        }

        private static string ResolveVisibleIfMember(InfoBoxAttribute infoBox)
        {
            if (!string.IsNullOrEmpty(infoBox.VisibleIfMember))
            {
                return infoBox.VisibleIfMember;
            }

            return infoBox.VisibleIf;
        }

        private static HelpBoxMessageType MapMessageType(InfoMessageType messageType)
        {
            return messageType switch
            {
                InfoMessageType.Warning => HelpBoxMessageType.Warning,
                InfoMessageType.Error => HelpBoxMessageType.Error,
                _ => HelpBoxMessageType.Info,
            };
        }

        private static string ResolveInfoBoxMessage(string message, Object context)
        {
            if (string.IsNullOrEmpty(message) || context == null)
            {
                return message;
            }

            if (message[0] != '@' && message[0] != '$')
            {
                return message;
            }

            string memberName = message.Substring(1);
            object value = GetMemberValue(context, memberName);
            return value?.ToString() ?? string.Empty;
        }

        private static object GetMemberValue(Object context, string memberName)
        {
            if (context == null || string.IsNullOrEmpty(memberName))
            {
                return null;
            }

            Type type = context.GetType();
            FieldInfo field = FindInstanceField(type, memberName);
            if (field != null)
            {
                return field.GetValue(context);
            }

            PropertyInfo property = type.GetProperty(
                memberName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null && property.GetIndexParameters().Length == 0)
            {
                return property.GetValue(context);
            }

            return null;
        }

        private static FieldInfo FindInstanceField(Type type, string fieldName)
        {
            for (Type current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                FieldInfo field = current.GetField(
                    fieldName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (field != null)
                {
                    return field;
                }
            }

            return null;
        }

        private sealed class PropertyFieldDrawResult
        {
            public PropertyFieldDrawResult(VisualElement field, VisualElement colorTarget)
            {
                Field = field;
                ColorTarget = colorTarget;
            }

            public VisualElement Field { get; }

            public VisualElement ColorTarget { get; }
        }

        private static PropertyFieldDrawResult DrawPropertyField(
            VisualElement container,
            PropertyDrawEntry entry,
            SerializedObject serializedObject,
            Object context)
        {
            SerializedProperty property = serializedObject.FindProperty(entry.Property.propertyPath);
            if (property == null)
            {
                return null;
            }

            FieldAttributeMeta meta = entry.Meta;
            string label = meta?.LabelText != null ? meta.LabelText.Text : property.displayName;

            Type nestedType = GetExpandableNestedType(property, meta);
            VisualElement field = nestedType != null
                ? CreateNestedFoldout(property, label, nestedType, serializedObject, context)
                : new PropertyField(property, label);
            VisualElement fieldRow = field;

            if (meta?.SuffixLabel != null && !string.IsNullOrEmpty(meta.SuffixLabel.Label))
            {
                VisualElement row = new()
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row,
                        alignItems = Align.Center,
                    },
                };

                field.style.flexGrow = 1f;
                row.Add(field);

                Label suffix = new(meta.SuffixLabel.Label)
                {
                    style =
                    {
                        color = new Color(0.65f, 0.65f, 0.65f, 1f),
                        marginLeft = 6,
                        unityTextAlign = TextAnchor.MiddleRight,
                        flexShrink = 0,
                    },
                };
                row.Add(suffix);
                fieldRow = row;
            }

            VisualElement colorTarget = fieldRow;
            if (meta?.GuiColor != null)
            {
                VisualElement colorWrapper = new();
                colorWrapper.Add(fieldRow);
                colorTarget = colorWrapper;
                InspectorGuiColorUtility.BindBackgroundColor(
                    colorWrapper,
                    serializedObject,
                    context,
                    meta.Field,
                    meta.GuiColor,
                    property);
            }

            container.Add(colorTarget);
            return new PropertyFieldDrawResult(field, colorTarget);
        }

        private static Type GetExpandableNestedType(SerializedProperty property, FieldAttributeMeta meta)
        {
            if (meta == null || property.propertyType != SerializedPropertyType.Generic || property.isArray)
            {
                return null;
            }

            Type fieldType = meta.Field.FieldType;
            return FieldAttributeCache.HasConditions(fieldType) ? fieldType : null;
        }

        private static Foldout CreateNestedFoldout(
            SerializedProperty property,
            string label,
            Type nestedType,
            SerializedObject serializedObject,
            Object context)
        {
            Foldout foldout = new()
            {
                text = label,
                value = property.isExpanded,
            };
            foldout.RegisterValueChangedCallback(evt =>
            {
                if (evt.target == foldout)
                {
                    property.isExpanded = evt.newValue;
                }
            });

            string childPathPrefix = property.propertyPath + ".";
            SerializedProperty child = property.Copy();
            SerializedProperty end = property.GetEndProperty();
            bool enterChildren = true;
            int iteratorIndex = 0;

            while (child.NextVisible(enterChildren) && !SerializedProperty.EqualContents(child, end))
            {
                enterChildren = false;
                FieldAttributeMeta childMeta = FieldAttributeCache.Get(nestedType, child.name);
                PropertyDrawEntry childEntry = new(child.Copy(), childMeta, iteratorIndex++);
                DrawEntry(foldout, childEntry, serializedObject, context, childPathPrefix);
            }

            return foldout;
        }

        private static void BindEntryConditions(
            VisualElement entryRoot,
            PropertyFieldDrawResult drawResult,
            PropertyDrawEntry entry,
            SerializedObject serializedObject,
            Object context,
            string conditionPathPrefix)
        {
            if (entryRoot == null || drawResult?.Field == null)
            {
                return;
            }

            FieldAttributeMeta meta = entry.Meta;
            if (meta == null || !meta.HasConditions)
            {
                return;
            }

            bool hasShowIf = meta.ShowIf.Length > 0;
            bool isReadOnly = meta.ReadOnly != null;

            void Refresh()
            {
                serializedObject.Update();

                if (hasShowIf)
                {
                    bool visible = InspectorConditionEvaluator.EvaluateShowIf(
                        serializedObject,
                        context,
                        conditionPathPrefix,
                        meta.ShowIf);
                    entryRoot.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
                    if (!visible)
                    {
                        return;
                    }
                }

                bool enabled = InspectorConditionEvaluator.EvaluateEnableIf(
                    serializedObject,
                    context,
                    conditionPathPrefix,
                    meta.EnableIf);
                if (InspectorConditionEvaluator.EvaluateDisableIf(
                        serializedObject,
                        context,
                        conditionPathPrefix,
                        meta.DisableIf))
                {
                    enabled = false;
                }

                if (isReadOnly)
                {
                    enabled = false;
                }

                drawResult.Field.SetEnabled(enabled);
            }

            Refresh();
            entryRoot.TrackSerializedObjectValue(serializedObject, _ => Refresh());
        }
    }
}
