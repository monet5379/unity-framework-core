using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Monet5379;
using UnityEditor;

namespace Monet5379.Inspectors.Internal
{
    internal sealed class ButtonMethod
    {
        public ButtonMethod(MethodInfo method, ButtonAttribute attribute, int depth, int token)
        {
            Method = method;
            Attribute = attribute;
            Depth = depth;
            Token = token;
            Label = string.IsNullOrEmpty(attribute.Name) ? ObjectNames.NicifyVariableName(method.Name) : attribute.Name;
            Arguments = BuildArguments(method, out string unsupportedReason);
            UnsupportedReason = unsupportedReason;
        }

        public MethodInfo Method { get; }

        public ButtonAttribute Attribute { get; }

        public string Label { get; }

        public object[] Arguments { get; }

        public string UnsupportedReason { get; }

        public bool IsSupported => UnsupportedReason == null;

        public int Depth { get; }

        public int Token { get; }

        private static object[] BuildArguments(MethodInfo method, out string unsupportedReason)
        {
            unsupportedReason = null;

            if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
            {
                unsupportedReason = "코루틴 메서드는 버튼으로 호출할 수 없습니다.";
                return null;
            }

            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length == 0)
            {
                return null;
            }

            object[] arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (!parameters[i].HasDefaultValue)
                {
                    unsupportedReason = $"파라미터가 있는 메서드는 버튼으로 호출할 수 없습니다: {parameters[i].ParameterType.Name} {parameters[i].Name}";
                    return null;
                }

                arguments[i] = parameters[i].DefaultValue;
            }

            return arguments;
        }
    }

    internal static class ButtonMethodCache
    {
        private const BindingFlags METHOD_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

        private static readonly Dictionary<Type, ButtonMethod[]> _cache = new();

        public static ButtonMethod[] Get(Type type)
        {
            if (!_cache.TryGetValue(type, out ButtonMethod[] methods))
            {
                methods = Collect(type);
                _cache[type] = methods;
            }

            return methods;
        }

        private static ButtonMethod[] Collect(Type type)
        {
            Dictionary<(Module, int), ButtonMethod> found = new();
            HashSet<(Module, int)> visited = new();

            for (Type current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                foreach (MethodInfo method in current.GetMethods(METHOD_FLAGS))
                {
                    if (method.IsSpecialName || method.IsGenericMethodDefinition)
                    {
                        continue;
                    }

                    MethodInfo baseDefinition = method.GetBaseDefinition();
                    (Module, int) key = (baseDefinition.Module, baseDefinition.MetadataToken);
                    if (!visited.Add(key))
                    {
                        continue;
                    }

                    ButtonAttribute attribute = (ButtonAttribute)System.Attribute.GetCustomAttribute(method, typeof(ButtonAttribute), true);
                    if (attribute == null)
                    {
                        continue;
                    }

                    int depth = GetDepth(type, baseDefinition.DeclaringType);
                    found[key] = new ButtonMethod(method, attribute, depth, baseDefinition.MetadataToken);
                }
            }

            ButtonMethod[] result = new ButtonMethod[found.Count];
            found.Values.CopyTo(result, 0);
            Array.Sort(result, Compare);
            return result;
        }

        private static int Compare(ButtonMethod left, ButtonMethod right)
        {
            int order = left.Attribute.Order.CompareTo(right.Attribute.Order);
            if (order != 0)
            {
                return order;
            }

            int depth = left.Depth.CompareTo(right.Depth);
            if (depth != 0)
            {
                return depth;
            }

            return left.Token.CompareTo(right.Token);
        }

        private static int GetDepth(Type type, Type declaringType)
        {
            int depth = 0;
            for (Type current = type; current != null; current = current.BaseType)
            {
                if (current == declaringType)
                {
                    return depth;
                }

                depth++;
            }

            return depth;
        }
    }
}
