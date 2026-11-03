using System;

namespace Monet5379
{
    [Serializable]
    public struct LocalizedTextStyleEntry
    {
        public LocalizedTextUsage Usage;
        public bool UseBold;
        public bool UseItalic;
        public float FontSize;
    }
}