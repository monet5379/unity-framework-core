using ConditionalLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monet5379
{
    public class BitConvert
    {
        public static int Enum32ToInt<TEnum>(TEnum enumName) where TEnum : Enum
        {
            return Convert.ToInt32(enumName);
        }

        public static T IntToEnum32<T>(int value, T defaultValue = default) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.ToObject(typeof(T), value);
            }

            GameLog.Warning($"{value}는 {typeof(T)}에 정의되지 않은 값입니다. 기본값({defaultValue})을 반환합니다.");
            return defaultValue;
        }

        private static readonly Dictionary<Type, HashSet<int>> EnumValuesCache = new();

        public static T IntToEnum32Cached<T>(int value, T defaultValue = default) where T : Enum
        {
            Type enumType = typeof(T);
            if (!EnumValuesCache.ContainsKey(enumType))
            {
                EnumValuesCache[enumType] = new HashSet<int>(Enum.GetValues(enumType).Cast<int>());
            }

            if (EnumValuesCache[enumType].Contains(value))
            {
                return (T)Enum.ToObject(enumType, value);
            }

            GameLog.Warning($"{value}는 {enumType}에 정의되지 않은 값입니다. 기본값({defaultValue})을 반환합니다.");
            return defaultValue;
        }
    }
}