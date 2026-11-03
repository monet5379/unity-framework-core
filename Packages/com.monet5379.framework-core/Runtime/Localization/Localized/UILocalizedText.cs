using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Monet5379
{
    public partial class UILocalizedText : XBehaviour
    {
        [FormerlySerializedAs("_content")]
        [SerializeField] private LocalizedTextContentBinding _contentBinding = new();

        [FormerlySerializedAs("_font")]
        [SerializeField] private LocalizedTextFontBinding _fontBinding = new();

        [FormerlySerializedAs("_typography")]
        [SerializeField] private LocalizedTextTypographyBinding _typographyBinding = new();

        [FormerlySerializedAs("TextPro")]
        [FormerlySerializedAs("_textPro")]
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        [FormerlySerializedAs("IsBlockRefreshOnGlobalEvent")]
        [FormerlySerializedAs("_isBlockRefreshOnGlobalEvent")]
        [SerializeField] private bool _ignoreLanguageChangedEvent;

        public float OverrideFontSize { get; set; }

        private LanguageNames _lastAppliedLanguage;
        private float _defaultFontSize;

        public TextMeshProUGUI TextMeshPro => _textMeshPro;

        public Color InitialTextColor { get; private set; }

        public int DisplayedFontSize => _textMeshPro != null ? (int)_textMeshPro.fontSize : 0;

        public LocalizedTextContentBinding Content => _contentBinding;

        public LocalizedTextFontBinding Font => _fontBinding;

        public LocalizedTextTypographyBinding Typography => _typographyBinding;

        public string StringKey
        {
            get => _contentBinding.StringKey;
            set => _contentBinding.StringKey = value;
        }

        protected void Awake()
        {
            EnsureTextMeshProAssigned();

            if (_textMeshPro != null)
            {
                InitialTextColor = _textMeshPro.color;
                _defaultFontSize = _textMeshPro.fontSize;
            }
        }

        protected override void RegisterGlobalEvent()
        {
            LocalizationContext.LanguageChanged += OnGameLanguageChanged;
        }

        protected override void UnregisterGlobalEvent()
        {
            LocalizationContext.LanguageChanged -= OnGameLanguageChanged;
        }

        private void OnGameLanguageChanged()
        {
            if (_ignoreLanguageChangedEvent)
            {
                return;
            }

            RefreshIfLanguageChanged();
        }

        protected override void OnEnabled()
        {
            _fontBinding.OnEnabled();

            base.OnEnabled();

            RefreshIfLanguageChanged();
        }

        protected override void OnDisabled()
        {
            _fontBinding.OnDisabled();
            base.OnDisabled();
        }

        private void RefreshIfLanguageChanged()
        {
            ILocalizationSource source = LocalizationContext.Source;
            if (source == null)
            {
                return;
            }

            LanguageNames current = source.CurrentLanguage;
            if (_lastAppliedLanguage == current)
            {
                return;
            }

            _lastAppliedLanguage = current;
            RefreshForLanguage(current);
        }

        public void ClearRuntimeText()
        {
            _contentBinding.ClearRuntimeText();
            ApplyContent();
        }

        public void SetRuntimeText(string text)
        {
            if (_textMeshPro == null)
            {
                return;
            }

            _contentBinding.SetRuntimeText(text);
            ApplyContent();
        }

        public void SetStringKey(string stringKey)
        {
            if (_contentBinding.StringKey == stringKey)
            {
                return;
            }

            _contentBinding.StringKey = stringKey;
            ILocalizationSource source = LocalizationContext.Source;
            if (source != null)
            {
                _contentBinding.ReloadFromTable(source.CurrentLanguage);
            }

            ApplyContent();
        }

        public void ResetStringKey()
        {
            if (string.IsNullOrEmpty(_contentBinding.StringKey))
            {
                return;
            }

            _contentBinding.StringKey = string.Empty;
            _contentBinding.ClearRuntimeText();
            ApplyContent();
        }

        public void SetSpritePrefix(string spritePrefix)
        {
            if (_textMeshPro == null)
            {
                return;
            }

            if (!_contentBinding.TrySetSpriteText(spritePrefix))
            {
                return;
            }

            ApplyContent();
        }

        public void ClearSpritePrefix()
        {
            if (_textMeshPro == null || !_contentBinding.HasSpriteContent())
            {
                return;
            }

            _contentBinding.ClearSpriteText();
            ApplyContent();
        }

        private void ApplyContent()
        {
            _contentBinding.WriteTo(_textMeshPro);
        }

        public void SetDefaultFontSize(float fontSize)
        {
            _defaultFontSize = fontSize;
            ApplyTypography();
        }

        public void ApplyTypography()
        {
            _typographyBinding.Apply(_textMeshPro, _defaultFontSize, OverrideFontSize, this);
        }

        public void RestoreInitialTextColor()
        {
            if (_textMeshPro != null)
            {
                _textMeshPro.color = InitialTextColor;
            }
        }

        public void RefreshForLanguage(LanguageNames language)
        {
            if (_textMeshPro == null)
            {
                return;
            }

            _fontBinding.Apply(_textMeshPro, language);
            _typographyBinding.Apply(_textMeshPro, _defaultFontSize, OverrideFontSize, this);
            _contentBinding.ReloadFromTable(language);
            ApplyContent();
        }
    }
}