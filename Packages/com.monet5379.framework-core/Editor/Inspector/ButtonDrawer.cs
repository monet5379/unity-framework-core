using System.Collections.Generic;
using System.Reflection;
using Monet5379;
using Monet5379.Inspectors.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Monet5379.Inspectors
{
    public static class ButtonDrawer
    {
        private const string FOLDOUT_TEXT = "Buttons";
        private const string FOLDOUT_VIEW_DATA_KEY = "Monet5379.ButtonDrawer.Foldout";
        private const float FOLDOUT_MARGIN_TOP = 6f;

        public static void Append(VisualElement root, Editor editor)
        {
            if (root == null || editor == null || editor.target == null)
            {
                return;
            }

            ButtonMethod[] methods = ButtonMethodCache.Get(editor.target.GetType());
            if (methods.Length == 0)
            {
                return;
            }

            Foldout foldout = new()
            {
                text = FOLDOUT_TEXT,
                value = true,
                viewDataKey = FOLDOUT_VIEW_DATA_KEY,
            };
            foldout.style.marginTop = FOLDOUT_MARGIN_TOP;

            for (int i = 0; i < methods.Length; i++)
            {
                foldout.Add(CreateButton(methods[i], editor));
            }

            root.Add(foldout);
        }

        private static Button CreateButton(ButtonMethod buttonMethod, Editor editor)
        {
            Button button = new(() => Invoke(buttonMethod, editor))
            {
                text = buttonMethod.Label,
                tooltip = buttonMethod.UnsupportedReason ?? string.Empty,
            };

            if (buttonMethod.Attribute.Size != ButtonSizes.Small)
            {
                button.style.height = (int)buttonMethod.Attribute.Size;
            }

            button.SetEnabled(buttonMethod.IsSupported);
            return button;
        }

        private static void Invoke(ButtonMethod buttonMethod, Editor editor)
        {
            MethodInfo method = buttonMethod.Method;
            Object[] targets = editor.targets;

            if (method.IsStatic)
            {
                InvokeSafe(buttonMethod, null, editor.target);
                return;
            }

            Undo.RecordObjects(CollectUndoObjects(targets), buttonMethod.Label);

            for (int i = 0; i < targets.Length; i++)
            {
                Object target = targets[i];
                if (target == null)
                {
                    continue;
                }

                InvokeSafe(buttonMethod, target, target);
                EditorUtility.SetDirty(target);

                if (PrefabUtility.IsPartOfPrefabInstance(target))
                {
                    PrefabUtility.RecordPrefabInstancePropertyModifications(target);
                }
            }

            editor.serializedObject.Update();
        }

        private static void InvokeSafe(ButtonMethod buttonMethod, Object target, Object context)
        {
            try
            {
                buttonMethod.Method.Invoke(target, buttonMethod.Arguments);
            }
            catch (TargetInvocationException exception)
            {
                Debug.LogException(exception.InnerException ?? exception, context);
            }
        }

        private static Object[] CollectUndoObjects(Object[] targets)
        {
            List<Object> undoObjects = new(targets.Length * 2);
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                {
                    continue;
                }

                undoObjects.Add(targets[i]);

                if (targets[i] is Component component)
                {
                    undoObjects.Add(component.gameObject);
                }
            }

            return undoObjects.ToArray();
        }
    }
}
