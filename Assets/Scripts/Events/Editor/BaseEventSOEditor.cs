using UnityEditor;
using UnityEngine;

namespace MobileGame.Events.Editor
{
    public abstract class BaseEventSOEditor<T> : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var eventSo = (BaseEventSO<T>)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Debug", EditorStyles.boldLabel);
            DrawLastSender(eventSo.LastSender);
            EditorGUILayout.LabelField("Listener Count", eventSo.ListenerCount.ToString());

            foreach (string description in eventSo.GetListenerDescriptions())
            {
                EditorGUILayout.LabelField("-", description);
            }

            if (EditorApplication.isPlaying)
            {
                Repaint();
            }
        }

        private static void DrawLastSender(object sender)
        {
            if (sender is UnityEngine.Object unityObject)
            {
                EditorGUILayout.ObjectField("Last Sender", unityObject, typeof(UnityEngine.Object), true);
                return;
            }

            EditorGUILayout.LabelField("Last Sender", sender?.ToString() ?? "None");
        }
    }

    [CustomEditor(typeof(IntEventSO))]
    public sealed class IntEventSOEditor : BaseEventSOEditor<int>
    {
    }

    [CustomEditor(typeof(ObjectEventSO))]
    public sealed class ObjectEventSOEditor : BaseEventSOEditor<object>
    {
    }
}
