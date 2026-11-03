using ConditionalLog;
using TMPro;
using UnityEngine;

namespace Monet5379
{
    [System.Serializable]
    public class LocalizedTextTypographyBinding
    {
        public LocalizedTextUsage TextUsage;

        public string TextUsageString;

        public int CustomAdditionalFontSize;

        public void Apply(TextMeshProUGUI text, float defaultFontSize, float customFontSizeOverride, Component owner)
        {
            if (text == null)
            {
                return;
            }

            FontStyles fontStyle = FontStyles.Normal;
            float fontSize = defaultFontSize;

            ILocalizationSource source = LocalizationContext.Source;
            LocalizedTextStyleTable table = source?.StyleTable;
            if (table != null
                && TextUsage != LocalizedTextUsage.None
                && table.TryGetStyle(TextUsage, out LocalizedTextStyleEntry entry))
            {
                if (entry.UseBold)
                {
                    fontStyle |= FontStyles.Bold;
                }

                if (entry.UseItalic)
                {
                    fontStyle |= FontStyles.Italic;
                }

                if (entry.FontSize > 0f)
                {
                    fontSize = entry.FontSize;
                }
            }

            if (customFontSizeOverride > 0f)
            {
                fontSize = customFontSizeOverride;
            }
            else if (fontSize > 0f)
            {
                fontSize += CustomAdditionalFontSize;
            }

            text.fontStyle = fontStyle;

            if (fontSize > 0f && text.fontSize != fontSize)
            {
                text.enableAutoSizing = false;
                text.fontSize = fontSize;

                GameLog.Info(
                    LocalizationLogTags.Font,
                    "LocalizedText 스타일 적용: usage={0}, size={1}, {2}",
                    TextUsage,
                    text.fontSize,
                    owner.GetHierarchyPath());
            }
        }

        public void ValidateEnumStrings(string hierarchyPath)
        {
            if (TextUsage != LocalizedTextUsage.None && string.IsNullOrEmpty(TextUsageString))
            {
                GameLog.Warning(
                    "{0} TextUsage 문자열이 설정되지 않았습니다: {1}",
                    TextUsage,
                    hierarchyPath);
            }

            if (!EnumEx.ConvertTo(ref TextUsage, TextUsageString))
            {
                GameLog.Error(
                    "TextUsage를 갱신할 수 없습니다. {0}, {1}",
                    TextUsageString,
                    hierarchyPath);
            }
        }

        public void AutoSetting()
        {
            if (TextUsage != LocalizedTextUsage.None)
            {
                TextUsageString = TextUsage.ToString();
            }
        }
    }
}
