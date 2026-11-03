using UnityEditor;
using UnityEngine;

namespace Monet5379.Inspectors
{
    [CustomEditor(typeof(ScriptableObject), true)]
    [CanEditMultipleObjects]
    public sealed class ScriptableObjectInspector : FrameworkInspectorBase
    {
    }
}
