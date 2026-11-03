using UnityEngine;

namespace Monet5379
{
    public static class RandomEx
    {
        private static readonly System.Random random;

#if UNITY_EDITOR
        public static float? EditModeNextUnitFloat;
#endif

        static RandomEx()
        {
            int seed = Application.version.GetHashCode();
            random = new System.Random(seed);
        }

        public static bool GetBoolValue()
        {
            return random.Next(0, 2) == 1;
        }

        public static float GetFloatValue()
        {
#if UNITY_EDITOR
            if (EditModeNextUnitFloat.HasValue)
            {
                float value = EditModeNextUnitFloat.Value;
                EditModeNextUnitFloat = null;
                return value;
            }
#endif
            return (float)random.NextDouble();
        }

        #region Vector

        public static Vector2 GetVector2Value(Vector2 size)
        {
            float x = size.x.IsZero() ? 0 : Range(-size.x, size.x);
            float y = size.y.IsZero() ? 0 : Range(-size.y, size.y);

            return new Vector2(x, y);
        }

        public static Vector3 GetVector3Value(Vector2 size)
        {
            float x = size.x.IsZero() ? 0 : Range(-size.x, size.x);
            float y = size.y.IsZero() ? 0 : Range(-size.y, size.y);

            return new Vector3(x, y, 0);
        }

        #endregion Vector

        public static int Range(int max)
        {
            return Range(0, max);
        }

        public static float Range(float max)
        {
            return Range(0, max);
        }

        public static int Range(int min, int max)
        {
            if (min >= max)
            {
                return min;
            }
            else
            {
                return random.Next(min, max);
            }
        }

        public static float Range(float min, float max)
        {
            if (min >= max || Mathf.Approximately(min, max))
            {
                return min;
            }
            else
            {
                float randomValue = GetFloatValue();
                return (randomValue * (max - min)) + min;
            }
        }
    }
}