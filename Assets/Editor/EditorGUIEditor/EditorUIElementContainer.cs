using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorGUIEditor
{
    [CustomEditor(typeof(UIElementContainer))]
    public class EditorUIElementContainer : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("OpenWindow"))
            {
                EditorGUIEditorWindow.OpenWindowWithParam(target as UIElementContainer);
            }

            base.OnInspectorGUI();
        }
    }
}