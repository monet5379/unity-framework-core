namespace Monet5379
{
    public static class LocalizedTextFontCatalogLanguageIds
    {
        public static string ToCatalogId(LanguageNames language)
        {
            switch (language)
            {
                case LanguageNames.Korean:
                    return "KO";

                case LanguageNames.English:
                    return "EN";

                case LanguageNames.SimplifiedChinese:
                    return "SC";

                case LanguageNames.TraditionalChinese:
                    return "TC";

                case LanguageNames.Japanese:
                    return "JP";

                case LanguageNames.French:
                    return "FR";

                case LanguageNames.German:
                    return "DE";

                case LanguageNames.Italian:
                    return "IT";

                case LanguageNames.Spanish:
                    return "ES";

                default:
                    return null;
            }
        }
    }
}