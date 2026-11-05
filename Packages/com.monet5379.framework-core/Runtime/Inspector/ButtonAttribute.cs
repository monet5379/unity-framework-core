using System;

namespace Monet5379
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ButtonAttribute : Attribute
    {
        public string Name { get; }

        public ButtonSizes Size { get; }

        public int Order { get; set; }

        public bool HideUnlessOverridden { get; set; }

        public ButtonAttribute()
            : this(null, ButtonSizes.Small)
        {
        }

        public ButtonAttribute(string name)
            : this(name, ButtonSizes.Small)
        {
        }

        public ButtonAttribute(ButtonSizes size)
            : this(null, size)
        {
        }

        public ButtonAttribute(string name, ButtonSizes size)
        {
            Name = name;
            Size = size;
        }
    }
}
