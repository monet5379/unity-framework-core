using System;

namespace Monet5379
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class EnableIfAttribute : Attribute
    {
        public string MemberName { get; }

        public object Value { get; }

        public EnableIfAttribute(string memberName)
        {
            MemberName = memberName;
            Value = null;
        }

        public EnableIfAttribute(string memberName, object value)
        {
            MemberName = memberName;
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class DisableIfAttribute : Attribute
    {
        public string MemberName { get; }

        public object Value { get; }

        public DisableIfAttribute(string memberName)
        {
            MemberName = memberName;
            Value = null;
        }

        public DisableIfAttribute(string memberName, object value)
        {
            MemberName = memberName;
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class ShowIfAttribute : Attribute
    {
        public string MemberName { get; }

        public object Value { get; }

        public ShowIfAttribute(string memberName)
        {
            MemberName = memberName;
            Value = null;
        }

        public ShowIfAttribute(string memberName, object value)
        {
            MemberName = memberName;
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class ReadOnlyAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class GUIColorAttribute : Attribute
    {
        public string GetColor { get; }

        public GUIColorAttribute(string getColor)
        {
            GetColor = getColor;
        }
    }
}
