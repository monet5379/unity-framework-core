
using UnityEngine;

namespace Monet5379
{
    public class GUIData
    {
        public float WindowWidth;
        public float WindowHeight;
        public float ButtonWidth;
        public float ButtonHeight;

        public Vector2 ScrollPosition = Vector2.zero;

        public GUIStyle TitleStyle;
        public GUIStyle ButtonStyle;
        public GUIStyle ContentStyle;
        public GUIStyle SubContentStyle;
        public GUIStyle TextureStyle;

        public void RefreshSize(float width, float height)
        {
            WindowWidth = width;
            WindowHeight = height;

            ButtonWidth = 128;
            ButtonHeight = 32;
        }

        public void RefreshStyle()
        {
            TitleStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,

                fontSize = 14,
                richText = true,
            };
            TitleStyle.normal.textColor = Colors.CreamIvory;

            ContentStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.MiddleLeft,

                fontSize = 12,
                wordWrap = true,
                richText = true,
            };
            ContentStyle.normal.textColor = Colors.CreamIvory;

            SubContentStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleLeft,

                fontSize = 11,
                wordWrap = true,
                richText = true,
            };
            SubContentStyle.normal.textColor = Colors.DarkGray;

            ButtonStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,

                fontSize = 14,
                richText = true,
            };
            ButtonStyle.normal.textColor = Colors.CreamIvory;

            TextureStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
            };
        }
    }
}