using Monet5379.Inspectors.Internal;
using UnityEditor;
using UnityEngine.UIElements;

namespace Monet5379.Inspectors
{
    public abstract class FrameworkInspectorBase : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            InspectorLayoutBuilder.Build(root, serializedObject, this);
            return root;
        }
    }
}
