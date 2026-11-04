using TMPro;

namespace Monet5379
{
    [System.Serializable]
    public class LocalizedTextContentBinding
    {
        [StringTableKey]
        public string StringKey;

        public bool UseTmpTextWhenStringKeyEmpty;

        private string _content;
        private string _spriteContent;

        public void ClearRuntimeText()
        {
            _content = string.Empty;
        }

        public void SetRuntimeText(string content)
        {
            _content = content;
        }

        public bool TrySetSpriteText(string spriteContent)
        {
            if (_spriteContent == spriteContent)
            {
                return false;
            }

            _spriteContent = spriteContent;
            return true;
        }

        public void ClearSpriteText()
        {
            _spriteContent = string.Empty;
        }

        public bool HasSpriteContent()
        {
            return !string.IsNullOrEmpty(_spriteContent);
        }

        public void TryCaptureTmpTextWhenNoStringKey(TextMeshProUGUI text)
        {
            if (!UseTmpTextWhenStringKeyEmpty)
            {
                return;
            }

            if (!string.IsNullOrEmpty(StringKey))
            {
                return;
            }

            if (!string.IsNullOrEmpty(_content))
            {
                return;
            }

            if (text == null || string.IsNullOrEmpty(text.text))
            {
                return;
            }

            _content = text.text;
        }

        public void WriteTo(TextMeshProUGUI text)
        {
            if (text == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_spriteContent) && !string.IsNullOrEmpty(_content))
            {
                string result = $"{_spriteContent} {_content}";
                if (text.text != result)
                {
                    text.SetText(result);
                }
            }
            else if (!string.IsNullOrEmpty(_spriteContent))
            {
                if (text.text != _spriteContent)
                {
                    text.SetText(_spriteContent);
                }
            }
            else if (!string.IsNullOrEmpty(_content))
            {
                if (text.text != _content)
                {
                    text.SetText(_content);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(text.text))
                {
                    text.text = string.Empty;
                }
            }
        }

        public void ReloadFromTable(LanguageNames languageName)
        {
            if (string.IsNullOrEmpty(StringKey))
            {
                return;
            }

            ILocalizationSource source = LocalizationContext.Source;
            if (source == null)
            {
                return;
            }

            _content = source.TryGetString(StringKey, languageName, out string text) ? text : string.Empty;
        }

        public void AutoSettingStringKey()
        {
            if (!string.IsNullOrEmpty(StringKey))
            {
                StringKey = StringKey.Replace(" ", string.Empty);
            }
        }
    }
}