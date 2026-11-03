using System;

namespace Monet5379
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class FoldoutGroupAttribute : Attribute
    {
        public string GroupName { get; }

        public bool Expanded { get; }

        public int Order { get; }

        public FoldoutGroupAttribute(string groupName)
            : this(groupName, true, 0)
        {
        }

        public FoldoutGroupAttribute(string groupName, int order)
            : this(groupName, true, order)
        {
        }

        public FoldoutGroupAttribute(string groupName, bool expanded)
            : this(groupName, expanded, 0)
        {
        }

        public FoldoutGroupAttribute(string groupName, bool expanded, int order)
        {
            GroupName = groupName;
            Expanded = expanded;
            Order = order;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class ShowIfGroupAttribute : Attribute
    {
        public string MemberName { get; }

        public object Value { get; }

        public ShowIfGroupAttribute(string memberName)
        {
            MemberName = memberName;
            Value = null;
        }

        public ShowIfGroupAttribute(string memberName, object value)
        {
            MemberName = memberName;
            Value = value;
        }
    }
}
