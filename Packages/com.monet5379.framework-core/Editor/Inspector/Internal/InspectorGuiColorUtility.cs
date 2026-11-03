using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using Monet5379;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Monet5379.Inspectors.Internal
{
    internal static class InspectorGuiColorUtility
    {
        public static void BindBackgroundColor(
            VisualElement target,
            SerializedObject serializedObject,
            Object context,
            FieldInfo field,
            GUIColorAttribute guiColor,
            SerializedProperty property)
        {
            if (target == null || guiColor == null || string.IsNullOrEmpty(guiColor.GetColor))
            {
                return;
            }

            void ApplyColor()
            {
                Color? color = ResolveColor(context, field, guiColor.GetColor, property);
                if (color.HasValue)
                {
                    target.style.backgroundColor = color.Value;
                    target.style.borderTopLeftRadius = 2;
                    target.style.borderTopRightRadius = 2;
                    target.style.borderBottomLeftRadius = 2;
                    target.style.borderBottomRightRadius = 2;
                    target.style.paddingLeft = 2;
                    target.style.paddingRight = 2;
                    target.style.paddingTop = 1;
                    target.style.paddingBottom = 1;
                }
            }

            ApplyColor();
            if (property != null)
            {
                BindingExtensions.TrackPropertyValue(target, property, _ => ApplyColor());
            }

            if (serializedObject != null)
            {
                BindingExtensions.TrackSerializedObjectValue(target, serializedObject, _ => ApplyColor());
            }
        }

        private static Color? ResolveColor(Object context, FieldInfo field, string methodName, SerializedProperty property)
        {
            if (context == null || string.IsNullOrEmpty(methodName))
            {
                return null;
            }

            MethodInfo method = FindColorMethod(context.GetType(), methodName);
            if (method == null || method.ReturnType != typeof(Color))
            {
                return null;
            }

            try
            {
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    return (Color)method.Invoke(context, null);
                }

                if (parameters.Length == 1)
                {
                    object argument = field != null
                        ? field.GetValue(context)
                        : ReadSerializedPropertyValue(property);
                    argument = CoerceArgument(argument, parameters[0].ParameterType);
                    return (Color)method.Invoke(context, new[] { argument });
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning(
                    $"GUIColor 메서드 호출 실패 ({context.GetType().Name}.{methodName}): {exception.Message}");
            }

            return null;
        }

        private static MethodInfo FindColorMethod(Type type, string methodName)
        {
            const BindingFlags flags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            for (Type current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                MethodInfo method = current.GetMethod(methodName, flags);
                if (method != null && !method.IsStatic)
                {
                    return method;
                }
            }

            return null;
        }

        private static object CoerceArgument(object argument, Type parameterType)
        {
            if (argument == null || parameterType.IsInstanceOfType(argument))
            {
                return argument;
            }

            if (parameterType.IsEnum)
            {
                if (argument is int intValue)
                {
                    return Enum.ToObject(parameterType, intValue);
                }

                if (argument is string stringValue && Enum.IsDefined(parameterType, stringValue))
                {
                    return Enum.Parse(parameterType, stringValue);
                }
            }

            try
            {
                return Convert.ChangeType(argument, parameterType);
            }
            catch (Exception)
            {
                return argument;
            }
        }

        private static object ReadSerializedPropertyValue(SerializedProperty property)
        {
            if (property == null)
            {
                return null;
            }

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
    }
}
