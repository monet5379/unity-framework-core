using System.Text;

namespace Monet5379
{
    public static class StringGetter
    {
        private static readonly StringBuilder _stringBuilder = new();

        public static string ConcatString(params string[] values)
        {
            if (values == null)
            {
                return null;
            }

            if (values.Length == 1)
            {
                return values[0];
            }

            _stringBuilder.Clear();

            for (int i = 0; i < values.Length; i++)
            {
                _stringBuilder.Append(values[i]);
            }

            return _stringBuilder.ToString();
        }

        public static string ConcatStringWithComma(params string[] values)
        {
            _stringBuilder.Clear();

            for (int i = 0; i < values.Length; i++)
            {
                _stringBuilder.Append(values[i]);
                if (i < values.Length - 1)
                {
                    _stringBuilder.Append(", ");
                }
            }

            return _stringBuilder.ToString();
        }
    }
}