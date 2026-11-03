using System;

namespace Monet5379
{
    public enum InfoMessageType
    {
        None,
        Info,
        Warning,
        Error,
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class TitleAttribute : Attribute
    {
        public string Title { get; }

        public string Subtitle { get; }

        public TitleAttribute()
        {
            Title = null;
            Subtitle = null;
        }

        public TitleAttribute(string title)
        {
            Title = title;
            Subtitle = null;
        }

        public TitleAttribute(string title, string subtitle)
        {
            Title = title;
            Subtitle = subtitle;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class LabelTextAttribute : Attribute
    {
        public string Text { get; }

        public LabelTextAttribute(string text)
        {
            Text = text;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class SuffixLabelAttribute : Attribute
    {
        public string Label { get; }

        public SuffixLabelAttribute(string label)
        {
            Label = label;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class HideLabelAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class InfoBoxAttribute : Attribute
    {
        public string Message { get; }

        public string VisibleIfMember { get; }

        public InfoMessageType MessageType { get; }

        public string VisibleIf { get; set; }

        public InfoBoxAttribute(string message)
        {
            Message = message;
            VisibleIfMember = null;
            MessageType = InfoMessageType.Info;
        }

        public InfoBoxAttribute(string message, string visibleIfMember)
        {
            Message = message;
            VisibleIfMember = visibleIfMember;
            MessageType = InfoMessageType.Info;
        }

        public InfoBoxAttribute(string message, InfoMessageType messageType)
        {
            Message = message;
            VisibleIfMember = null;
            MessageType = messageType;
        }
    }
}
