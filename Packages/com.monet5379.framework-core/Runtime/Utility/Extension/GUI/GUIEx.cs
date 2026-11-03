using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;

namespace Monet5379
{
    public static class GUIEx
    {
        // Grid

        public static int SelectionGrid(this GUIData data, int index, string[] contents, int count, bool useWidth = true, bool useHeight = true)
        {
            int contentCount = contents.Length;
            int row = Mathf.CeilToInt((float)contentCount / count);

            float width = data.ButtonWidth * count;
            float height = data.ButtonHeight * row;

            int result = index;
            GUILayout.BeginVertical("Box");
            {
                if (false == useWidth && false == useHeight)
                {
                    result = GUILayout.SelectionGrid(index, contents, count, GUILayout.ExpandWidth(false), GUILayout.MinHeight(height));
                }
                else if (useWidth && useHeight)
                {
                    result = GUILayout.SelectionGrid(index, contents, count, GUILayout.Width(width), GUILayout.Height(height));
                }
                else if (useWidth)
                {
                    result = GUILayout.SelectionGrid(index, contents, count, GUILayout.Width(width), GUILayout.MinHeight(height));
                }
                else if (useHeight)
                {
                    result = GUILayout.SelectionGrid(index, contents, count, GUILayout.ExpandWidth(false), GUILayout.Height(height));
                }
            }
            GUILayout.EndVertical();

            return result;
        }

        // Button

        public static void Button(this GUIData data, string text, UnityAction OnClink, bool useWidth = true, bool useHeight = true)
        {
            bool isClick = false;

            GUIStyle style = GUI.skin.button;

            style.richText = true;

            if (false == useWidth && false == useHeight)
            {
                isClick = GUILayout.Button(text, style, GUILayout.ExpandWidth(false), GUILayout.MinHeight(data.ButtonHeight));
            }
            else if (useWidth && useHeight)
            {
                isClick = GUILayout.Button(text, style, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                isClick = GUILayout.Button(text, style, GUILayout.Width(data.ButtonWidth), GUILayout.MinHeight(data.ButtonHeight));
            }
            else if (useHeight)
            {
                isClick = GUILayout.Button(text, style, GUILayout.ExpandWidth(false), GUILayout.Height(data.ButtonHeight));
            }

            if (isClick)
            {
                if (OnClink != null)
                {
                    OnClink.Invoke();
                }
            }
        }

        public static void CustomButton(this GUIData data, string text, UnityAction OnClink, float widthRate = 1f, float heightRate = 1f)
        {
            GUIStyle style = GUI.skin.button;
            style.richText = true;

            bool isClick = GUILayout.Button(text, style, GUILayout.Width(data.ButtonWidth * widthRate), GUILayout.Height(data.ButtonHeight * heightRate));
            if (isClick)
            {
                if (OnClink != null)
                {
                    OnClink.Invoke();
                }
            }
        }

        // Toggle

        public static bool ToggleButton(this GUIData data, bool value, bool useColor = true)
        {
            if (useColor)
            {
                if (value)
                {
                    if (GUILayout.Button("On".ToSelectString(), GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight)))
                    {
                        return !value;
                    }
                }
                else
                {
                    if (GUILayout.Button("Off".ToDisableString(), GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight)))
                    {
                        return !value;
                    }
                }
            }
            else
            {
                if (value)
                {
                    if (GUILayout.Button("On", GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight)))
                    {
                        return !value;
                    }
                }
                else
                {
                    if (GUILayout.Button("Off", GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight)))
                    {
                        return !value;
                    }
                }
            }

            return value;
        }

        // Dropdown

        public static TEnum Dropdown<TEnum>(this GUIData data, string dropDownName, int index) where TEnum : Enum
        {
#if UNITY_EDITOR
            string[] dropdownOptions = EnumEx.GetNames<TEnum>();
            int selectedIndex = EditorGUILayout.Popup(dropDownName, index, dropdownOptions);
            return selectedIndex.ToEnum<TEnum>();
#else
            return 0.ToEnum<TEnum>();
#endif
        }

        // Label

        public static void TitleLabel(this GUIData data, string text, bool useWidth = false, bool useHeight = true)
        {
            if (data.TitleStyle == null)
            {
                GUILayout.Label(text);
                return;
            }
            else if (false == useWidth && false == useHeight)
            {
                GUILayout.Label(text, data.TitleStyle);
            }
            else if (useWidth && useHeight)
            {
                GUILayout.Label(text, data.ButtonStyle, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                GUILayout.Label(text, data.TitleStyle, GUILayout.Width(data.ButtonWidth));
            }
            else if (useHeight)
            {
                GUILayout.Label(text, data.TitleStyle, GUILayout.Height(data.ButtonHeight));
            }
        }

        public static void TitleLabel(this GUIData data, string text, Color color, bool useWidth = false, bool useHeight = true)
        {
            string hexColor = ColorEx.ColorToHex(color);
            text = string.Format("<color={1}>{0}</color>", text, hexColor);

            if (data.TitleStyle == null)
            {
                GUILayout.Label(text);
                return;
            }
            else if (false == useWidth && false == useHeight)
            {
                GUILayout.Label(text, data.TitleStyle);
            }
            else if (useWidth && useHeight)
            {
                GUILayout.Label(text, data.ButtonStyle, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                GUILayout.Label(text, data.TitleStyle, GUILayout.Width(data.ButtonWidth));
            }
            else if (useHeight)
            {
                GUILayout.Label(text, data.TitleStyle, GUILayout.Height(data.ButtonHeight));
            }
        }

        //

        public static void ContentLabel(this GUIData data, string text, bool useWidth = false, bool useHeight = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (false == useWidth && false == useHeight)
            {
                GUILayout.Label(text, data.ContentStyle);
            }
            else if (useWidth && useHeight)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Width(data.ButtonWidth));
            }
            else if (useHeight)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Height(data.ButtonHeight));
            }
        }

        public static void ContentLabel(this GUIData data, string text, Color color, bool useWidth = false, bool useHeight = false)
        {
            string hexColor = ColorEx.ColorToHex(color);
            text = string.Format("<color={1}> {0}</color>", text, hexColor);

            if (false == useWidth && false == useHeight)
            {
                GUILayout.Label(text, data.ContentStyle);
            }
            else if (useWidth && useHeight)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Width(data.ButtonWidth));
            }
            else if (useHeight)
            {
                GUILayout.Label(text, data.ContentStyle, GUILayout.Height(data.ButtonHeight));
            }
        }

        public static void SubContentLabel(this GUIData data, string text, bool useWidth = false, bool useHeight = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (false == useWidth && false == useHeight)
            {
                GUILayout.Label(text, data.SubContentStyle);
            }
            else if (useWidth && useHeight)
            {
                GUILayout.Label(text, data.SubContentStyle, GUILayout.Width(data.ButtonWidth), GUILayout.Height(data.ButtonHeight));
            }
            else if (useWidth)
            {
                GUILayout.Label(text, data.SubContentStyle, GUILayout.Width(data.ButtonWidth));
            }
            else if (useHeight)
            {
                GUILayout.Label(text, data.SubContentStyle, GUILayout.Height(data.ButtonHeight));
            }
        }

        //

        public static void Box(this GUIData data, GUIContent content, int width, int height)
        {
            GUILayout.Box(content, data.TextureStyle, GUILayout.Width(width), GUILayout.Height(height));
        }
    }
}