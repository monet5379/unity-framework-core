using System.Collections.Generic;
using Monet5379.Inspectors.Internal;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monet5379.Inspectors
{
    internal sealed class StringTableKeyDropdown : AdvancedDropdown
    {
        private const string RootTitle = "String Key";
        private const float MinimumHeight = 300f;

        private readonly Object[] _targets;
        private readonly string _propertyPath;
        private readonly IReadOnlyList<string> _choices;

        public StringTableKeyDropdown(AdvancedDropdownState state, SerializedProperty property, float width)
            : base(state)
        {
            _targets = property.serializedObject.targetObjects;
            _propertyPath = property.propertyPath;
            string current = property.hasMultipleDifferentValues ? string.Empty : property.stringValue;
            _choices = StringTableKeyCatalog.BuildChoices(current, StringTableKeyCatalog.GetSortedKeys());
            minimumSize = new Vector2(width, MinimumHeight);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new(RootTitle);
            for (int i = 0; i < _choices.Count; i++)
            {
                root.AddChild(new AdvancedDropdownItem(_choices[i]));
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            SerializedObject serializedObject = new(_targets);
            SerializedProperty property = serializedObject.FindProperty(_propertyPath);
            if (property == null)
            {
                return;
            }

            property.stringValue = StringTableKeyCatalog.StoredValueForChoiceLabel(item.name);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
