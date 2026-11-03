using UnityEngine;

namespace Monet5379
{
    public static class EnumStringEx
    {
        private static string GetColorString(Color color, string content)
        {
#if UNITY_EDITOR

            if (content == "None")
            {
                return string.Format("<color={0}>{1}</color>", ColorEx.ColorToHex(Colors.Gray), content);
            }
            else
            {
                return string.Format("<color={0}>{1}</color>", ColorEx.ColorToHex(color), content);
            }
#else
            return content;
#endif
        }

        public static string ToLogString(this string type)
        {
            return ToLogString(type, type);
        }

        public static string ToLogString(this string content, string type)
        {
            switch (type)
            {
                case "Buff":
                    return GetColorString(Colors.Buff, content);

                case "Equipment":
                case "Essence":
                case "Elixir":
                case "SoulGem":
                case "Relic":
                    return GetColorString(Colors.Item, content);
            }

            return content;
        }

        public static string ToLogString(this Component component, string type)
        {
            switch (type)
            {
                case "Buff":
                    return GetColorString(Colors.Buff, component.ToString());

                case "Equipment":
                case "Essence":
                case "Elixir":
                case "SoulGem":
                case "Relic":
                    return GetColorString(Colors.Item, component.ToString());
            }

            return component.ToString();
        }
    }
}