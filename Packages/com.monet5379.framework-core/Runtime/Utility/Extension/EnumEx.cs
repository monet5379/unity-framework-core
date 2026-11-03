using ConditionalLog;
using System;
using System.Collections.Generic;

namespace Monet5379
{
    public static class EnumEx
    {
        public static T[] GetValues<T>(bool isExcludeFirst = false)
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            if (false == isExcludeFirst)
            {
                return array;
            }
            else if (array == null || array.Length < 2)
            {
                return array;
            }
            else
            {
                T[] newArray = new T[array.Length - 1];
                for (int i = 1; i < array.Length; i++)
                {
                    newArray[i - 1] = array[i];
                }

                return newArray;
            }
        }

        public static string[] GetNames<T>(bool isExcludeFirst = false)
        {
            string[] array = Enum.GetNames(typeof(T));

            if (false == isExcludeFirst)
            {
                return array;
            }
            else if (array == null || array.Length < 2)
            {
                return array;
            }
            else
            {
                string[] newArray = new string[array.Length - 1];
                for (int i = 1; i < array.Length; i++)
                {
                    newArray[i - 1] = array[i];
                }

                return newArray;
            }
        }

        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return BitConvert.IntToEnum32<TEnum>(value);
        }

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : Enum
        {
            return false == Enum.IsDefined(typeof(TEnum), value) ? default : (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }

        public static int ToInt(this bool boolValue)
        {
            return boolValue ? 1 : 0;
        }

        public static int ToInt(this float floatValue)
        {
            return (int)floatValue;
        }

        public static int ToInt<TEnum>(this TEnum enumName) where TEnum : Enum
        {
            return BitConvert.Enum32ToInt(enumName);
        }

        public static int[] ToInt<TEnum>(this TEnum[] enumNames) where TEnum : Enum
        {
            int[] result = new int[enumNames.Length];

            for (int i = 0; i < enumNames.Length; i++)
            {
                result[i] = BitConvert.Enum32ToInt(enumNames[i]);
            }

            return result;
        }

        public static float ToFloat<TEnum>(this TEnum enumName) where TEnum : Enum
        {
            return BitConvert.Enum32ToInt(enumName);
        }

        public static TEnum ConvertTo<TEnum>(this string nameString) where TEnum : struct
        {
            if (Enum.TryParse(nameString, out TEnum convertedName))
            {
                return convertedName;
            }

            LogWarningFailedToConvert<TEnum>(nameString);
            return default;
        }

        public static TEnum[] ConvertTo<TEnum>(this string[] nameStrings) where TEnum : struct
        {
            if (nameStrings != null && nameStrings.Length > 0)
            {
                TEnum[] names = new TEnum[nameStrings.Length];

                for (int i = 0; i < nameStrings.Length; i++)
                {
                    if (string.IsNullOrEmpty(nameStrings[i]) || nameStrings[i] == "None")
                    {
                        continue;
                    }

                    if (false == Enum.TryParse(nameStrings[i], out names[i]))
                    {
                        LogWarningFailedToConvert<TEnum>(nameStrings[i]);
                    }
                }

                return names;
            }

            return default;
        }

        public static bool ConvertTo<TEnum>(ref TEnum name, string nameString, bool ignoreLog = false) where TEnum : struct
        {
            if (string.IsNullOrEmpty(nameString))
            {
                LogFailedNullToConvert(name);
                return true;
            }
            if (nameString == "None")
            {
                return true;
            }

            if (false == Enum.TryParse(nameString, out TEnum convertedName))
            {
                if (!ignoreLog)
                {
                    LogErrorFailedToConvert<TEnum>(nameString);
                }

                name = default;
                return false;
            }

            name = convertedName;
            return true;
        }

        public static bool ConvertTo<TEnum>(ref TEnum[] names, string[] nameStrings) where TEnum : struct
        {
            bool result = true;

            if (nameStrings != null && nameStrings.Length > 0)
            {
                TEnum[] newNames = new TEnum[nameStrings.Length];
                for (int i = 0; i < nameStrings.Length; i++)
                {
                    if (string.IsNullOrEmpty(nameStrings[i]))
                    {
                        if (names.Length > i)
                        {
                            LogFailedNullToConvert(names[i]);
                        }

                        continue;
                    }
                    if (nameStrings[i] == "None")
                    {
                        continue;
                    }

                    if (false == Enum.TryParse(nameStrings[i], out newNames[i]))
                    {
                        LogErrorFailedToConvert<TEnum>(nameStrings[i]);
                        result = false;
                    }
                }

                names = newNames;
            }

            return result;
        }

        public static bool ConvertTo<TEnum>(ref TEnum[] names, int[] namesToInt) where TEnum : Enum
        {
            bool result = true;

            if (namesToInt != null && namesToInt.Length > 0)
            {
                names = new TEnum[namesToInt.Length];

                for (int i = 0; i < namesToInt.Length; i++)
                {
                    names[i] = namesToInt[i].ToEnum<TEnum>();
                }
            }

            return result;
        }

        public static void ExtractValidEnumsFromSaveData<TEnum>(ref List<TEnum> enumList, List<string> stringList) where TEnum : struct, Enum
        {
            if (enumList == null)
            {
                enumList = new List<TEnum>();
            }
            else
            {
                enumList.Clear();
            }

            foreach (string str in stringList)
            {
                if (Enum.TryParse<TEnum>(str, out TEnum value))
                {
                    enumList.Add(value);
                }
                else
                {
                    GameLog.Warning($"[ExtractValidEnumsFromSaveData] '{str}' is not a valid {typeof(TEnum).Name}.");
                }
            }
        }

        public static bool TryParseEnumListFromEditor<TEnum>(ref List<TEnum> enumList, List<string> stringList) where TEnum : struct, Enum
        {
            enumList = new List<TEnum>();

            foreach (string str in stringList)
            {
                if (Enum.TryParse<TEnum>(str, out TEnum value))
                {
                    enumList.Add(value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static TEnum GetName<TEnum>(this int index) where TEnum : Enum
        {
            TEnum[] list = GetValues<TEnum>();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].ToInt() == index)
                {
                    return list[i];
                }
            }

            LogFailedToFindByIndex<TEnum>(index);

            return default;
        }

        private static void LogFailedNullToConvert<TEnum>(TEnum name)
        {
            if (GameLog.LevelWarning)
            {
                if (name.ToString() == "None") return;
                GameLog.Warning($"변환에 실패하였습니다. enum:({name.ToString()}), Type:({typeof(TEnum)})");
            }
        }

        private static void LogWarningFailedToConvert<TEnum>(string nameString)
        {
            if (GameLog.LevelWarning)
            {
                GameLog.Warning($"변환에 실패하였습니다. string:({nameString}), Type:({typeof(TEnum)})");
            }
        }

        private static void LogErrorFailedToConvert<TEnum>(string nameString)
        {
            if (GameLog.LevelError)
            {
                GameLog.Error($"변환에 실패하였습니다. string:({nameString}), Type:({typeof(TEnum)})");
            }
        }

        private static void LogFailedToFindByIndex<TEnum>(int index)
        {
            if (GameLog.LevelWarning)
            {
                GameLog.Warning($"매개변수의 인덱스({index})가 enum에 존재하지 않습니다. Type:({typeof(TEnum)})");
            }
        }
    }
}