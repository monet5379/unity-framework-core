using System;
using System.Reflection;
using Monet5379;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monet5379.Inspectors.Internal
{
    internal static class InspectorConditionEvaluator
    {
        public static bool IsUnsupportedMemberExpression(string memberName)
        {
            return !string.IsNullOrEmpty(memberName) && memberName[0] == '@';
        }

        public static bool EvaluateShowIf(
            SerializedObject serializedObject,
            Object context,
            string propertyPathPrefix,
            ShowIfAttribute[] attributes)
        {
            return EvaluateAll(attributes, (attribute) =>
                EvaluateMemberCondition(
                    serializedObject,
                    context,
                    propertyPathPrefix,
                    attribute.MemberName,
                    attribute.Value));
        }

        public static bool EvaluateEnableIf(
            SerializedObject serializedObject,
            Object context,
            string propertyPathPrefix,
            EnableIfAttribute[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
            {
                return true;
            }

            return EvaluateAll(attributes, (attribute) =>
                EvaluateMemberCondition(
                    serializedObject,
                    context,
                    propertyPathPrefix,
                    attribute.MemberName,
                    attribute.Value));
        }

        public static bool EvaluateDisableIf(
            SerializedObject serializedObject,
            Object context,
            string propertyPathPrefix,
            DisableIfAttribute[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < attributes.Length; i++)
            {
                DisableIfAttribute attribute = attributes[i];
                if (EvaluateMemberCondition(
                        serializedObject,
                        context,
                        propertyPathPrefix,
                        attribute.MemberName,
                        attribute.Value))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool EvaluateAll<T>(T[] attributes, Func<T, bool> evaluateOne)
        {
            if (attributes == null || attributes.Length == 0)
            {
                return true;
            }

            for (int i = 0; i < attributes.Length; i++)
            {
                if (!evaluateOne(attributes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool EvaluateMemberCondition(
            SerializedObject serializedObject,
            Object context,
            string propertyPathPrefix,
            string memberName,
            object expectedValue)
        {
            if (string.IsNullOrEmpty(memberName) || IsUnsupportedMemberExpression(memberName))
            {
                return true;
            }

            object memberValue = GetConditionMemberValue(serializedObject, context, propertyPathPrefix, memberName);
            if (expectedValue == null)
            {
                return IsTruthy(memberValue);
            }

            return ValuesEqual(memberValue, expectedValue);
        }

        private static object GetConditionMemberValue(
            SerializedObject serializedObject,
            Object context,
            string propertyPathPrefix,
            string memberName)
        {
            if (serializedObject != null)
            {
                SerializedProperty property = serializedObject.FindProperty(propertyPathPrefix + memberName);
                if (property != null)
                {
                    return ReadSerializedPropertyValue(property);
                }
            }

            if (!string.IsNullOrEmpty(propertyPathPrefix))
            {
                return null;
            }

            return GetInstanceMemberValue(context, memberName);
        }

        private static object ReadSerializedPropertyValue(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return property.boolValue;
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.ArraySize:
                    return property.intValue;
                case SerializedPropertyType.Enum:
                    return property.intValue;
                case SerializedPropertyType.Float:
                    return property.floatValue;
                case SerializedPropertyType.String:
                    return property.stringValue;
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue;
                default:
                    return null;
            }
        }

        private static object GetInstanceMemberValue(Object context, string memberName)
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

            PropertyInfo propertyInfo = type.GetProperty(
                memberName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (propertyInfo != null && propertyInfo.GetIndexParameters().Length == 0)
            {
                return propertyInfo.GetValue(context);
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

        private static bool IsTruthy(object value)
        {
            if (value == null)
            {
                return false;
            }

            switch (value)
            {
                case bool boolValue:
                    return boolValue;
                case int intValue:
                    return intValue != 0;
                case float floatValue:
                    return !Mathf.Approximately(floatValue, 0f);
                case double doubleValue:
                    return Math.Abs(doubleValue) > double.Epsilon;
                case string stringValue:
                    return !string.IsNullOrEmpty(stringValue);
                default:
                    Type valueType = value.GetType();
                    if (valueType.IsEnum)
                    {
                        return Convert.ToInt32(value) != 0;
                    }

                    return true;
            }
        }

        private static bool ValuesEqual(object memberValue, object expectedValue)
        {
            if (expectedValue == null)
            {
                return memberValue == null;
            }

            if (memberValue == null)
            {
                return false;
            }

            Type memberType = memberValue.GetType();
            if (memberType.IsEnum)
            {
                if (expectedValue.GetType().IsEnum)
                {
                    return Equals(memberValue, expectedValue);
                }

                try
                {
                    object converted = Enum.ToObject(memberType, expectedValue);
                    return Equals(memberValue, converted);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            if (memberValue is int intMember && expectedValue is Enum enumExpected)
            {
                return intMember == Convert.ToInt32(enumExpected);
            }

            if (memberValue is float floatMember && expectedValue is int intExpected)
            {
                return Mathf.Approximately(floatMember, intExpected);
            }

            if (memberValue is int intMemberValue && expectedValue is float floatExpected)
            {
                return Mathf.Approximately(intMemberValue, floatExpected);
            }

            return Equals(memberValue, expectedValue);
        }
    }
}
