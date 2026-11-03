using ConditionalLog;
using TmpFontPipeline;
using TMPro;

namespace Monet5379
{
    [System.Serializable]
    public class LocalizedTextFontBinding
    {
        public FontUsageRole PipelineRole;

        public string PipelineRoleString;

        public bool ApplyFontFromPipeline = true;

        public bool UseCustomLanguage;

        [ShowIf("UseCustomLanguage")]
        public LanguageNames CustomLanguage;

        [ShowIf("UseCustomLanguage")]
        public string CustomLanguageString;

        private bool _customLanguageLabelHeld;

        private LanguageNames _heldCustomLanguageLabel;

        public void OnEnabled()
        {
            TryAcquireCustomLanguageLabel();
        }

        public void OnDisabled()
        {
            TryReleaseCustomLanguageLabel();
        }

        public void Apply(TextMeshProUGUI text, LanguageNames displayLanguage)
        {
            if (text == null || !ApplyFontFromPipeline)
            {
                return;
            }

            LanguageNames fontLanguage = displayLanguage;
            if (UseCustomLanguage && CustomLanguage != LanguageNames.None)
            {
                fontLanguage = CustomLanguage;
            }

            string languageId = LocalizedTextFontCatalogLanguageIds.ToCatalogId(fontLanguage);
            ILocalizationSource source = LocalizationContext.Source;
            if (source == null)
            {
                return;
            }

            FontRoleCatalog catalog = source.FontRoleCatalog;
            if (string.IsNullOrEmpty(languageId) || catalog == null)
            {
                return;
            }

            TMP_FontAsset fontAsset = catalog.GetFont(languageId, PipelineRole);
            if (fontAsset == null)
            {
                return;
            }

            text.text = string.Empty;
            text.font = fontAsset;
        }

        public void ValidateEnumStrings(string hierarchyPath)
        {
            if (ApplyFontFromPipeline && string.IsNullOrEmpty(PipelineRoleString))
            {
                GameLog.Warning("PipelineRole 문자열이 비어 있습니다: {0}", hierarchyPath);
            }

            if (CustomLanguage != LanguageNames.None && string.IsNullOrEmpty(CustomLanguageString))
            {
                GameLog.Warning(
                    "{0} 커스텀 언어의 문자열이 설정되지 않았습니다: {1}",
                    CustomLanguage,
                    hierarchyPath);
            }

            if (!EnumEx.ConvertTo(ref PipelineRole, PipelineRoleString))
            {
                GameLog.Error(
                    "PipelineRole을 갱신할 수 없습니다. {0}, {1}",
                    PipelineRoleString,
                    hierarchyPath);
            }

            if (!EnumEx.ConvertTo(ref CustomLanguage, CustomLanguageString))
            {
                GameLog.Error(
                    "{0} 커스텀 언어 타입을 갱신할 수 없습니다. {1}, {2}",
                    CustomLanguage,
                    CustomLanguageString,
                    hierarchyPath);
            }
        }

        public void AutoSetting()
        {
            PipelineRoleString = PipelineRole.ToString();
            if (CustomLanguage != LanguageNames.None)
            {
                CustomLanguageString = CustomLanguage.ToString();
            }
        }

        private void TryAcquireCustomLanguageLabel()
        {
            if (!UseCustomLanguage || CustomLanguage == LanguageNames.None)
            {
                return;
            }

            if (_customLanguageLabelHeld && _heldCustomLanguageLabel == CustomLanguage)
            {
                return;
            }

            TryReleaseCustomLanguageLabel();
            ILocalizationSource source = LocalizationContext.Source;
            if (source == null)
            {
                return;
            }

            source.AcquireFontLanguage(CustomLanguage);
            _customLanguageLabelHeld = true;
            _heldCustomLanguageLabel = CustomLanguage;
        }

        private void TryReleaseCustomLanguageLabel()
        {
            if (!_customLanguageLabelHeld)
            {
                return;
            }

            ILocalizationSource source = LocalizationContext.Source;
            if (source != null)
            {
                source.ReleaseFontLanguage(_heldCustomLanguageLabel);
            }

            _customLanguageLabelHeld = false;
            _heldCustomLanguageLabel = LanguageNames.None;
        }
    }
}