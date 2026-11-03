using TmpFontPipeline;

namespace Monet5379
{
    public interface ILocalizationSource
    {
        LanguageNames CurrentLanguage { get; }

        bool TryGetString(string key, LanguageNames language, out string text);

        FontRoleCatalog FontRoleCatalog { get; }

        LocalizedTextStyleTable StyleTable { get; }

        void AcquireFontLanguage(LanguageNames language);

        void ReleaseFontLanguage(LanguageNames language);
    }
}
