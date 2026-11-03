using Monet5379;
using Monet5379.Inspectors.Internal;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Monet5379.Inspectors
{
    [CustomPropertyDrawer(typeof(StringTableKeyAttribute))]
    public sealed class StringTableKeyDrawer : PropertyDrawer
    {
        private const string MixedValueLabel = "—";

        private readonly AdvancedDropdownState _dropdownState = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect buttonRect = EditorGUI.PrefixLabel(position, label);
            if (EditorGUI.DropdownButton(buttonRect, new GUIContent(ButtonText(property)), FocusType.Keyboard))
            {
                new StringTableKeyDropdown(_dropdownState, property, buttonRect.width).Show(buttonRect);
            }

            EditorGUI.EndProperty();
        }

        private static string ButtonText(SerializedProperty property)
        {
            if (property.hasMultipleDifferentValues)
            {
                return MixedValueLabel;
            }

            return string.IsNullOrEmpty(property.stringValue)
                ? StringTableKeyCatalog.NoneChoiceLabel
                : property.stringValue;
        }
    }
}
